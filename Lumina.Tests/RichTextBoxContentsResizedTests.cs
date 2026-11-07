using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxContentsResizedTests
{
    private const int EmGetEventMask = 0x043B;
    private const int EmRequestResize = 0x0441;
    private const int EnRequestResize = 0x0701;
    private const int EnmRequestResize = 0x00040000;

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 2, 30, 40)]
    [InlineData(-20, -30, 10, 5)]
    [InlineData(5, 6, -10, -12)]
    [InlineData(int.MinValue, int.MaxValue, int.MaxValue, int.MinValue)]
    public void EventArgumentsPreserveTheSuppliedRectangle(int x, int y, int width, int height)
    {
        Rectangle rectangle = new(x, y, width, height);
        var arguments = new ContentsResizedEventArgs(rectangle);

        Assert.Equal(rectangle, arguments.NewRectangle);
        Assert.Equal(rectangle, arguments.NewRectangle);
    }

    [Fact]
    public void EventArgumentsHaveTheStandardPublicConstructorAndReadOnlyProperty()
    {
        Type type = typeof(ContentsResizedEventArgs);
        Assert.True(type.IsPublic);
        Assert.False(type.IsSealed);
        Assert.Equal(typeof(EventArgs), type.BaseType);
        ConstructorInfo constructor = Assert.Single(type.GetConstructors());
        ParameterInfo parameter = Assert.Single(constructor.GetParameters());
        Assert.Equal(typeof(Rectangle), parameter.ParameterType);
        Assert.Equal("newRectangle", parameter.Name);
        PropertyInfo property = type.GetProperty(nameof(ContentsResizedEventArgs.NewRectangle))!;
        Assert.Equal(typeof(Rectangle), property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.False(property.GetMethod.IsVirtual);
        Assert.Null(property.SetMethod);
    }

    [Fact]
    public void EventHandlerHasTheStandardPublicDelegateSignature()
    {
        Type type = typeof(ContentsResizedEventHandler);
        Assert.True(type.IsPublic);
        Assert.Equal(typeof(MulticastDelegate), type.BaseType);
        MethodInfo invoke = type.GetMethod("Invoke")!;
        Assert.Equal(typeof(void), invoke.ReturnType);
        ParameterInfo[] parameters = invoke.GetParameters();
        Assert.Equal(2, parameters.Length);
        Assert.Equal(typeof(object), parameters[0].ParameterType);
        Assert.Equal("sender", parameters[0].Name);
        Assert.Equal(typeof(ContentsResizedEventArgs), parameters[1].ParameterType);
        Assert.Equal("e", parameters[1].Name);
        Assert.Equal(NullabilityState.Nullable, new NullabilityInfoContext().Create(parameters[0]).ReadState);
    }

    [Fact]
    public void EventAndProtectedRaiserUseTheStandardDeclaredSurface()
    {
        EventInfo notification = typeof(RichTextBox).GetEvent(nameof(RichTextBox.ContentsResized),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(notification);
        Assert.Equal(typeof(ContentsResizedEventHandler), notification.EventHandlerType);
        Assert.True(notification.AddMethod!.IsPublic);
        Assert.False(notification.AddMethod.IsVirtual);
        Assert.True(notification.RemoveMethod!.IsPublic);
        Assert.False(notification.RemoveMethod.IsVirtual);
        Assert.Equal("Behavior", notification.GetCustomAttribute<CategoryAttribute>()!.Category);
        MethodInfo raise = typeof(RichTextBox).GetMethod("OnContentsResized",
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!;
        Assert.True(raise.IsFamily);
        Assert.True(raise.IsVirtual);
        Assert.False(raise.IsFinal);
        Assert.Equal(typeof(void), raise.ReturnType);
        Assert.Equal(typeof(ContentsResizedEventArgs), Assert.Single(raise.GetParameters()).ParameterType);
    }

    [Fact]
    public void RaisingWithoutSubscribersDoesNotCreateAHandle()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox();
            rich.Raise(new ContentsResizedEventArgs(new Rectangle(1, 2, 3, 4)));
            Assert.False(rich.IsHandleCreated);
            Assert.Equal(1, rich.VirtualCalls);
        });

    [Fact]
    public void RaisingPreservesTheSenderAndArgumentIdentity()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox();
            var arguments = new ContentsResizedEventArgs(new Rectangle(-1, 2, 30, 40));
            object? sender = null;
            ContentsResizedEventArgs? received = null;
            rich.ContentsResized += (source, value) => { sender = source; received = value; };

            rich.Raise(arguments);

            Assert.Same(rich, sender);
            Assert.Same(arguments, received);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void DirectRaisingPassesANullArgumentUnchanged()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox();
            int calls = 0;
            rich.ContentsResized += (sender, value) =>
            {
                Assert.Same(rich, sender);
                Assert.Null(value);
                calls++;
            };

            rich.Raise(null);

            Assert.Equal(1, calls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void DuplicateSubscriptionsAndRemovalFollowDelegateOrder()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox();
            List<string> calls = [];
            ContentsResizedEventHandler first = (_, _) => calls.Add("first");
            ContentsResizedEventHandler second = (_, _) => calls.Add("second");
            rich.ContentsResized += first;
            rich.ContentsResized += second;
            rich.ContentsResized += first;
            rich.Raise(new ContentsResizedEventArgs(Rectangle.Empty));
            Assert.Equal(["first", "second", "first"], calls);
            calls.Clear();
            rich.ContentsResized -= first;
            rich.Raise(new ContentsResizedEventArgs(Rectangle.Empty));
            Assert.Equal(["first", "second"], calls);
            calls.Clear();
            rich.ContentsResized -= first;
            rich.ContentsResized -= second;
            rich.Raise(new ContentsResizedEventArgs(Rectangle.Empty));
            Assert.Empty(calls);
        });

    [Fact]
    public void ComponentCanRaiseEventsControlsTheEventSlotWithoutSkippingTheVirtualRaiser()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { EventsAllowed = false };
            int calls = 0;
            rich.ContentsResized += (_, _) => calls++;
            rich.Raise(new ContentsResizedEventArgs(Rectangle.Empty));
            Assert.Equal(0, calls);
            Assert.Equal(1, rich.VirtualCalls);
            rich.EventsAllowed = true;
            rich.Raise(new ContentsResizedEventArgs(Rectangle.Empty));
            Assert.Equal(1, calls);
            Assert.Equal(2, rich.VirtualCalls);
        });

    [Fact]
    public void AProtectedOverrideCanSuppressBaseNotification()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { InvokeBaseRaiser = false };
            int calls = 0;
            rich.ContentsResized += (_, _) => calls++;
            rich.Raise(new ContentsResizedEventArgs(Rectangle.Empty));
            Assert.Equal(1, rich.VirtualCalls);
            Assert.Equal(0, calls);
        });

    [Fact]
    public void HandlerExceptionsRetainTheirIdentityAndStopTheCurrentInvocation()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox();
            var failure = new InvalidOperationException("contents resize handler");
            int laterCalls = 0;
            ContentsResizedEventHandler throws = (_, _) => throw failure;
            rich.ContentsResized += throws;
            rich.ContentsResized += (_, _) => laterCalls++;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() =>
                rich.Raise(new ContentsResizedEventArgs(Rectangle.Empty))));
            Assert.Equal(0, laterCalls);
            rich.ContentsResized -= throws;
            rich.Raise(new ContentsResizedEventArgs(Rectangle.Empty));
            Assert.Equal(1, laterCalls);
        });

    [Theory]
    [InlineData(BorderStyle.None, 31)]
    [InlineData(BorderStyle.FixedSingle, 31)]
    [InlineData(BorderStyle.Fixed3D, 32)]
    public void NativeResizeNotificationCopiesTheRectangleAndOnlyAdjustsTheFixed3DBottom(
        BorderStyle border, int expectedBottom)
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { BorderStyle = border };
            ContentsResizedEventArgs? received = null;
            rich.ContentsResized += (sender, arguments) =>
            {
                Assert.Same(rich, sender);
                received = arguments;
            };
            NativeRequestResize request = NewRequest(new NativeRectangle(-7, 11, 23, 31));
            WithRequest(request, pointer =>
            {
                Assert.True(rich.Notify(EnRequestResize, pointer));
                NativeRequestResize after = Marshal.PtrToStructure<NativeRequestResize>(pointer);
                Assert.Equal(request.Header.Window, after.Header.Window);
                Assert.Equal(request.Header.Id, after.Header.Id);
                Assert.Equal(request.Header.Code, after.Header.Code);
                Assert.Equal(new NativeRectangle(-7, 11, 23, expectedBottom), after.Rectangle);
                Assert.Equal(Rectangle.FromLTRB(-7, 11, 23, expectedBottom), received!.NewRectangle);
            });
            Assert.Equal(1, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void NativeResizeNotificationDispatchesTheVirtualRaiserWithoutSubscribers()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { BorderStyle = BorderStyle.None, InvokeBaseRaiser = false };
            WithRequest(NewRequest(new NativeRectangle(1, 2, 5, 7)), pointer =>
                Assert.True(rich.Notify(EnRequestResize, pointer)));
            Assert.Equal(1, rich.VirtualCalls);
            Assert.Equal(Rectangle.FromLTRB(1, 2, 5, 7), rich.LastArguments!.NewRectangle);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void UnknownNotificationDoesNotReadItsNullPayloadOrRaiseAResize()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox();
            Assert.False(rich.Notify(0x7FFF, 0));
            Assert.Equal(0, rich.VirtualCalls);
            Assert.False(rich.IsHandleCreated);
        });

    [Fact]
    public void NativeResizeHandlerFailureDoesNotUndoTheInPlaceBorderAdjustment()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { BorderStyle = BorderStyle.Fixed3D };
            var failure = new InvalidOperationException("native resize failure");
            rich.ContentsResized += (_, _) => throw failure;
            WithRequest(NewRequest(new NativeRectangle(0, 0, 20, 30)), pointer =>
            {
                Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Notify(EnRequestResize, pointer)));
                Assert.Equal(31, Marshal.PtrToStructure<NativeRequestResize>(pointer).Rectangle.Bottom);
            });
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RealRichEditResizeRequestsReachTheControlWithAndWithoutAForm(bool attachToForm)
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ResizeRichTextBox { Text = "resize request\nsecond line", Width = 240, Height = 120 };
            if (attachToForm) { form.Controls.Add(rich); }
            nint handle = rich.Handle;
            List<ContentsResizedEventArgs> notifications = [];
            rich.ContentsResized += (sender, arguments) =>
            {
                Assert.Same(rich, sender);
                notifications.Add(arguments);
            };
            string before = rich.Text;
            rich.Select(2, 4);
            bool modified = rich.Modified;

            _ = SendMessageW(handle, EmRequestResize, 0, 0);

            Assert.NotEmpty(notifications);
            Assert.All(notifications, arguments =>
            {
                Assert.True(arguments.NewRectangle.Width >= 0);
                Assert.True(arguments.NewRectangle.Height > 0);
            });
            Assert.Equal(before, rich.Text);
            Assert.Equal(2, rich.SelectionStart);
            Assert.Equal(4, rich.SelectionLength);
            Assert.Equal(modified, rich.Modified);
            Assert.Equal(handle, rich.Handle);
        });

    [Fact]
    public void CreationAndRecreationEnableTheNativeResizeEventMask()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { Text = "resize mask" };
            nint firstHandle = rich.Handle;
            Assert.Equal(EnmRequestResize, (int)SendMessageW(firstHandle, EmGetEventMask, 0, 0) & EnmRequestResize);
            rich.RecreateNow();
            Assert.True(rich.IsHandleCreated);
            Assert.Equal(EnmRequestResize, (int)SendMessageW(rich.Handle, EmGetEventMask, 0, 0) & EnmRequestResize);
            int calls = 0;
            rich.ContentsResized += (_, _) => calls++;
            _ = SendMessageW(rich.Handle, EmRequestResize, 0, 0);
            Assert.True(calls > 0);
            Assert.Equal("resize mask", rich.Text);
        });

    [Fact]
    public void ParkingResizeRejectsAMappedControlOwnedByADifferentReceiver()
        => OnSta(() =>
        {
            using var first = new ResizeRichTextBox { BorderStyle = BorderStyle.Fixed3D };
            using var second = new ResizeRichTextBox { BorderStyle = BorderStyle.Fixed3D };
            nint firstHandle = first.Handle;
            nint secondHandle = second.Handle;
            nint firstParking = GetParent(firstHandle);
            nint secondParking = GetParent(secondHandle);
            Assert.NotEqual((nint)0, firstParking);
            Assert.NotEqual(firstParking, secondParking);
            int firstCalls = 0;
            int secondCalls = 0;
            first.ContentsResized += (_, _) => firstCalls++;
            second.ContentsResized += (_, _) => secondCalls++;
            NativeRequestResize request = NewRequest(new NativeRectangle(1, 2, 30, 40));
            request.Header.Window = secondHandle;
            WithRequest(request, pointer =>
            {
                _ = SendMessageW(firstParking, 0x004E, (nint)request.Header.Id, pointer);
                Assert.Equal(40, Marshal.PtrToStructure<NativeRequestResize>(pointer).Rectangle.Bottom);
            });
            Assert.Equal(0, firstCalls);
            Assert.Equal(0, secondCalls);
            Assert.Equal(firstParking, GetParent(firstHandle));
            Assert.Equal(secondParking, GetParent(secondHandle));
        });

    [Fact]
    public void ParkingResizeRejectsAnUnmappedSourceAndANullNotification()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { BorderStyle = BorderStyle.Fixed3D };
            nint handle = rich.Handle;
            nint parking = GetParent(handle);
            Assert.NotEqual((nint)0, parking);
            Assert.Null(Control.FromHandle(parking));
            int calls = 0;
            rich.ContentsResized += (_, _) => calls++;
            NativeRequestResize request = NewRequest(new NativeRectangle(1, 2, 30, 40));
            request.Header.Window = parking;
            WithRequest(request, pointer =>
            {
                _ = SendMessageW(parking, 0x004E, (nint)request.Header.Id, pointer);
                Assert.Equal(40, Marshal.PtrToStructure<NativeRequestResize>(pointer).Rectangle.Bottom);
            });
            _ = SendMessageW(parking, 0x004E, 0, 0);
            Assert.Equal(0, calls);
            Assert.Equal(parking, GetParent(handle));
        });

    [Fact]
    public void ParkingResizeRoutesBySourceHandleWhenNotificationIdentifiersCollide()
        => OnSta(() =>
        {
            using var first = new ResizeRichTextBox { BorderStyle = BorderStyle.None };
            using var second = new ResizeRichTextBox { BorderStyle = BorderStyle.None };
            nint firstHandle = first.Handle;
            nint secondHandle = second.Handle;
            nint firstParking = GetParent(firstHandle);
            nint secondParking = GetParent(secondHandle);
            List<Rectangle> firstCalls = [];
            List<Rectangle> secondCalls = [];
            first.ContentsResized += (_, arguments) => firstCalls.Add(arguments.NewRectangle);
            second.ContentsResized += (_, arguments) => secondCalls.Add(arguments.NewRectangle);
            NativeRequestResize firstRequest = NewRequest(new NativeRectangle(1, 2, 30, 40));
            firstRequest.Header.Window = firstHandle;
            NativeRequestResize secondRequest = NewRequest(new NativeRectangle(5, 6, 70, 80));
            secondRequest.Header.Window = secondHandle;
            Assert.Equal(firstRequest.Header.Id, secondRequest.Header.Id);

            WithRequest(firstRequest, pointer =>
                _ = SendMessageW(firstParking, 0x004E, (nint)firstRequest.Header.Id, pointer));
            Assert.Equal([Rectangle.FromLTRB(1, 2, 30, 40)], firstCalls);
            Assert.Empty(secondCalls);
            WithRequest(secondRequest, pointer =>
                _ = SendMessageW(secondParking, 0x004E, (nint)secondRequest.Header.Id, pointer));
            Assert.Equal([Rectangle.FromLTRB(1, 2, 30, 40)], firstCalls);
            Assert.Equal([Rectangle.FromLTRB(5, 6, 70, 80)], secondCalls);
        });

    [Fact]
    public void ReparentedRichEditKeepsItsOwnOriginalParkingNotificationReceiver()
        => OnSta(() =>
        {
            using var form = new Form();
            using var rich = new ResizeRichTextBox { BorderStyle = BorderStyle.None, Text = "retained resize parent" };
            nint handle = rich.Handle;
            nint parking = GetParent(handle);
            Assert.NotEqual((nint)0, parking);
            _ = form.Handle;
            form.Controls.Add(rich);
            Assert.Equal(handle, rich.Handle);
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.NotEqual(parking, GetParent(handle));
            List<Rectangle> notifications = [];
            rich.ContentsResized += (_, arguments) => notifications.Add(arguments.NewRectangle);
            NativeRequestResize request = NewRequest(new NativeRectangle(1, 2, 30, 40));
            request.Header.Window = handle;

            WithRequest(request, pointer =>
                _ = SendMessageW(parking, 0x004E, (nint)request.Header.Id, pointer));

            Assert.Equal([Rectangle.FromLTRB(1, 2, 30, 40)], notifications);
            Assert.Equal(handle, rich.Handle);
            Assert.Equal(form.Handle, GetParent(handle));
        });

    [Fact]
    public void FontChangedCallbackSuppressesResizeUntilTheResolvedFontAssignmentFinishes()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { Text = "font resize", BorderStyle = BorderStyle.Fixed3D };
            _ = rich.Handle;
            using var font = new Font("Arial", 21, FontStyle.Bold);
            int fontCallbacks = 0;
            int resizeCallbacks = 0;
            rich.ContentsResized += (_, _) => resizeCallbacks++;
            rich.FontChanged += (_, _) =>
            {
                fontCallbacks++;
                int before = resizeCallbacks;
                int virtualBefore = rich.VirtualCalls;
                WithRequest(NewRequest(new NativeRectangle(1, 2, 30, 40)), pointer =>
                {
                    Assert.True(rich.Notify(EnRequestResize, pointer));
                    Assert.Equal(40, Marshal.PtrToStructure<NativeRequestResize>(pointer).Rectangle.Bottom);
                });
                Assert.Equal(before, resizeCallbacks);
                Assert.Equal(virtualBefore, rich.VirtualCalls);
            };

            rich.Font = font;

            Assert.Equal(1, fontCallbacks);
            int completedCalls = resizeCallbacks;
            WithRequest(NewRequest(new NativeRectangle(1, 2, 30, 40)), pointer =>
            {
                Assert.True(rich.Notify(EnRequestResize, pointer));
                Assert.Equal(41, Marshal.PtrToStructure<NativeRequestResize>(pointer).Rectangle.Bottom);
            });
            Assert.Equal(completedCalls + 1, resizeCallbacks);
        });

    [Fact]
    public void AFontChangedExceptionStillRestoresResizeNotification()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { Text = "font failure", BorderStyle = BorderStyle.Fixed3D };
            _ = rich.Handle;
            using var font = new Font("Arial", 22, FontStyle.Italic);
            var failure = new InvalidOperationException("font changed failure");
            EventHandler throwing = (_, _) => throw failure;
            rich.FontChanged += throwing;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => rich.Font = font));

            rich.FontChanged -= throwing;
            int calls = 0;
            rich.ContentsResized += (_, _) => calls++;
            WithRequest(NewRequest(new NativeRectangle(0, 0, 30, 40)), pointer =>
            {
                Assert.True(rich.Notify(EnRequestResize, pointer));
                Assert.Equal(41, Marshal.PtrToStructure<NativeRequestResize>(pointer).Rectangle.Bottom);
            });
            Assert.Equal(1, calls);
        });

    [Fact]
    public void EmptyDocumentFontAssignmentDoesNotSuppressResizeDuringFontChanged()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { BorderStyle = BorderStyle.None };
            _ = rich.Handle;
            using var font = new Font("Arial", 23);
            int fontCallbacks = 0;
            int resizeCallbacks = 0;
            rich.ContentsResized += (_, _) => resizeCallbacks++;
            rich.FontChanged += (_, _) =>
            {
                fontCallbacks++;
                int before = resizeCallbacks;
                WithRequest(NewRequest(new NativeRectangle(0, 0, 30, 40)), pointer =>
                    Assert.True(rich.Notify(EnRequestResize, pointer)));
                Assert.Equal(before + 1, resizeCallbacks);
            };

            rich.Font = font;

            Assert.Equal(1, fontCallbacks);
        });

    [Fact]
    public void NullFontAssignmentDoesNotSuppressResizeDuringTheResetNotification()
        => OnSta(() =>
        {
            using var rich = new ResizeRichTextBox { Text = "font reset", BorderStyle = BorderStyle.None };
            _ = rich.Handle;
            using var font = new Font("Arial", 24);
            rich.Font = font;
            int fontCallbacks = 0;
            int resizeCallbacks = 0;
            rich.ContentsResized += (_, _) => resizeCallbacks++;
            rich.FontChanged += (_, _) =>
            {
                fontCallbacks++;
                int before = resizeCallbacks;
                WithRequest(NewRequest(new NativeRectangle(0, 0, 30, 40)), pointer =>
                    Assert.True(rich.Notify(EnRequestResize, pointer)));
                Assert.Equal(before + 1, resizeCallbacks);
            };

            rich.Font = null;

            Assert.Equal(1, fontCallbacks);
        });

    private static NativeRequestResize NewRequest(NativeRectangle rectangle) => new()
    {
        Header = new NativeHeader { Window = (nint)0x1234, Id = (nuint)0x5678, Code = EnRequestResize },
        Rectangle = rectangle
    };

    private static void WithRequest(NativeRequestResize request, Action<nint> action)
    {
        nint pointer = Marshal.AllocHGlobal(Marshal.SizeOf<NativeRequestResize>());
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox ContentsResized test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ResizeRichTextBox : RichTextBox
    {
        internal bool EventsAllowed { get; set; } = true;
        internal bool InvokeBaseRaiser { get; set; } = true;
        internal int VirtualCalls { get; private set; }
        internal ContentsResizedEventArgs? LastArguments { get; private set; }
        protected override bool CanRaiseEvents => EventsAllowed;
        internal void Raise(ContentsResizedEventArgs? arguments) => OnContentsResized(arguments!);
        internal bool Notify(int code, nint payload) => base.OnNotify(code, payload);
        internal void RecreateNow() => RecreateHandle();

        protected override void OnContentsResized(ContentsResizedEventArgs e)
        {
            VirtualCalls++;
            LastArguments = e;
            if (InvokeBaseRaiser) { base.OnContentsResized(e); }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeHeader
    {
        internal nint Window;
        internal nuint Id;
        internal uint Code;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRequestResize
    {
        internal NativeHeader Header;
        internal NativeRectangle Rectangle;
    }

    [StructLayout(LayoutKind.Sequential)]
    private readonly record struct NativeRectangle(int Left, int Top, int Right, int Bottom);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint handle, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint handle);
}
