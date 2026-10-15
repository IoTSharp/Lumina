using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlBoundsNotificationsTests
{
    private static readonly Rectangle Initial = new(11, 13, 73, 37);
    private static readonly Rectangle Changed = new(23, 29, 83, 47);
    private static readonly Rectangle Reentered = new(31, 41, 97, 61);
    private static readonly string[] LocationCalls = ["enter:LocationChanged", "enter:Move", "event:Move", "exit:Move", "event:LocationChanged", "exit:LocationChanged"];
    private static readonly string[] SizeCalls = ["enter:SizeChanged", "enter:Resize", "event:Resize", "exit:Resize", "event:SizeChanged", "exit:SizeChanged"];

    [Theory]
    [InlineData("LocationChanged", EditorBrowsableState.Always)]
    [InlineData("Move", EditorBrowsableState.Always)]
    [InlineData("SizeChanged", EditorBrowsableState.Always)]
    [InlineData("Resize", EditorBrowsableState.Advanced)]
    public void Notifications_HaveStandardEventAndProtectedVirtualSignatures(string name, EditorBrowsableState editorState)
    {
        EventInfo notification = typeof(Control).GetEvent(name)!;
        Assert.NotNull(notification);
        Assert.Equal(typeof(EventHandler), notification.EventHandlerType);
        foreach (MethodInfo accessor in new[] { notification.AddMethod!, notification.RemoveMethod! })
        {
            Assert.True(accessor.IsPublic);
            Assert.False(accessor.IsVirtual);
            Assert.False(accessor.IsFinal);
            Assert.Equal(typeof(void), accessor.ReturnType);
            ParameterInfo parameter = Assert.Single(accessor.GetParameters());
            Assert.Equal(typeof(EventHandler), parameter.ParameterType);
            Assert.Equal("value", parameter.Name);
        }
        Assert.True(notification.GetCustomAttribute<BrowsableAttribute>()?.Browsable ?? true);
        Assert.Equal(editorState, notification.GetCustomAttribute<EditorBrowsableAttribute>()?.State ?? EditorBrowsableState.Always);
        MethodInfo hook = typeof(Control).GetMethod("On" + name, BindingFlags.Instance | BindingFlags.NonPublic, [typeof(EventArgs)])!;
        Assert.NotNull(hook);
        Assert.True(hook.IsFamily);
        Assert.True(hook.IsVirtual);
        Assert.False(hook.IsFinal);
        Assert.Equal(typeof(void), hook.ReturnType);
        Assert.Equal("e", Assert.Single(hook.GetParameters()).Name);
    }

    [Theory]
    [InlineData("left", 23, 13, 73, 37)]
    [InlineData("top", 11, 29, 73, 37)]
    [InlineData("location", 23, 29, 73, 37)]
    [InlineData("width", 11, 13, 83, 37)]
    [InlineData("height", 11, 13, 73, 47)]
    [InlineData("size", 11, 13, 83, 47)]
    [InlineData("bounds", 23, 29, 83, 47)]
    [InlineData("four", 23, 29, 83, 47)]
    [InlineData("five", 23, 29, 83, 47)]
    public void StandardEntryPoints_NotifyOnlyChangedFamiliesAfterCommittingTheWholeRectangle(string entry, int x, int y, int width, int height)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            var expected = new Rectangle(x, y, width, height);
            Apply();
            AssertNotifications(control, expected, expected.Location != Initial.Location, expected.Size != Initial.Size);
            Assert.Equal(1, control.LegacyChanges);
            control.Reset();
            Apply();
            Assert.Empty(control.Calls);
            Assert.Empty(control.Events);
            Assert.Equal(0, control.LegacyChanges);
            Assert.False(control.IsHandleCreated);

            void Apply()
            {
                switch (entry)
                {
                    case "left": control.Left = 23; break;
                    case "top": control.Top = 29; break;
                    case "location": control.Location = new Point(23, 29); break;
                    case "width": control.Width = 83; break;
                    case "height": control.Height = 47; break;
                    case "size": control.Size = new Size(83, 47); break;
                    case "bounds": control.Bounds = Changed; break;
                    case "four": control.SetBounds(23, 29, 83, 47); break;
                    case "five": control.SetBounds(23, 29, 83, 47, BoundsSpecified.All); break;
                }
            }
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(14)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(31)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void SpecifiedBounds_NotifyFromEffectiveChangesAndIgnoreUnspecifiedOrUnknownComponents(int mask)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            var expected = new Rectangle((mask & 1) != 0 ? 23 : 11, (mask & 2) != 0 ? 29 : 13,
                (mask & 4) != 0 ? 83 : 73, (mask & 8) != 0 ? 47 : 37);
            control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask);
            AssertNotifications(control, expected, (mask & 3) != 0, (mask & 12) != 0);
            control.Reset();
            control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask);
            control.SetBounds((mask & 1) == 0 ? 199 : expected.X, (mask & 2) == 0 ? 211 : expected.Y,
                (mask & 4) == 0 ? 223 : expected.Width, (mask & 8) == 0 ? 227 : expected.Height, (BoundsSpecified)mask);
            Assert.Empty(control.Calls);
            Assert.Empty(control.Events);
            Assert.Equal(expected, control.Bounds);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(-1)]
    public void DirectCore_NotifiesForItsCompleteRectangleRegardlessOfSpecified(int mask)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            control.CallCore(Changed, (BoundsSpecified)mask);
            AssertNotifications(control, Changed, location: true, size: true);
            control.Reset();
            control.CallCore(Changed, (BoundsSpecified)mask);
            Assert.Empty(control.Calls);
            Assert.Empty(control.Events);
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-3, -5)]
    [InlineData(-3, 37)]
    [InlineData(73, -5)]
    [InlineData(int.MinValue, -1)]
    public void HandlelessDimensions_NotifyForNegativeRequestsWithoutCreatingHandle(int width, int height)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            control.Size = new Size(width, height);
            AssertNotifications(control, new Rectangle(11, 13, width, height), location: false, size: true);
            Assert.False(control.IsHandleCreated);
            control.Reset();
            control.Size = new Size(width, height);
            Assert.Empty(control.Calls);
            control.Size = Initial.Size;
            AssertNotifications(control, Initial, location: false, size: true);
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-3, -5)]
    [InlineData(-3, 37)]
    [InlineData(73, -5)]
    [InlineData(int.MinValue, -1)]
    [Trait("Category", "WindowsHandle")]
    public void NativeDimensions_NotifyOnceForNormalizedSizeAndCanRestore(int width, int height)
        => OnSta(() =>
        {
            using var form = new Form();
            using TraceControl control = Create(form);
            nint handle = control.Handle;
            var expected = new Rectangle(11, 13, Math.Max(0, width), Math.Max(0, height));
            control.Size = new Size(width, height);
            AssertNotifications(control, expected, location: false, size: true);
            AssertNativeBounds(form, control, expected);
            control.Reset();
            control.Size = new Size(width, height);
            Assert.Empty(control.Calls);
            Assert.Empty(control.Events);
            control.Size = Initial.Size;
            AssertNotifications(control, Initial, location: false, size: true);
            AssertNativeBounds(form, control, Initial);
            Assert.Equal(handle, control.Handle);
        });

    [Theory]
    [InlineData("LocationChanged", "Resize,SizeChanged")]
    [InlineData("Move", "LocationChanged,Resize,SizeChanged")]
    [InlineData("SizeChanged", "Move,LocationChanged")]
    [InlineData("Resize", "Move,LocationChanged,SizeChanged")]
    public void OverrideWithoutBase_SuppressesOnlyItsStandardNotificationChain(string skipped, string eventNames)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            control.Skip = skipped;
            control.Bounds = Changed;
            Assert.Equal(Changed, control.Bounds);
            Assert.Equal(eventNames.Split(','), control.Events.Select(notification => notification.Name));
            Assert.Equal(1, control.LegacyChanges);
        });

    [Theory]
    [InlineData("LocationChanged", "Move,LocationChanged", "empty")]
    [InlineData("LocationChanged", "Move,LocationChanged", "custom")]
    [InlineData("LocationChanged", "Move,LocationChanged", "null")]
    [InlineData("Move", "Move", "empty")]
    [InlineData("Move", "Move", "custom")]
    [InlineData("Move", "Move", "null")]
    [InlineData("SizeChanged", "Resize,SizeChanged", "empty")]
    [InlineData("SizeChanged", "Resize,SizeChanged", "custom")]
    [InlineData("SizeChanged", "Resize,SizeChanged", "null")]
    [InlineData("Resize", "Resize", "empty")]
    [InlineData("Resize", "Resize", "custom")]
    [InlineData("Resize", "Resize", "null")]
    public void ProtectedHooks_PassTheOriginalArgumentsWithoutChangingBounds(string hook, string expectedEvents, string argument)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            EventArgs? args = argument == "empty" ? EventArgs.Empty : argument == "custom" ? new EventArgs() : null;
            control.CallNotification(hook, args!);
            Assert.Equal(expectedEvents.Split(','), control.Events.Select(notification => notification.Name));
            Assert.All(control.Events, notification =>
            {
                Assert.Same(control, notification.Sender);
                EventArgs? expectedArgs = hook == "SizeChanged" && notification.Name == "Resize" ? EventArgs.Empty : args;
                Assert.Same(expectedArgs, notification.Args);
                Assert.Equal(Initial, notification.Bounds);
            });
            Assert.Equal(Initial, control.Bounds);
            Assert.Equal(0, control.LegacyChanges);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData("enter:LocationChanged", "")]
    [InlineData("enter:Move", "")]
    [InlineData("event:Move", "Move")]
    [InlineData("event:LocationChanged", "Move,LocationChanged")]
    [InlineData("enter:SizeChanged", "Move,LocationChanged")]
    [InlineData("enter:Resize", "Move,LocationChanged")]
    [InlineData("event:Resize", "Move,LocationChanged,Resize")]
    [InlineData("event:SizeChanged", "Move,LocationChanged,Resize,SizeChanged")]
    public void HandlelessExceptions_PropagateOriginalFailureAfterCommittingBoundsAndStopRemainingNotifications(string point, string events)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            var failure = new InvalidOperationException("notification test");
            control.ActionPoint = point;
            control.AtPoint = () => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.Bounds = Changed));
            Assert.Equal(Changed, control.Bounds);
            Assert.Equal(SplitEvents(events), control.Events.Select(notification => notification.Name));
            control.Reset();
            control.Bounds = Changed;
            Assert.Empty(control.Calls);
            control.Bounds = Initial;
            AssertNotifications(control, Initial, location: true, size: true);
        });

    [Theory]
    [InlineData("enter:LocationChanged", "")]
    [InlineData("enter:Move", "")]
    [InlineData("event:Move", "Move")]
    [InlineData("event:LocationChanged", "Move,LocationChanged")]
    [InlineData("enter:SizeChanged", "Move,LocationChanged")]
    [InlineData("enter:Resize", "Move,LocationChanged")]
    [InlineData("event:Resize", "Move,LocationChanged,Resize")]
    [InlineData("event:SizeChanged", "Move,LocationChanged,Resize,SizeChanged")]
    [Trait("Category", "WindowsHandle")]
    public void NativeExceptions_RouteOriginalFailureToThreadExceptionAndLeaveCommittedBounds(string point, string events)
        => OnSta(() =>
        {
            using var form = new Form();
            using TraceControl control = Create(form);
            var failure = new InvalidOperationException("native notification test");
            var delivered = new List<Exception>();
            void OnException(object sender, ThreadExceptionEventArgs e) => delivered.Add(e.Exception);
            Application.ThreadException += OnException;
            try
            {
                control.ActionPoint = point;
                control.AtPoint = () => throw failure;
                control.Bounds = Changed;
                Assert.Same(failure, Assert.Single(delivered));
                Assert.Equal(Changed, control.Bounds);
                AssertNativeBounds(form, control, Changed);
                Assert.Equal(SplitEvents(events), control.Events.Select(notification => notification.Name));
                control.Reset();
                control.Bounds = Changed;
                Assert.Empty(control.Calls);
                Assert.Single(delivered);
                control.Bounds = Initial;
                AssertNotifications(control, Initial, location: true, size: true);
                Assert.Single(delivered);
            }
            finally { Application.ThreadException -= OnException; }
        });

    [Theory]
    [InlineData("LocationChanged")]
    [InlineData("Move")]
    [InlineData("SizeChanged")]
    [InlineData("Resize")]
    [Trait("Category", "WindowsHandle")]
    public void DirectProtectedHook_WithHandleStillPropagatesOriginalFailureSynchronously(string hook)
        => OnSta(() =>
        {
            using var form = new Form();
            using TraceControl control = Create(form);
            var failure = new InvalidOperationException("direct hook test");
            control.ActionPoint = "event:" + hook;
            control.AtPoint = () => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.CallNotification(hook, EventArgs.Empty)));
            Assert.Equal(Initial, control.Bounds);
            Assert.Equal(0, control.LegacyChanges);
        });

    [Theory]
    [InlineData("enter:LocationChanged", "Move,LocationChanged,Resize,SizeChanged,Move,LocationChanged,Resize,SizeChanged", false)]
    [InlineData("event:Move", "Move,Move,LocationChanged,Resize,SizeChanged,LocationChanged,Resize,SizeChanged", false)]
    [InlineData("event:LocationChanged", "Move,LocationChanged,Move,LocationChanged,Resize,SizeChanged,Resize,SizeChanged", false)]
    [InlineData("enter:SizeChanged", "Move,LocationChanged,Move,LocationChanged,Resize,SizeChanged,Resize,SizeChanged", false)]
    [InlineData("event:Resize", "Move,LocationChanged,Resize,Move,LocationChanged,Resize,SizeChanged,SizeChanged", false)]
    [InlineData("event:SizeChanged", "Move,LocationChanged,Resize,SizeChanged,Move,LocationChanged,Resize,SizeChanged", false)]
    [InlineData("enter:LocationChanged", "Move,LocationChanged,Resize,SizeChanged,Move,LocationChanged,Resize,SizeChanged", true)]
    [InlineData("event:Move", "Move,Move,LocationChanged,Resize,SizeChanged,LocationChanged,Resize,SizeChanged", true)]
    [InlineData("event:LocationChanged", "Move,LocationChanged,Move,LocationChanged,Resize,SizeChanged,Resize,SizeChanged", true)]
    [InlineData("enter:SizeChanged", "Move,LocationChanged,Move,LocationChanged,Resize,SizeChanged,Resize,SizeChanged", true)]
    [InlineData("event:Resize", "Move,LocationChanged,Resize,Move,LocationChanged,Resize,SizeChanged,SizeChanged", true)]
    [InlineData("event:SizeChanged", "Move,LocationChanged,Resize,SizeChanged,Move,LocationChanged,Resize,SizeChanged", true)]
    public void ReentrantBounds_KeepOuterPendingNotificationsAndExposeCurrentRectangle(string point, string events, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            control.ActionPoint = point;
            control.AtPoint = () => control.Bounds = Reentered;
            control.Bounds = Changed;
            Assert.Equal(Reentered, control.Bounds);
            Assert.Equal(events.Split(','), control.Events.Select(notification => notification.Name));
            Assert.Equal(2, control.LegacyChanges);
            int reentry = control.Events.FindIndex(notification => notification.Bounds == Reentered);
            Assert.True(reentry >= 0);
            Assert.All(control.Events.Skip(reentry), notification => Assert.Equal(Reentered, notification.Bounds));
            if (form is not null) { AssertNativeBounds(form, control, Reentered); }
            else { Assert.False(control.IsHandleCreated); }
        });

    private static string[] SplitEvents(string events) => events.Length == 0 ? [] : events.Split(',');

    private static TraceControl Create(Form? form = null)
    {
        var control = new TraceControl { Bounds = Initial };
        if (form is not null)
        {
            form.Controls.Add(control);
            form.Show();
            Assert.NotEqual(0, control.Handle);
        }
        control.Reset();
        return control;
    }

    private static void AssertNotifications(TraceControl control, Rectangle expected, bool location, bool size)
    {
        Assert.Equal(expected, control.Bounds);
        string[] calls = [.. (location ? LocationCalls : []), .. (size ? SizeCalls : [])];
        Assert.Equal(calls, control.Calls);
        Assert.Equal((location ? 2 : 0) + (size ? 2 : 0), control.Events.Count);
        Assert.All(control.Events, notification =>
        {
            Assert.Same(control, notification.Sender);
            Assert.Same(EventArgs.Empty, notification.Args);
            Assert.Equal(expected, notification.Bounds);
        });
    }

    private static void AssertNativeBounds(Form form, Control control, Rectangle expected)
    {
        Assert.True(GetWindowRect(control.Handle, out NativeRect bounds));
        var point = new NativePoint { X = bounds.Left, Y = bounds.Top };
        Assert.True(ScreenToClient(form.Handle, ref point));
        Assert.Equal(expected, new Rectangle(point.X, point.Y, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top));
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The bounds notification check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct EventCall(string Name, object? Sender, EventArgs? Args, Rectangle Bounds);

    private sealed class TraceControl : Control
    {
        internal readonly List<string> Calls = [];
        internal readonly List<EventCall> Events = [];
        internal int LegacyChanges;
        internal string? Skip;
        internal string? ActionPoint;
        internal Action? AtPoint;

        internal TraceControl()
        {
            Move += (sender, e) => Event("Move", sender, e);
            LocationChanged += (sender, e) => Event("LocationChanged", sender, e);
            Resize += (sender, e) => Event("Resize", sender, e);
            SizeChanged += (sender, e) => Event("SizeChanged", sender, e);
        }

        internal void Reset()
        {
            Calls.Clear();
            Events.Clear();
            LegacyChanges = 0;
        }

        internal void CallCore(Rectangle bounds, BoundsSpecified specified)
            => SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);

        internal void CallNotification(string name, EventArgs e)
        {
            switch (name)
            {
                case "LocationChanged": OnLocationChanged(e); break;
                case "Move": OnMove(e); break;
                case "SizeChanged": OnSizeChanged(e); break;
                case "Resize": OnResize(e); break;
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            Point("enter:LocationChanged");
            if (Skip != "LocationChanged") { base.OnLocationChanged(e); }
            Point("exit:LocationChanged");
        }

        protected override void OnMove(EventArgs e)
        {
            Point("enter:Move");
            if (Skip != "Move") { base.OnMove(e); }
            Point("exit:Move");
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Point("enter:SizeChanged");
            if (Skip != "SizeChanged") { base.OnSizeChanged(e); }
            Point("exit:SizeChanged");
        }

        protected override void OnResize(EventArgs e)
        {
            Point("enter:Resize");
            if (Skip != "Resize") { base.OnResize(e); }
            Point("exit:Resize");
        }

        // Deliberately do not call base: several existing layout controls use this pattern.
        protected override void OnBoundsChanged() => LegacyChanges++;

        private void Event(string name, object? sender, EventArgs? e)
        {
            Events.Add(new EventCall(name, sender, e, Bounds));
            Point("event:" + name);
        }

        private void Point(string point)
        {
            Calls?.Add(point);
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
    [StructLayout(LayoutKind.Sequential)]
    private struct NativePoint { public int X; public int Y; }
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint handle, out NativeRect rectangle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ScreenToClient(nint handle, ref NativePoint point);
}
