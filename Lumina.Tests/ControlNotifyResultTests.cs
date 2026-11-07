using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlNotifyResultTests
{
    private const int WmNotify = 0x004E;
    private const int WmSetText = 0x000C;
    private const int EmReplaceSel = 0x00C2;
    private const int EnProtected = 0x0704;
    private const int EnRequestResize = 0x0701;
    private const int EnSelectionChange = 0x0702;
    private const int CustomNotification = unchecked((int)0x87654321);

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void SyntheticProtectedRejectionReturnsOneThroughEveryReceiver(int route)
        => OnSta(() =>
        {
            using var rich = new RichTextBox();
            using var host = new ReceiverHost(rich, route);
            int notifications = 0;
            rich.Protected += (_, _) => notifications++;

            nint result = SendProtected(host.Receiver, rich.Handle, NativeId(rich.Handle), EmReplaceSel);

            Assert.Equal((nint)1, result);
            Assert.Equal(1, notifications);
            Assert.Equal(rich.Handle, host.Source);
            Assert.Same(rich, Control.FromHandle(host.Source));
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void SyntheticAllowedProtectedMessageReturnsZeroWithoutAnEvent(int route)
        => OnSta(() =>
        {
            using var rich = new RichTextBox();
            using var host = new ReceiverHost(rich, route);
            int notifications = 0;
            rich.Protected += (_, _) => notifications++;

            nint result = SendProtected(host.Receiver, rich.Handle, NativeId(rich.Handle), WmSetText);

            Assert.Equal((nint)0, result);
            Assert.Equal(0, notifications);
            Assert.Equal(host.Source, rich.Handle);
        });

    [Theory]
    [InlineData(0x0444, 0, 0x10, 0)]
    [InlineData(0x0444, 0, 0x40000010, 0)]
    [InlineData(0x0444, 0, 0x40000000, 1)]
    [InlineData(0x0449, 0, 0, 0)]
    [InlineData(0x0449, 0x8000, 0, 1)]
    [InlineData(0x0301, 0, 0, 0)]
    [InlineData(0x000C, 0, 0, 0)]
    [InlineData(0x0435, 0, 0, 0)]
    [InlineData(0x0447, 0, 0, 1)]
    [InlineData(0x00C2, 0, 0, 1)]
    [InlineData(0x0102, 0, 0, 1)]
    public void ProtectedMessageBranchesReturnTheirDecisionThroughTheForm(
        int message, int flags, int mask, int expected)
        => OnSta(() =>
        {
            using var rich = new RichTextBox();
            using var host = new ReceiverHost(rich, 0);
            int notifications = 0;
            rich.Protected += (_, _) => notifications++;
            nint format = Marshal.AllocHGlobal(116);
            try
            {
                Marshal.Copy(new byte[116], 0, format, 116);
                Marshal.WriteInt32(format, 0, 116);
                Marshal.WriteInt32(format, 4, mask);

                // Only the enclosing WM_NOTIFY is sent. In particular, the
                // WM_COPY branch does not access the system clipboard.
                nint result = SendProtected(host.Receiver, rich.Handle,
                    NativeId(rich.Handle), message, (nint)flags, format);

                Assert.Equal((nint)expected, result);
                Assert.Equal(expected, notifications);
                Assert.Equal(host.Source, rich.Handle);
            }
            finally { Marshal.FreeHGlobal(format); }
        });

    [Theory]
    [InlineData(0, false)]
    [InlineData(0, true)]
    [InlineData(1, false)]
    [InlineData(1, true)]
    [InlineData(2, false)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(3, true)]
    public void LegacyNotificationKeepsZeroResultAndInvokesTheExistingOverrideOnce(int route, bool handled)
        => OnSta(() =>
        {
            using var control = new LegacyNotifyControl { Handled = handled };
            using var host = new ReceiverHost(control, route);
            control.Calls.Clear();
            NativeHeader header = Header(control.Handle, NativeId(control.Handle), CustomNotification);

            WithPayload(header, pointer =>
            {
                Assert.Equal((nint)0, SendMessageW(host.Receiver, WmNotify, (nint)header.Id, pointer));
                NotifyCall call = Assert.Single(control.Calls);
                Assert.Equal(CustomNotification, call.Code);
                Assert.Equal(pointer, call.Payload);
                Assert.Equal(control.Handle, call.Source);
            });

            Assert.Equal(host.Source, control.Handle);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void LegacyInternalEntryPreservesItsBooleanAndDoesNotCreateAHandle(bool handled)
        => OnSta(() =>
        {
            using var control = new LegacyNotifyControl { Handled = handled };
            WithPayload(Header(0, 0, CustomNotification), pointer =>
            {
                MethodInfo method = typeof(Control).GetMethod("HandleNotify",
                    BindingFlags.Instance | BindingFlags.NonPublic)!;

                Assert.Equal(handled, (bool)method.Invoke(control, [CustomNotification, pointer])!);

                NotifyCall call = Assert.Single(control.Calls);
                Assert.Equal(CustomNotification, call.Code);
                Assert.Equal(pointer, call.Payload);
                Assert.Equal((nint)0, call.Source);
            });
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ResultAwareBaseEntryResetsTheOutValueAndKeepsTheLegacyBoolean(bool handled)
        => OnSta(() =>
        {
            using var control = new LegacyNotifyControl { Handled = handled };
            WithPayload(Header(0, 0, CustomNotification), pointer =>
            {
                MethodInfo method = typeof(Control).GetMethod("HandleNotifyWithResult",
                    BindingFlags.Instance | BindingFlags.NonPublic)!;
                object?[] arguments = [CustomNotification, pointer, unchecked((nint)(-123456789))];

                Assert.Equal(handled, (bool)method.Invoke(control, arguments)!);

                Assert.Equal((nint)0, Assert.IsType<nint>(arguments[2]));
                NotifyCall call = Assert.Single(control.Calls);
                Assert.Equal(CustomNotification, call.Code);
                Assert.Equal(pointer, call.Payload);
            });
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void FormUsesTheSourceWindowWhenTheHeaderContainsAnotherControlsId()
        => OnSta(() =>
        {
            using var form = new Form();
            using var first = new RichTextBox();
            using var second = new RichTextBox();
            _ = form.Handle;
            form.Controls.Add(first);
            form.Controls.Add(second);
            _ = first.Handle;
            _ = second.Handle;
            Assert.NotEqual(NativeId(first.Handle), NativeId(second.Handle));
            int firstNotifications = 0;
            int secondNotifications = 0;
            first.Protected += (_, _) => firstNotifications++;
            second.Protected += (_, _) => secondNotifications++;

            nint result = SendProtected(form.Handle, second.Handle, NativeId(first.Handle), EmReplaceSel);

            Assert.Equal((nint)1, result);
            Assert.Equal(0, firstNotifications);
            Assert.Equal(1, secondNotifications);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(305419896)]
    public void FormDoesNotUseAKnownIdToRouteAnUnmappedSource(int source)
        => OnSta(() =>
        {
            using var rich = new RichTextBox();
            using var host = new ReceiverHost(rich, 0);
            int notifications = 0;
            rich.Protected += (_, _) => notifications++;

            nint result = SendProtected(host.Receiver, (nint)source, NativeId(rich.Handle), EmReplaceSel);

            Assert.Equal((nint)0, result);
            Assert.Equal(0, notifications);
            Assert.Same(rich, Control.FromHandle(rich.Handle));
        });

    [Fact]
    public void OriginalLiveFormReturnsTheReparentedSourcesDecisionDespiteAnIdCollision()
        => OnSta(() =>
        {
            using var originalForm = new Form();
            using var newForm = new Form();
            using var moved = new RichTextBox();
            using var replacement = new RichTextBox();
            _ = originalForm.Handle;
            _ = newForm.Handle;
            originalForm.Controls.Add(moved);
            nint source = moved.Handle;
            newForm.Controls.Add(moved);
            originalForm.Controls.Add(replacement);
            _ = replacement.Handle;
            Assert.Equal(newForm.Handle, GetParent(source));
            int movedNotifications = 0;
            int replacementNotifications = 0;
            moved.Protected += (_, _) => movedNotifications++;
            replacement.Protected += (_, _) => replacementNotifications++;

            nint result = SendProtected(originalForm.Handle, source, NativeId(replacement.Handle), EmReplaceSel);

            Assert.Equal((nint)1, result);
            Assert.Equal(1, movedNotifications);
            Assert.Equal(0, replacementNotifications);
            Assert.Equal(source, moved.Handle);
            Assert.Equal(newForm.Handle, GetParent(source));
        });

    [Fact]
    public void ParkingReceiverRejectsAnotherControlsSourceEvenWithItsOwnId()
        => OnSta(() =>
        {
            using var first = new RichTextBox();
            using var second = new RichTextBox();
            nint firstHandle = first.Handle;
            nint secondHandle = second.Handle;
            nint firstParking = GetParent(firstHandle);
            nint secondParking = GetParent(secondHandle);
            Assert.NotEqual(firstParking, secondParking);
            Assert.Null(Control.FromHandle(firstParking));
            Assert.Null(Control.FromHandle(secondParking));
            int firstNotifications = 0;
            int secondNotifications = 0;
            first.Protected += (_, _) => firstNotifications++;
            second.Protected += (_, _) => secondNotifications++;

            Assert.Equal((nint)0,
                SendProtected(firstParking, secondHandle, NativeId(firstHandle), EmReplaceSel));
            Assert.Equal(0, firstNotifications);
            Assert.Equal(0, secondNotifications);

            Assert.Equal((nint)1,
                SendProtected(secondParking, secondHandle, NativeId(firstHandle), EmReplaceSel));
            Assert.Equal(0, firstNotifications);
            Assert.Equal(1, secondNotifications);
        });

    [Fact]
    public void RetainedParkingReceiverReturnsProtectedDecisionsAfterReparenting()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new RichTextBox();
            form.Controls.Add(rich);
            nint source = rich.Handle;
            nint parking = GetParent(source);
            Assert.False(form.IsHandleCreated);
            Assert.Null(Control.FromHandle(parking));
            form.Show();
            Assert.Equal(source, rich.Handle);
            Assert.Equal(form.Handle, GetParent(source));
            Assert.True(IsWindow(parking));
            int notifications = 0;
            rich.Protected += (_, _) => notifications++;

            Assert.Equal((nint)0, SendProtected(parking, source, 0, WmSetText));
            Assert.Equal(0, notifications);
            Assert.Equal((nint)1, SendProtected(parking, source, 0, EmReplaceSel));
            Assert.Equal(1, notifications);
            Assert.Equal(source, rich.Handle);
            Assert.Equal(form.Handle, GetParent(source));
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void ExistingResizeNotificationKeepsZeroResultAndItsRectangle(int route)
        => OnSta(() =>
        {
            using var rich = new RichTextBox { BorderStyle = BorderStyle.None };
            using var host = new ReceiverHost(rich, route);
            List<Rectangle> received = [];
            rich.ContentsResized += (_, arguments) => received.Add(arguments.NewRectangle);
            NativeResize request = new()
            {
                Header = Header(rich.Handle, NativeId(rich.Handle), EnRequestResize),
                Bounds = new NativeRectangle(1, 2, 30, 40)
            };

            WithPayload(request, pointer =>
                Assert.Equal((nint)0, SendMessageW(host.Receiver, WmNotify, 0, pointer)));

            Assert.Equal([Rectangle.FromLTRB(1, 2, 30, 40)], received);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void ExistingSelectionNotificationKeepsZeroResultAndDispatchesOnce(int route)
        => OnSta(() =>
        {
            using var rich = new RichTextBox();
            using var host = new ReceiverHost(rich, route);
            int notifications = 0;
            rich.SelectionChanged += (_, _) => notifications++;
            NativeSelection request = new()
            {
                Header = Header(rich.Handle, NativeId(rich.Handle), EnSelectionChange),
                Range = new NativeRange(2, 5),
                Type = 1
            };

            WithPayload(request, pointer =>
            {
                Assert.Equal((nint)0, SendMessageW(host.Receiver, WmNotify, 0, pointer));
                Assert.Equal((nint)0, SendMessageW(host.Receiver, WmNotify, 0, pointer));
            });

            Assert.Equal(1, notifications);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void NullNotifyPayloadReturnsZeroWithoutCallingTheSource(int route)
        => OnSta(() =>
        {
            using var control = new LegacyNotifyControl { Handled = true };
            using var host = new ReceiverHost(control, route);
            control.Calls.Clear();

            Assert.Equal((nint)0, SendMessageW(host.Receiver, WmNotify, (nint)NativeId(control.Handle), 0));

            Assert.Empty(control.Calls);
            Assert.Equal(host.Source, control.Handle);
        });

    private static NativeHeader Header(nint source, nuint id, int code) => new()
    {
        Window = source,
        Id = id,
        Code = unchecked((uint)code)
    };

    private static nuint NativeId(nint handle) => unchecked((nuint)GetDlgCtrlID(handle));

    private static nint SendProtected(nint receiver, nint source, nuint id, int message,
        nint wParam = 0, nint lParam = 0)
    {
        NativeProtected request = new()
        {
            Header = Header(source, id, EnProtected),
            Message = unchecked((uint)message),
            WParam = wParam,
            LParam = lParam,
            Range = new NativeRange(0, 0)
        };
        nint result = 0;
        WithPayload(request, pointer => result = SendMessageW(receiver, WmNotify, (nint)id, pointer));
        return result;
    }

    private static void WithPayload<T>(T request, Action<nint> action) where T : struct
    {
        nint pointer = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
        try
        {
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "Control notification-result test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ReceiverHost : IDisposable
    {
        private readonly Form? _form;
        private readonly Panel? _panel;

        internal ReceiverHost(Control source, int route)
        {
            switch (route)
            {
                case 0:
                    _form = new Form();
                    _ = _form.Handle;
                    _form.Controls.Add(source);
                    Source = source.Handle;
                    Receiver = _form.Handle;
                    break;
                case 1:
                    _form = new Form();
                    _panel = new Panel();
                    _ = _form.Handle;
                    _form.Controls.Add(_panel);
                    _ = _panel.Handle;
                    _panel.Controls.Add(source);
                    Source = source.Handle;
                    Receiver = _panel.Handle;
                    break;
                case 2:
                    _panel = new Panel();
                    _ = _panel.Handle;
                    _panel.Controls.Add(source);
                    Source = source.Handle;
                    Receiver = _panel.Handle;
                    break;
                case 3:
                    Source = source.Handle;
                    Receiver = GetParent(Source);
                    Assert.NotEqual((nint)0, Receiver);
                    Assert.Null(Control.FromHandle(Receiver));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(route));
            }
            Assert.NotEqual((nint)0, Source);
            Assert.NotEqual((nint)0, Receiver);
            Assert.Equal(Receiver, GetParent(Source));
        }

        internal nint Source { get; }
        internal nint Receiver { get; }

        public void Dispose()
        {
            _panel?.Dispose();
            _form?.Dispose();
        }
    }

    private sealed class LegacyNotifyControl : Control
    {
        internal bool Handled { get; set; }
        internal List<NotifyCall> Calls { get; } = [];

        protected override bool OnNotify(int notificationCode, nint lParam)
        {
            NativeHeader header = Marshal.PtrToStructure<NativeHeader>(lParam);
            Calls.Add(new NotifyCall(notificationCode, lParam, header.Window));
            return Handled;
        }
    }

    private readonly record struct NotifyCall(int Code, nint Payload, nint Source);

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeHeader
    {
        internal nint Window;
        internal nuint Id;
        internal uint Code;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct NativeProtected
    {
        internal NativeHeader Header;
        internal uint Message;
        internal nint WParam;
        internal nint LParam;
        internal NativeRange Range;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    private struct NativeSelection
    {
        internal NativeHeader Header;
        internal NativeRange Range;
        internal ushort Type;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeResize
    {
        internal NativeHeader Header;
        internal NativeRectangle Bounds;
    }

    [StructLayout(LayoutKind.Sequential)]
    private readonly record struct NativeRange(int Minimum, int Maximum);

    [StructLayout(LayoutKind.Sequential)]
    private readonly record struct NativeRectangle(int Left, int Top, int Right, int Bottom);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetDlgCtrlID(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
