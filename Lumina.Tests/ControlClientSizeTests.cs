using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlClientSizeTests
{
    private static readonly Rectangle Initial = new(11, 13, 73, 37);

    [Theory]
    [InlineData("ClientSize", typeof(Size), true)]
    [InlineData("ClientRectangle", typeof(Rectangle), false)]
    public void ClientProperties_HaveStandardMetadata(string name, Type propertyType, bool writable)
    {
        PropertyInfo property = typeof(Control).GetProperty(name)!;
        Assert.Equal(propertyType, property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.False(property.GetMethod.IsVirtual);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(EditorBrowsableState.Advanced, property.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
        Assert.Equal(DesignerSerializationVisibility.Hidden, property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.Equal(writable, property.CanWrite);
        if (writable)
        {
            Assert.True(property.SetMethod!.IsPublic);
            Assert.False(property.SetMethod.IsVirtual);
            ParameterInfo parameter = Assert.Single(property.SetMethod.GetParameters());
            Assert.Equal("value", parameter.Name);
            Assert.Equal(typeof(Size), parameter.ParameterType);
        }
    }

    [Fact]
    public void ClientNotificationAndCore_HaveStandardPublicAndProtectedShape()
    {
        EventInfo notification = typeof(Control).GetEvent("ClientSizeChanged")!;
        Assert.Equal(typeof(EventHandler), notification.EventHandlerType);
        foreach (MethodInfo accessor in new[] { notification.AddMethod!, notification.RemoveMethod! })
        {
            Assert.True(accessor.IsPublic);
            Assert.False(accessor.IsVirtual);
            Assert.Equal(typeof(void), accessor.ReturnType);
            Assert.Equal("value", Assert.Single(accessor.GetParameters()).Name);
        }
        MethodInfo core = typeof(Control).GetMethod("SetClientSizeCore", BindingFlags.Instance | BindingFlags.NonPublic, [typeof(int), typeof(int)])!;
        MethodInfo hook = typeof(Control).GetMethod("OnClientSizeChanged", BindingFlags.Instance | BindingFlags.NonPublic, [typeof(EventArgs)])!;
        foreach (MethodInfo method in new[] { core, hook })
        {
            Assert.True(method.IsFamily);
            Assert.True(method.IsVirtual);
            Assert.False(method.IsFinal);
            Assert.Equal(typeof(void), method.ReturnType);
        }
        Assert.Equal(new[] { "x", "y" }, core.GetParameters().Select(parameter => parameter.Name));
        Assert.Equal("e", Assert.Single(hook.GetParameters()).Name);
    }

    [Theory]
    [InlineData("width", 83, 37, "Resize,SizeChanged,ClientSizeChanged")]
    [InlineData("height", 73, 47, "Resize,SizeChanged,ClientSizeChanged")]
    [InlineData("size", 83, 47, "Resize,SizeChanged,ClientSizeChanged")]
    [InlineData("bounds", 83, 47, "Move,LocationChanged,Resize,SizeChanged,ClientSizeChanged")]
    [InlineData("four", 83, 47, "Move,LocationChanged,Resize,SizeChanged,ClientSizeChanged")]
    [InlineData("specified", 83, 37, "Resize,SizeChanged,ClientSizeChanged")]
    [InlineData("direct-bounds", 83, 47, "Move,LocationChanged,Resize,SizeChanged,ClientSizeChanged")]
    [InlineData("client", 83, 47, "Resize,SizeChanged,ClientSizeChanged,ClientSizeChanged")]
    [InlineData("direct-client", 83, 47, "Resize,SizeChanged,ClientSizeChanged,ClientSizeChanged")]
    public void HandlelessEntryPoints_CommitClientRectangleBeforeIndependentNotifications(string entry, int width, int height, string events)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            void Apply()
            {
                switch (entry)
                {
                    case "width": control.Width = 83; break;
                    case "height": control.Height = 47; break;
                    case "size": control.Size = new Size(83, 47); break;
                    case "bounds": control.Bounds = new Rectangle(23, 29, 83, 47); break;
                    case "four": control.SetBounds(23, 29, 83, 47); break;
                    case "specified": control.SetBounds(23, 29, 83, 47, BoundsSpecified.Width); break;
                    case "direct-bounds": control.CallBoundsCore(new Rectangle(23, 29, 83, 47)); break;
                    case "client": control.ClientSize = new Size(83, 47); break;
                    case "direct-client": control.CallClientCore(83, 47); break;
                }
            }
            Apply();
            var expected = new Size(width, height);
            Assert.Equal(expected, control.ClientSize);
            Assert.Equal(new Rectangle(Point.Empty, expected), control.ClientRectangle);
            Assert.Equal(events.Split(','), control.Events.Select(call => call.Name));
            Assert.All(control.Events, call =>
            {
                Assert.Equal(expected, call.ClientSize);
                Assert.Equal(control.Bounds, call.Bounds);
                Assert.Same(control, call.Sender);
                Assert.Same(EventArgs.Empty, call.Args);
            });
            Assert.False(control.IsHandleCreated);
            control.Reset();
            Apply();
            if (entry is "client" or "direct-client") { Assert.Equal("ClientSizeChanged", Assert.Single(control.Events).Name); }
            else { Assert.Empty(control.Events); }
        });

    [Theory]
    [InlineData("none", 0)]
    [InlineData("border", 2)]
    [InlineData("edge", 4)]
    [InlineData("both", 6)]
    public void HandlelessBorders_AdjustOuterSizeAndPreserveNegativeClientRequests(string border, int nonClient)
        => OnSta(() =>
        {
            using TraceControl control = Create(border: border);
            Assert.Equal(new Size(73 - nonClient, 37 - nonClient), control.ClientSize);
            control.ClientSize = new Size(83, 47);
            Assert.Equal(new Size(83 + nonClient, 47 + nonClient), control.Size);
            Assert.Equal(new Size(83, 47), control.ClientSize);
            control.ClientSize = new Size(-3, -5);
            Assert.Equal(new Size(-3 + nonClient, -5 + nonClient), control.Size);
            Assert.Equal(new Size(-3, -5), control.ClientSize);
            control.Size = Size.Empty;
            Assert.Equal(new Size(-nonClient, -nonClient), control.ClientSize);
            control.Size = Initial.Size;
            Assert.Equal(new Size(73 - nonClient, 37 - nonClient), control.ClientSize);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData("none", 0, 0, 0)]
    [InlineData("border", 2, 0, 0)]
    [InlineData("edge", 4, 1, 0)]
    [InlineData("both", 6, 1, 0)]
    [Trait("Category", "WindowsHandle")]
    public void NativeBorders_MatchRealClientRectAndKeepRequestedNegativeCache(string border, int nonClient, int negativeNativeWidth, int negativeNativeHeight)
        => OnSta(() =>
        {
            using var form = new Form();
            using TraceControl control = Create(form, border);
            nint handle = control.Handle;
            Assert.Equal(new Size(73 - nonClient, 37 - nonClient), control.ClientSize);
            AssertNativeClient(control, control.ClientSize);
            control.ClientSize = new Size(83, 47);
            Assert.Equal(new Size(83 + nonClient, 47 + nonClient), control.Size);
            AssertNativeClient(control, new Size(83, 47));
            control.ClientSize = new Size(-3, -5);
            Assert.Equal(new Size(Math.Max(0, -3 + nonClient), Math.Max(0, -5 + nonClient)), control.Size);
            Assert.Equal(new Size(-3, -5), control.ClientSize);
            AssertNativeClient(control, new Size(negativeNativeWidth, negativeNativeHeight));
            control.Reset();
            control.ClientSize = new Size(-3, -5);
            Assert.Equal("ClientSizeChanged", Assert.Single(control.Events).Name);
            Assert.Equal(new Size(-3, -5), control.ClientSize);
            control.Size = Initial.Size;
            Assert.Equal(new Size(73 - nonClient, 37 - nonClient), control.ClientSize);
            AssertNativeClient(control, control.ClientSize);
            Assert.Equal(handle, control.Handle);
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-3, -5)]
    [InlineData(-3, 37)]
    [InlineData(73, -5)]
    [InlineData(int.MinValue, -1)]
    public void HandlelessClientCore_KeepsOriginalDimensionsAndCanRestore(int width, int height)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            control.CallClientCore(width, height);
            Assert.Equal(new Size(width, height), control.Size);
            Assert.Equal(new Size(width, height), control.ClientSize);
            Assert.Equal(new Rectangle(0, 0, width, height), control.ClientRectangle);
            control.Reset();
            control.CallClientCore(width, height);
            Assert.Equal("ClientSizeChanged", Assert.Single(control.Events).Name);
            control.Size = Initial.Size;
            Assert.Equal(Initial.Size, control.ClientSize);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData("SetClientSizeCore", "", 73, 37, 73, 37)]
    [InlineData("SetBoundsCore", "ClientSizeChanged", 73, 37, 83, 47)]
    [InlineData("SizeChanged", "ClientSizeChanged,ClientSizeChanged", 83, 47, 83, 47)]
    [InlineData("Resize", "SizeChanged,ClientSizeChanged,ClientSizeChanged", 83, 47, 83, 47)]
    [InlineData("ClientSizeChanged", "Resize,SizeChanged", 83, 47, 83, 47)]
    public void OverridesWithoutBase_KeepIndependentClientAndOuterContracts(string skipped, string events, int width, int height, int clientWidth, int clientHeight)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            control.Skip = skipped;
            control.ClientSize = new Size(83, 47);
            Assert.Equal(new Size(width, height), control.Size);
            Assert.Equal(new Size(clientWidth, clientHeight), control.ClientSize);
            Assert.Equal(Split(events), control.Events.Select(call => call.Name));
        });

    [Theory]
    [InlineData("empty")]
    [InlineData("custom")]
    [InlineData("null")]
    public void DirectClientNotification_PassesArgumentsAndDoesNotChangeRectangles(string argument)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            EventArgs? args = argument == "empty" ? EventArgs.Empty : argument == "custom" ? new EventArgs() : null;
            control.CallNotification(args!);
            EventCall call = Assert.Single(control.Events);
            Assert.Same(args, call.Args);
            Assert.Equal("ClientSizeChanged", call.Name);
            Assert.Equal(Initial, call.Bounds);
            Assert.Equal(Initial.Size, call.ClientSize);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData("enter:SizeChanged", false)]
    [InlineData("event:Resize", false)]
    [InlineData("event:SizeChanged", false)]
    [InlineData("enter:ClientSizeChanged", false)]
    [InlineData("event:ClientSizeChanged", false)]
    [InlineData("enter:SizeChanged", true)]
    [InlineData("event:Resize", true)]
    [InlineData("event:SizeChanged", true)]
    [InlineData("enter:ClientSizeChanged", true)]
    [InlineData("event:ClientSizeChanged", true)]
    public void ReentrantSizeChange_CommitsFinalOuterSizeThenOriginalClientRequest(string point, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            control.ActionPoint = point;
            control.AtPoint = () => control.Size = new Size(97, 61);
            control.ClientSize = new Size(83, 47);
            Assert.Equal(new Size(97, 61), control.Size);
            Assert.Equal(new Size(83, 47), control.ClientSize);
            Assert.Equal(new Size(83, 47), control.Events[^1].ClientSize);
            Assert.Equal("ClientSizeChanged", control.Events[^1].Name);
            Assert.Contains(control.Events, call => call.ClientSize == new Size(97, 61));
            if (native) { AssertNativeClient(control, new Size(97, 61)); }
            else { Assert.False(control.IsHandleCreated); }
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void MovingAfterReentrantClientCacheChange_RestoresComputedClientSizeAndNotifiesBothFamilies(bool native, bool specified)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            control.ActionPoint = "event:SizeChanged";
            control.AtPoint = () => control.Size = new Size(97, 61);
            control.ClientSize = new Size(83, 47);
            control.Reset();
            if (specified) { control.SetBounds(23, 199, 211, 223, BoundsSpecified.X); }
            else { control.Location = new Point(23, 29); }
            Assert.Equal(new Size(97, 61), control.ClientSize);
            Assert.Equal(new[] { "Move", "LocationChanged", "Resize", "SizeChanged", "ClientSizeChanged" }, control.Events.Select(call => call.Name));
            Assert.All(control.Events, call => Assert.Equal(new Size(97, 61), call.ClientSize));
            if (native) { AssertNativeClient(control, new Size(97, 61)); }
        });

    [Theory]
    [InlineData("none", "location", 0)]
    [InlineData("none", "specified", 0)]
    [InlineData("none", "core", 0)]
    [InlineData("border", "location", 0)]
    [InlineData("border", "specified", 0)]
    [InlineData("border", "core", 0)]
    [InlineData("edge", "location", 1)]
    [InlineData("edge", "specified", 1)]
    [InlineData("edge", "core", 1)]
    [InlineData("both", "location", 1)]
    [InlineData("both", "specified", 1)]
    [InlineData("both", "core", 1)]
    [Trait("Category", "WindowsHandle")]
    public void NativeMoveAfterNegativeClientRequest_RefreshesActualClientCacheAndNotifiesSize(string border, string entry, int expectedClientWidth)
        => OnSta(() =>
        {
            using var form = new Form();
            using TraceControl control = Create(form, border);
            control.ClientSize = new Size(-3, -5);
            Size outer = control.Size;
            control.Reset();
            if (entry == "location") { control.Location = new Point(23, 29); }
            else if (entry == "specified") { control.SetBounds(23, 199, 211, 223, BoundsSpecified.X); }
            else { control.CallBoundsCore(new Rectangle(23, 29, outer.Width, outer.Height)); }
            Assert.Equal(outer, control.Size);
            var expectedClient = new Size(expectedClientWidth, 0);
            Assert.Equal(expectedClient, control.ClientSize);
            AssertNativeClient(control, expectedClient);
            Assert.Equal(new[] { "Move", "LocationChanged", "Resize", "SizeChanged", "ClientSizeChanged" }, control.Events.Select(call => call.Name));
            Assert.All(control.Events, call => Assert.Equal(expectedClient, call.ClientSize));
        });

    [Theory]
    [InlineData("border", 0, 0)]
    [InlineData("edge", 1, 1)]
    [InlineData("both", 0, 0)]
    [Trait("Category", "WindowsHandle")]
    public void NativeSmallOuterSizeChange_UsesActualBorderBehaviorAndAlwaysNotifiesClient(string border, int clientWidth, int clientHeight)
        => OnSta(() =>
        {
            using var form = new Form();
            using TraceControl control = Create(form, border);
            control.Size = Size.Empty;
            control.Reset();
            control.Size = new Size(1, 1);
            var expectedClient = new Size(clientWidth, clientHeight);
            Assert.Equal(expectedClient, control.ClientSize);
            AssertNativeClient(control, expectedClient);
            Assert.Equal(new[] { "Resize", "SizeChanged", "ClientSizeChanged" }, control.Events.Select(call => call.Name));
        });

    [Theory]
    [InlineData("enter:SetClientSizeCore", false)]
    [InlineData("enter:SetBoundsCore", false)]
    [InlineData("enter:SetClientSizeCore", true)]
    [InlineData("enter:SetBoundsCore", true)]
    public void OverriddenCoreFailure_PropagatesSynchronouslyBeforeChangingEitherRectangle(string point, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            var failure = new InvalidOperationException("overridden client core");
            control.ActionPoint = point;
            control.AtPoint = () => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.ClientSize = new Size(83, 47)));
            Assert.Equal(Initial, control.Bounds);
            Assert.Equal(Initial.Size, control.ClientSize);
            Assert.Empty(control.Events);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DirectProtectedNotificationFailure_RemainsSynchronousWithOrWithoutHandle(bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            var failure = new InvalidOperationException("direct client notification");
            control.ActionPoint = "event:ClientSizeChanged";
            control.AtPoint = () => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.CallNotification(EventArgs.Empty)));
            Assert.Equal(Initial, control.Bounds);
            Assert.Equal(Initial.Size, control.ClientSize);
        });

    [Theory]
    [InlineData("enter:SizeChanged", "")]
    [InlineData("enter:Resize", "")]
    [InlineData("event:Resize", "Resize")]
    [InlineData("event:SizeChanged", "Resize,SizeChanged")]
    [InlineData("enter:ClientSizeChanged", "Resize,SizeChanged")]
    [InlineData("event:ClientSizeChanged", "Resize,SizeChanged,ClientSizeChanged")]
    public void HandlelessExceptions_PropagateOriginalFailureAfterCommittingClientRectangle(string point, string events)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            var failure = new InvalidOperationException("client test");
            control.ActionPoint = point;
            control.AtPoint = () => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.ClientSize = new Size(83, 47)));
            Assert.Equal(new Size(83, 47), control.Size);
            Assert.Equal(new Size(83, 47), control.ClientSize);
            Assert.Equal(Split(events), control.Events.Select(call => call.Name));
            control.Reset();
            control.ClientSize = new Size(83, 47);
            Assert.Equal("ClientSizeChanged", Assert.Single(control.Events).Name);
        });

    [Theory]
    [InlineData("enter:SizeChanged")]
    [InlineData("enter:Resize")]
    [InlineData("event:Resize")]
    [InlineData("event:SizeChanged")]
    [InlineData("enter:ClientSizeChanged")]
    [InlineData("event:ClientSizeChanged")]
    [Trait("Category", "WindowsHandle")]
    public void NativeBoundsExceptions_UseThreadExceptionThenFinishExplicitClientNotification(string point)
        => OnSta(() =>
        {
            using var form = new Form();
            using TraceControl control = Create(form);
            var failure = new InvalidOperationException("native client test");
            var delivered = new List<Exception>();
            void Handler(object sender, ThreadExceptionEventArgs e) => delivered.Add(e.Exception);
            Application.ThreadException += Handler;
            try
            {
                control.ActionPoint = point;
                control.AtPoint = () => throw failure;
                control.ClientSize = new Size(83, 47);
                Assert.Same(failure, Assert.Single(delivered));
                Assert.Equal(new Size(83, 47), control.ClientSize);
                Assert.Equal("ClientSizeChanged", control.Events[^1].Name);
                AssertNativeClient(control, new Size(83, 47));
                control.ActionPoint = "event:ClientSizeChanged";
                control.AtPoint = () => throw failure;
                Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.ClientSize = control.ClientSize));
                Assert.Single(delivered);
            }
            finally { Application.ThreadException -= Handler; }
        });

    private static string[] Split(string value) => value.Length == 0 ? [] : value.Split(',');

    private static TraceControl Create(Form? form = null, string border = "none")
    {
        var control = new TraceControl(border) { Bounds = Initial };
        if (form is not null)
        {
            form.Controls.Add(control);
            form.Show();
            Assert.NotEqual(0, control.Handle);
        }
        control.Reset();
        return control;
    }

    private static void AssertNativeClient(Control control, Size size)
    {
        Assert.True(GetClientRect(control.Handle, out NativeRect rectangle));
        Assert.Equal(new Rectangle(Point.Empty, size), new Rectangle(rectangle.Left, rectangle.Top, rectangle.Right - rectangle.Left, rectangle.Bottom - rectangle.Top));
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The client size check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct EventCall(string Name, object? Sender, EventArgs? Args, Rectangle Bounds, Size ClientSize);

    private sealed class TraceControl : Control
    {
        internal new readonly List<EventCall> Events = [];
        private readonly string _border;
        internal string? Skip;
        internal string? ActionPoint;
        internal Action? AtPoint;
        protected override uint Style => base.Style | (_border is "border" or "both" ? 0x00800000u : 0u);
        protected override uint ExStyle => base.ExStyle | (_border is "edge" or "both" ? 0x00000200u : 0u);

        internal TraceControl(string border)
        {
            _border = border;
            Move += (sender, args) => Event("Move", sender, args);
            LocationChanged += (sender, args) => Event("LocationChanged", sender, args);
            Resize += (sender, args) => Event("Resize", sender, args);
            SizeChanged += (sender, args) => Event("SizeChanged", sender, args);
            ClientSizeChanged += (sender, args) => Event("ClientSizeChanged", sender, args);
        }

        internal void Reset() => Events.Clear();
        internal void CallClientCore(int x, int y) => SetClientSizeCore(x, y);
        internal void CallBoundsCore(Rectangle rectangle) => SetBoundsCore(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, BoundsSpecified.None);
        internal void CallNotification(EventArgs e) => OnClientSizeChanged(e);

        protected override void SetClientSizeCore(int x, int y)
        {
            Point("enter:SetClientSizeCore");
            if (Skip != "SetClientSizeCore") { base.SetClientSizeCore(x, y); }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Point("enter:SetBoundsCore");
            if (Skip != "SetBoundsCore") { base.SetBoundsCore(x, y, width, height, specified); }
        }

        protected override void OnResize(EventArgs e)
        {
            Point("enter:Resize");
            if (Skip != "Resize") { base.OnResize(e); }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            Point("enter:SizeChanged");
            if (Skip != "SizeChanged") { base.OnSizeChanged(e); }
        }
        protected override void OnClientSizeChanged(EventArgs e)
        {
            Point("enter:ClientSizeChanged");
            if (Skip != "ClientSizeChanged") { base.OnClientSizeChanged(e); }
        }
        protected override void OnBoundsChanged() { }

        private void Event(string name, object? sender, EventArgs? args)
        {
            Events?.Add(new EventCall(name, sender, args, Bounds, ClientSize));
            Point("event:" + name);
        }
        private void Point(string point)
        {
            if (ActionPoint == point)
            {
                Action? action = AtPoint;
                ActionPoint = null;
                AtPoint = null;
                action?.Invoke();
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect { public int Left; public int Top; public int Right; public int Bottom; }
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetClientRect(nint handle, out NativeRect rectangle);
}
