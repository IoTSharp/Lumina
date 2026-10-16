using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlSizeConstraintsTests
{
    private static readonly Rectangle Initial = new(11, 13, 73, 37);

    [Theory]
    [InlineData("MinimumSize", "Specifies the minimum size of the control.")]
    [InlineData("MaximumSize", "Specifies the maximum size of the control.")]
    public void Constraints_HaveStandardPublicVirtualMetadata(string name, string description)
    {
        PropertyInfo property = typeof(Control).GetProperty(name)!;
        Assert.Equal(typeof(Size), property.PropertyType);
        Assert.Equal("Layout", property.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal(description, property.GetCustomAttribute<DescriptionAttribute>()!.Description);
        Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>());
        Assert.Null(property.GetCustomAttribute<BrowsableAttribute>());
        Assert.Null(property.GetCustomAttribute<EditorBrowsableAttribute>());
        Assert.Null(property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>());
        AmbientValueAttribute? ambient = property.GetCustomAttribute<AmbientValueAttribute>();
        if (name == "MaximumSize") { Assert.Equal(Size.Empty, ambient!.Value); }
        else { Assert.Null(ambient); }
        foreach (MethodInfo method in property.GetAccessors())
        {
            Assert.True(method.IsPublic);
            Assert.True(method.IsVirtual);
            Assert.False(method.IsFinal);
            Assert.False(method.IsStatic);
            Assert.NotEqual(0, (int)(method.Attributes & MethodAttributes.NewSlot));
        }
        ParameterInfo parameter = Assert.Single(property.SetMethod!.GetParameters());
        Assert.Equal("value", parameter.Name);
        Assert.Equal(typeof(Size), parameter.ParameterType);
    }

    [Theory]
    [InlineData("DefaultMinimumSize")]
    [InlineData("DefaultMaximumSize")]
    public void DefaultExtensions_HaveStandardProtectedVirtualMetadata(string name)
    {
        PropertyInfo property = typeof(Control).GetProperty(name, BindingFlags.Instance | BindingFlags.NonPublic)!;
        Assert.Equal(typeof(Size), property.PropertyType);
        Assert.Null(property.SetMethod);
        MethodInfo getter = property.GetMethod!;
        Assert.True(getter.IsFamily);
        Assert.False(getter.IsFamilyOrAssembly);
        Assert.True(getter.IsVirtual);
        Assert.False(getter.IsFinal);
        Assert.False(getter.IsStatic);
        Assert.NotEqual(0, (int)(getter.Attributes & MethodAttributes.NewSlot));
        Assert.Empty(getter.GetParameters());
    }

    public static IEnumerable<object[]> Contexts()
    {
        foreach (string border in new[] { "none", "border", "edge", "both" })
        {
            yield return [border, false];
            yield return [border, true];
        }
    }

    [Theory]
    [MemberData(nameof(Contexts))]
    public void Defaults_AreEmptyAndReadsDoNotResizeOrCreateAHandle(string border, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            Snapshot before = Capture(control);
            Assert.Equal(Size.Empty, control.MinimumSize);
            Assert.Equal(Size.Empty, control.MaximumSize);
            Assert.Equal(new[] { "get:min", "default:min", "get:max", "default:max" }, control.Calls);
            Assert.Empty(control.Notifications);
            Assert.Equal(before, Capture(control));
        });

    [Theory]
    [InlineData("min", 83, 47, 83, 47)]
    [InlineData("min", 0, 0, 73, 37)]
    [InlineData("min", -3, -5, 73, 37)]
    [InlineData("min", -3, 37, 73, 37)]
    [InlineData("min", 73, -5, 73, 37)]
    [InlineData("min", 0, 47, 73, 47)]
    [InlineData("min", 83, 0, 83, 37)]
    [InlineData("max", 83, 47, 73, 37)]
    [InlineData("max", 0, 0, 73, 37)]
    [InlineData("max", -3, -5, 0, 0)]
    [InlineData("max", -3, 37, 0, 37)]
    [InlineData("max", 73, -5, 73, 0)]
    [InlineData("max", 0, 47, 0, 37)]
    [InlineData("max", 83, 0, 73, 0)]
    public void Assignments_PreserveSignedValuesAndResizeThroughTheVirtualCore(string property, int width, int height, int expectedWidth, int expectedHeight)
        => OnSta(() =>
        {
            using TraceControl control = Create();
            var value = new Size(width, height);
            Assign(control, property, value);
            Assert.Equal(value, property == "min" ? control.MinimumSize : control.MaximumSize);
            Assert.Equal(new Size(expectedWidth, expectedHeight), control.Size);
            Assert.Equal(value == Size.Empty ? 0 : 1, control.CoreCalls.Count);
            Assert.All(control.CoreCalls, call => Assert.Equal(BoundsSpecified.Size, call.Specified));
            Assert.Equal(control.Size, control.ClientSize);
            control.Reset();
            Assign(control, property, value);
            Assert.Empty(control.CoreCalls);
            Assert.Empty(control.Notifications);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [MemberData(nameof(Contexts))]
    public void ConflictingConstraints_RemainIndependentAndMinimumWinsAtConsumption(string border, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            control.MinimumSize = new Size(101, 61);
            control.MaximumSize = new Size(43, 29);
            Assert.Equal(new Size(101, 61), control.MinimumSize);
            Assert.Equal(new Size(43, 29), control.MaximumSize);
            Assert.Equal(new Size(101, 61), control.Size);
            control.Reset();
            control.Bounds = new Rectangle(17, 19, 211, 151);
            Assert.Equal(new Rectangle(17, 19, 101, 61), control.Bounds);
            Assert.Equal(new[] { "Move", "LocationChanged" }, control.Notifications);
            control.MinimumSize = Size.Empty;
            Assert.Equal(new Size(101, 61), control.Size);
            control.Size = new Size(211, 151);
            Assert.Equal(new Size(43, 29), control.Size);
            Assert.Equal(native, control.IsHandleCreated);
        });

    [Theory]
    [MemberData(nameof(Contexts))]
    public void ZeroMaximumAxis_ShrinksDuringAssignmentThenRemainsUnboundedDuringSizing(string border, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            control.MaximumSize = new Size(0, 47);
            Assert.Equal(new Size(0, 37), control.Size);
            control.Size = new Size(97, 61);
            Assert.Equal(new Size(97, 47), control.Size);
            control.MaximumSize = Size.Empty;
            control.Size = new Size(-3, -5);
            Assert.Equal(native ? Size.Empty : new Size(-3, -5), control.Size);
        });

    public static IEnumerable<object[]> RouteCases()
    {
        foreach (object[] context in Contexts())
        {
            foreach (string entry in new[] { "bounds", "size", "width", "height", "set4", "set5-size", "set5-x", "set5-none", "set5-unknown", "core", "client", "client-core" })
            {
                foreach (Size input in new[] { new Size(19, 11), new Size(97, 61), new Size(-3, -5) })
                {
                    yield return [context[0], context[1], entry, input.Width, input.Height];
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(RouteCases))]
    public void BoundsAndClientRoutes_ConsumeConstraintsAndPreserveSpecifiedComponentsAndRawClientRequests(string border, bool native, string entry, int width, int height)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            SetProfile(control);
            var input = new Size(width, height);
            Apply(control, entry, input);
            bool client = entry is "client" or "client-core";
            Size expectedSize = entry is "set5-x" or "set5-none" or "set5-unknown" ? Initial.Size
                : entry == "width" ? new Size(width < 41 ? 41 : 83, 37)
                : entry == "height" ? new Size(73, height < 23 ? 23 : 47)
                : width < 41 ? new Size(41, 23) : new Size(83, 47);
            Point expectedLocation = entry is "bounds" or "set4" or "core" ? new Point(17, 19)
                : entry is "set5-x" or "set5-unknown" ? new Point(17, 13) : Initial.Location;
            Assert.Equal(new Rectangle(expectedLocation, expectedSize), control.Bounds);
            Assert.Equal(client ? input : new Size(expectedSize.Width - BorderSize(border), expectedSize.Height - BorderSize(border)), control.ClientSize);
            Assert.Equal(native, control.IsHandleCreated);
            if (native) { AssertNativeClient(control, new Size(expectedSize.Width - BorderSize(border), expectedSize.Height - BorderSize(border))); }
            Assert.DoesNotContain(control.Calls, call => call.StartsWith("set:", StringComparison.Ordinal));
            control.Reset();
            Apply(control, entry, input);
            Assert.Equal(new Rectangle(expectedLocation, expectedSize), control.Bounds);
            Assert.Equal(client ? input : new Size(expectedSize.Width - BorderSize(border), expectedSize.Height - BorderSize(border)), control.ClientSize);
            Assert.Equal(client ? native ? new[] { "ClientSizeChanged" }
                : new[] { "Resize", "SizeChanged", "ClientSizeChanged", "ClientSizeChanged" } : Array.Empty<string>(), control.Notifications);
        });

    [Theory]
    [MemberData(nameof(Contexts))]
    public void CoreRawEquality_ReturnsBeforeConstraintGettersWhileChangedRequestsKeepTheirReadOrder(string border, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            control.CallCore(Initial, BoundsSpecified.All);
            Assert.Equal(new[] { "core" }, control.Calls);
            control.Reset();
            control.Size = new Size(97, 61);
            Assert.Equal(new[] { "core", "get:max", "default:max", "get:min", "default:min" }, control.Calls.Where(call => !call.StartsWith("event:", StringComparison.Ordinal)));
            SetProfile(control);
            control.Reset();
            control.Size = new Size(19, 11);
            Assert.Equal(new[] { "core", "get:max", "default:max", "get:max", "default:max", "get:min", "default:min" }, control.Calls.Where(call => !call.StartsWith("event:", StringComparison.Ordinal)));
        });

    [Theory]
    [MemberData(nameof(Contexts))]
    public void DefaultExtensions_AreReadEvenWithStoredValuesAndMaximumResetRestoresItsDefault(string border, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            control.DefaultMin = new Size(31, 17);
            control.DefaultMax = new Size(101, 59);
            Assert.Equal(control.DefaultMin, control.MinimumSize);
            Assert.Equal(control.DefaultMax, control.MaximumSize);
            control.MinimumSize = new Size(83, 47);
            control.MaximumSize = new Size(97, 61);
            control.Reset();
            Assert.Equal(new Size(83, 47), control.MinimumSize);
            Assert.Equal(new Size(97, 61), control.MaximumSize);
            Assert.Equal(new[] { "get:min", "default:min", "get:max", "default:max" }, control.Calls);
            Snapshot before = Capture(control);
            control.MinimumSize = Size.Empty;
            control.MaximumSize = Size.Empty;
            Assert.Equal(Size.Empty, control.MinimumSize);
            Assert.Equal(new Size(101, 59), control.MaximumSize);
            Assert.Equal(before, Capture(control));
            control.DefaultMin = new Size(47, 31);
            control.DefaultMax = new Size(127, 67);
            Assert.Equal(Size.Empty, control.MinimumSize);
            Assert.Equal(new Size(127, 67), control.MaximumSize);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void MaximumReset_DoesNotConsumePoisonedGetterOrCallTheCore(bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            control.MaximumSize = new Size(83, 47);
            control.Reset();
            var failure = new InvalidOperationException("maximum default");
            control.ActionPoint = "default:max";
            control.AtPoint = () => throw failure;
            control.MaximumSize = Size.Empty;
            Assert.Equal(new[] { "set:max" }, control.Calls);
            Assert.Empty(control.CoreCalls);
            Assert.Empty(control.Notifications);
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => _ = control.MaximumSize));
            Assert.Equal(Size.Empty, control.MaximumSize);
        });

    [Theory]
    [MemberData(nameof(Contexts))]
    public void DerivedGetters_CanImposeIndependentConstraintsWithoutChangingStoredValues(string border, bool native)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            control.MinimumOverride = new Size(101, 61);
            control.MaximumOverride = new Size(43, 29);
            control.Size = new Size(97, 61);
            Assert.Equal(new Size(101, 61), control.Size);
            Assert.Equal(Size.Empty, control.BaseMinimum);
            Assert.Equal(Size.Empty, control.BaseMaximum);
            control.MaximumSize = new Size(127, 67);
            Assert.Equal(new Size(127, 67), control.BaseMaximum);
            Assert.Equal(new Size(43, 29), control.MaximumSize);
            Assert.Equal(new Size(101, 61), control.Size);
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void OverridesWithoutBase_PreserveSetterStorageAndClientCoreIndependence(bool native, bool skipCore)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            control.SkipSetter = !skipCore;
            control.SkipCore = skipCore;
            control.MinimumSize = new Size(83, 47);
            control.MaximumSize = new Size(43, 29);
            Assert.Equal(skipCore ? new Size(83, 47) : Size.Empty, control.BaseMinimum);
            Assert.Equal(skipCore ? new Size(43, 29) : Size.Empty, control.BaseMaximum);
            Assert.Equal(Initial, control.Bounds);
            control.ClientSize = new Size(19, 11);
            Assert.Equal(skipCore ? Initial.Size : new Size(19, 11), control.Size);
            Assert.Equal(new Size(19, 11), control.ClientSize);
        });

    [Theory]
    [InlineData(false, "get:min", 41, 23)]
    [InlineData(false, "default:min", 41, 23)]
    [InlineData(false, "get:max", 101, 61)]
    [InlineData(false, "default:max", 101, 61)]
    [InlineData(true, "get:min", 41, 23)]
    [InlineData(true, "default:min", 41, 23)]
    [InlineData(true, "get:max", 101, 61)]
    [InlineData(true, "default:max", 101, 61)]
    public void GetterFailures_PropagateOriginalExceptionAndKeepTheObservedStorageBoundary(bool native, string point, int minWidth, int minHeight)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            SetProfile(control);
            var failure = new InvalidOperationException("constraint getter");
            control.ActionPoint = point;
            control.AtPoint = () => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.MinimumSize = new Size(101, 61)));
            Assert.Equal(new Size(minWidth, minHeight), control.MinimumSize);
            Assert.Equal(new Size(83, 47), control.MaximumSize);
            Assert.Equal(Initial, control.Bounds);
            Assert.Empty(control.Notifications);
        });

    [Theory]
    [InlineData(false, "min")]
    [InlineData(false, "max")]
    [InlineData(true, "min")]
    [InlineData(true, "max")]
    public void CoreOverrideFailure_LeavesTheAssignedConstraintStoredBeforeAnyBoundsChange(bool native, string property)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            var failure = new InvalidOperationException("constraint core");
            control.ActionPoint = "core";
            control.AtPoint = () => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => Assign(control, property, new Size(101, 61))));
            Assert.Equal(new Size(101, 61), property == "min" ? control.MinimumSize : control.MaximumSize);
            Assert.Equal(Initial, control.Bounds);
            Assert.Empty(control.Notifications);
        });

    [Theory]
    [InlineData(false, "Resize")]
    [InlineData(false, "SizeChanged")]
    [InlineData(false, "ClientSizeChanged")]
    [InlineData(true, "Resize")]
    [InlineData(true, "SizeChanged")]
    [InlineData(true, "ClientSizeChanged")]
    public void NotificationFailure_PreservesCommittedConstraintAndExistingNativeExceptionRouting(bool native, string notification)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form);
            var failure = new InvalidOperationException("constraint notification");
            var delivered = new List<Exception>();
            void Handler(object sender, ThreadExceptionEventArgs args) => delivered.Add(args.Exception);
            Application.ThreadException += Handler;
            try
            {
                control.ActionPoint = "event:" + notification;
                control.AtPoint = () => throw failure;
                if (native) { control.MinimumSize = new Size(101, 61); Assert.Same(failure, Assert.Single(delivered)); }
                else { Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.MinimumSize = new Size(101, 61))); Assert.Empty(delivered); }
                Assert.Equal(new Size(101, 61), control.MinimumSize);
                Assert.Equal(new Size(101, 61), control.Size);
                Assert.Equal(new Size(101, 61), control.ClientSize);
            }
            finally { Application.ThreadException -= Handler; }
        });

    public static IEnumerable<object[]> ReentryCases()
    {
        foreach (object[] context in Contexts())
        {
            yield return [context[0], context[1], "get:max", 47, 31];
            yield return [context[0], context[1], "get:min", 83, 47];
        }
    }

    [Theory]
    [MemberData(nameof(ReentryCases))]
    public void ReentrantMaximumChange_PreservesGetterReadOrderAndTheOuterRawClientRequest(string border, bool native, string point, int width, int height)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            SetProfile(control);
            control.ActionPoint = point;
            control.AtPoint = () => control.MaximumSize = new Size(47, 31);
            control.ClientSize = new Size(101, 61);
            Assert.Equal(new Size(width, height), control.Size);
            Assert.Equal(new Size(47, 31), control.MaximumSize);
            Assert.Equal(new Size(41, 23), control.MinimumSize);
            Assert.Equal(new Size(101, 61), control.ClientSize);
            Assert.Equal(point == "get:max" ? 4 : 7, control.Notifications.Count);
            Assert.Equal("ClientSizeChanged", control.Notifications[^1]);
            if (native) { AssertNativeClient(control, new Size(width - BorderSize(border), height - BorderSize(border))); }
        });

    private static void Assign(TraceControl control, string property, Size value)
    {
        if (property == "min") { control.MinimumSize = value; }
        else { control.MaximumSize = value; }
    }

    private static void SetProfile(TraceControl control)
    {
        control.MinimumSize = new Size(41, 23);
        control.MaximumSize = new Size(83, 47);
        control.Reset();
    }

    private static void Apply(TraceControl control, string entry, Size input)
    {
        if (entry == "bounds") { control.Bounds = new Rectangle(17, 19, input.Width, input.Height); }
        else if (entry == "size") { control.Size = input; }
        else if (entry == "width") { control.Width = input.Width; }
        else if (entry == "height") { control.Height = input.Height; }
        else if (entry == "set4") { control.SetBounds(17, 19, input.Width, input.Height); }
        else if (entry == "set5-size") { control.SetBounds(17, 19, input.Width, input.Height, BoundsSpecified.Size); }
        else if (entry == "set5-x") { control.SetBounds(17, 19, input.Width, input.Height, BoundsSpecified.X); }
        else if (entry == "set5-none") { control.SetBounds(17, 19, input.Width, input.Height, BoundsSpecified.None); }
        else if (entry == "set5-unknown") { control.SetBounds(17, 19, input.Width, input.Height, (BoundsSpecified)257); }
        else if (entry == "core") { control.CallCore(new Rectangle(17, 19, input.Width, input.Height), BoundsSpecified.None); }
        else if (entry == "client") { control.ClientSize = input; }
        else { control.CallClientCore(input); }
    }

    private static int BorderSize(string border) => border switch { "border" => 2, "edge" => 4, "both" => 6, _ => 0 };
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

    private static Snapshot Capture(Control control) => new(control.Bounds, control.ClientSize, control.IsHandleCreated, control.IsHandleCreated ? control.Handle : 0);
    private readonly record struct Snapshot(Rectangle Bounds, Size ClientSize, bool Created, nint Handle);
    private readonly record struct CoreCall(Rectangle Bounds, BoundsSpecified Specified);

    private static void AssertNativeClient(Control control, Size expected)
    {
        Assert.True(GetClientRect(control.Handle, out NativeRect client));
        Assert.Equal(expected, new Size(client.Right - client.Left, client.Bottom - client.Top));
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception error) { failure = error; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The size constraints check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class TraceControl : Control
    {
        internal readonly List<string> Calls = [];
        internal readonly List<string> Notifications = [];
        internal readonly List<CoreCall> CoreCalls = [];
        private readonly string _border;
        internal Size DefaultMin;
        internal Size DefaultMax;
        internal Size? MinimumOverride;
        internal Size? MaximumOverride;
        internal bool SkipSetter;
        internal bool SkipCore;
        internal string? ActionPoint;
        internal Action? AtPoint;
        internal Size BaseMinimum => base.MinimumSize;
        internal Size BaseMaximum => base.MaximumSize;
        protected override uint Style => base.Style | (_border is "border" or "both" ? 0x00800000u : 0u);
        protected override uint ExStyle => base.ExStyle | (_border is "edge" or "both" ? 0x00000200u : 0u);

        internal TraceControl(string border)
        {
            _border = border;
            Move += (_, _) => Notify("Move");
            LocationChanged += (_, _) => Notify("LocationChanged");
            Resize += (_, _) => Notify("Resize");
            SizeChanged += (_, _) => Notify("SizeChanged");
            ClientSizeChanged += (_, _) => Notify("ClientSizeChanged");
        }

        public override Size MinimumSize
        {
            get { Point("get:min"); return MinimumOverride ?? base.MinimumSize; }
            set { Point("set:min"); if (!SkipSetter) { base.MinimumSize = value; } }
        }
        public override Size MaximumSize
        {
            get { Point("get:max"); return MaximumOverride ?? base.MaximumSize; }
            set { Point("set:max"); if (!SkipSetter) { base.MaximumSize = value; } }
        }
        protected override Size DefaultMinimumSize { get { Point("default:min"); return DefaultMin; } }
        protected override Size DefaultMaximumSize { get { Point("default:max"); return DefaultMax; } }
        protected override void OnBoundsChanged() { }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            CoreCalls?.Add(new CoreCall(new Rectangle(x, y, width, height), specified));
            Point("core");
            if (!SkipCore) { base.SetBoundsCore(x, y, width, height, specified); }
        }
        internal void CallCore(Rectangle bounds, BoundsSpecified specified) => SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
        internal void CallClientCore(Size size) => SetClientSizeCore(size.Width, size.Height);
        internal void Reset() { Calls.Clear(); Notifications.Clear(); CoreCalls.Clear(); }
        private void Notify(string name) { Notifications?.Add(name); Point("event:" + name); }
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
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetClientRect(nint handle, out NativeRect rectangle);
}
