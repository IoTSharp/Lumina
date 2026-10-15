using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlDefaultSizeTests
{
    [ThreadStatic] private static Settings? Current;

    [Fact]
    public void DefaultSize_HasStandardProtectedVirtualGetterMetadata()
    {
        PropertyInfo property = typeof(Control).GetProperty("DefaultSize", BindingFlags.Instance | BindingFlags.NonPublic)!;
        Assert.Equal(typeof(Control), property.DeclaringType);
        Assert.Equal(typeof(Size), property.PropertyType);
        Assert.Null(property.SetMethod);
        Assert.Empty(property.GetIndexParameters());
        Assert.Empty(property.CustomAttributes);
        MethodInfo getter = property.GetMethod!;
        Assert.True(getter.IsFamily);
        Assert.False(getter.IsFamilyOrAssembly);
        Assert.False(getter.IsPublic);
        Assert.False(getter.IsStatic);
        Assert.True(getter.IsVirtual);
        Assert.False(getter.IsFinal);
        Assert.False(getter.IsAbstract);
        Assert.NotEqual(0, (int)(getter.Attributes & MethodAttributes.NewSlot));
        Assert.Equal(getter, getter.GetBaseDefinition());
        Assert.Empty(getter.GetParameters());
    }

    [Fact]
    public void BaseConstruction_StartsWithEmptyBoundsAndClientAreaWithoutCreatingAHandle()
        => OnSta(() =>
        {
            using var control = new Control();
            Assert.Equal(Rectangle.Empty, control.Bounds);
            Assert.Equal(Size.Empty, control.Size);
            Assert.Equal(Size.Empty, control.ClientSize);
            Assert.Equal(Rectangle.Empty, control.ClientRectangle);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, control.Handle);
            int notifications = 0;
            control.LocationChanged += (_, _) => notifications++;
            control.Move += (_, _) => notifications++;
            control.SizeChanged += (_, _) => notifications++;
            control.Resize += (_, _) => notifications++;
            control.ClientSizeChanged += (_, _) => notifications++;
            control.HandleCreated += (_, _) => notifications++;
            _ = control.Bounds;
            _ = control.Size;
            _ = control.ClientSize;
            _ = control.ClientRectangle;
            Assert.Equal(0, notifications);
            Assert.False(control.IsHandleCreated);
        });

    public static IEnumerable<object[]> InitialCases()
    {
        foreach (string border in new[] { "none", "border", "edge", "both" })
        {
            foreach ((int width, int height) in new[]
            {
                (83, 47), (0, 0), (0, 47), (83, 0), (-3, -5), (-3, 37), (73, -5), (1, 1),
                (int.MinValue, -1), (-1, int.MinValue), (int.MaxValue, int.MaxValue),
                (int.MinValue, int.MaxValue), (int.MaxValue - 1, int.MinValue + 1)
            })
            {
                foreach (string profile in new[] { "empty", "fixed", "conflict", "negative", "zero-axis" })
                {
                    yield return [border, width, height, profile];
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(InitialCases))]
    public void Construction_ConsumesDefaultConstraintsBeforeAssigningUnclampedDefaultSizeAndConditionalClientArea(
        string border, int width, int height, string profile)
        => OnSta(() =>
        {
            Settings settings = Configure(new Size(width, height), profile, border);
            using var control = new TraceControl();
            Assert.Equal(new Rectangle(0, 0, width, height), control.Bounds);
            int nonClient = border switch { "border" => 2, "edge" => 4, "both" => 6, _ => 0 };
            Size client = width == 0 || height == 0 ? Size.Empty
                : new Size(unchecked(width - nonClient), unchecked(height - nonClient));
            Assert.Equal(client, control.ClientSize);
            Assert.Equal(new Rectangle(Point.Empty, client), control.ClientRectangle);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, control.Handle);
            Assert.DoesNotContain(settings.Calls, call => call.Point is "enter:core" or "HandleCreated" || call.Point.StartsWith("notify:", StringComparison.Ordinal));
            string[] expected = profile == "empty"
                ? ["default:min", "default:max", "default:size", "derived-constructor"]
                : ["default:min", "default:min", "enter:set:min", "get:min", "default:min", "exit:set:min",
                    "default:max", "default:max", "enter:set:max", "get:max", "default:max", "exit:set:max", "default:size", "derived-constructor"];
            Assert.Equal(expected, settings.Calls.Select(call => call.Point));
            foreach (Call call in settings.Calls.Where(call => call.Point != "derived-constructor"))
            {
                Assert.Equal(Rectangle.Empty, call.Bounds);
                Assert.Equal(Size.Empty, call.Client);
                Assert.False(call.Created);
                Assert.Null(call.Context);
            }
            Assert.Equal(settings.Minimum, control.MinimumSize);
            Assert.Equal(settings.Maximum, control.MaximumSize);
            int calls = settings.Calls.Count;
            Assert.Equal(Size.Empty, control.BaseDefaultSize);
            Assert.Equal(calls, settings.Calls.Count);
        });

    [Theory]
    [InlineData("empty")]
    [InlineData("fixed")]
    [InlineData("negative")]
    public void BaseDefaultSize_BypassesOverrideAndDoesNotApplyDefaultConstraints(string profile)
        => OnSta(() =>
        {
            Settings settings = Configure(new Size(83, 47), profile);
            settings.UseBaseSize = true;
            using var control = new TraceControl();
            Assert.Equal(Size.Empty, control.Size);
            Assert.Equal(Size.Empty, control.ClientSize);
            Assert.DoesNotContain(settings.Calls, call => call.Point == "enter:core");
        });

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void Construction_DispatchesPublicConstraintSettersAndHonorsOverridesThatSkipBase(bool minimum, bool maximum)
        => OnSta(() =>
        {
            Settings settings = Configure(new Size(83, 47), "fixed");
            settings.SkipMinimum = minimum;
            settings.SkipMaximum = maximum;
            using var control = new TraceControl();
            Assert.Contains(settings.Calls, call => call.Point == "enter:set:min" && call.Value == new Size(31, 17));
            Assert.Contains(settings.Calls, call => call.Point == "enter:set:max" && call.Value == new Size(101, 59));
            Assert.Equal(minimum ? 0 : 1, settings.Calls.Count(call => call.Point == "get:min"));
            Assert.Equal(maximum ? 0 : 1, settings.Calls.Count(call => call.Point == "get:max"));
            Assert.Equal(new Size(83, 47), control.Size);
            Assert.DoesNotContain(settings.Calls, call => call.Point == "enter:core");
        });

    [Fact]
    public void Construction_SetterCanResizeThroughAnOverriddenGetterBeforeDefaultSizeOverwritesOnlyDimensions()
        => OnSta(() =>
        {
            Settings settings = Configure(new Size(83, 47), "fixed");
            settings.EmptyGetters = true;
            using var control = new TraceControl();
            Call beforeDefault = Assert.Single(settings.Calls, call => call.Point == "default:size");
            Assert.Equal(new Rectangle(0, 0, 31, 17), beforeDefault.Bounds);
            Assert.Equal(new Size(31, 17), beforeDefault.Client);
            Assert.Equal(new[] { "notify:SizeChanged", "notify:Resize", "notify:ClientSizeChanged" },
                settings.Calls.Where(call => call.Point.StartsWith("notify:", StringComparison.Ordinal)).Select(call => call.Point));
            Assert.Equal(2, settings.Calls.Count(call => call.Point == "enter:core"));
            Assert.All(settings.Calls.Where(call => call.Point == "enter:core"), call =>
            {
                Assert.Equal(new Size(31, 17), call.Value);
                Assert.Equal(BoundsSpecified.Size, call.Specified);
            });
            Assert.Equal(new Size(83, 47), control.Size);
            Assert.Equal(new Size(83, 47), control.ClientSize);
            Assert.Equal(Size.Empty, control.MinimumSize);
            Assert.Equal(Size.Empty, control.MaximumSize);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void Construction_UsesRepeatedDefaultReadsAndRetainsTheValueActuallyStoredByItsSetter()
        => OnSta(() =>
        {
            Settings settings = Configure(new Size(83, 47), "fixed");
            settings.ChangingDefaults = true;
            using var control = new TraceControl();
            Assert.Equal(new Size(43, 29), Assert.Single(settings.Calls, call => call.Point == "enter:set:min").Value);
            Assert.Equal(new Size(131, 71), Assert.Single(settings.Calls, call => call.Point == "enter:set:max").Value);
            Assert.Equal(new Rectangle(0, 0, 43, 29), Assert.Single(settings.Calls, call => call.Point == "default:size").Bounds);
            Assert.Equal(new Size(43, 29), control.MinimumSize);
            Assert.Equal(new Size(131, 71), control.MaximumSize);
            settings.ChangingDefaults = false;
            settings.Minimum = new Size(53, 37);
            settings.Maximum = new Size(151, 79);
            Assert.Equal(new Size(43, 29), control.MinimumSize);
            Assert.Equal(new Size(151, 79), control.MaximumSize);
            Assert.Equal(new Size(83, 47), control.Size);
        });

    public static IEnumerable<object[]> ReentryCases()
    {
        foreach (string point in new[] { "default:min", "get:min", "enter:set:min", "exit:set:min", "default:max",
            "get:max", "enter:set:max", "exit:set:max", "default:size" })
        {
            foreach (bool client in new[] { false, true })
            {
                foreach ((int width, int height) in new[] { (83, 47), (0, 0), (0, 47), (-3, -5) })
                {
                    yield return [point, client, width, height];
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(ReentryCases))]
    public void ReentrantDefaults_PreserveLocationAndRetainRawClientCacheWhenEitherDefaultAxisIsZero(
        string point, bool client, int width, int height)
        => OnSta(() =>
        {
            Settings settings = Configure(new Size(width, height), "fixed");
            bool called = false;
            settings.AtPoint = (control, observed) =>
            {
                if (point != observed) { return; }
                settings.AtPoint = null;
                called = true;
                if (client) { control.ClientSize = new Size(19, 11); }
                else { control.Bounds = new Rectangle(7, 9, 41, 23); }
            };
            using var control = new TraceControl();
            Assert.True(called);
            Assert.Equal(new Rectangle(client ? Point.Empty : new Point(7, 9), new Size(width, height)), control.Bounds);
            Assert.Equal(width == 0 || height == 0 ? client ? new Size(19, 11) : new Size(41, 23) : new Size(width, height), control.ClientSize);
            Assert.Equal(new Rectangle(Point.Empty, control.ClientSize), control.ClientRectangle);
            Assert.False(control.IsHandleCreated);
            Assert.DoesNotContain(settings.Calls, call => call.Point == "HandleCreated");
        });

    [Theory]
    [InlineData("default:min")]
    [InlineData("get:min")]
    [InlineData("enter:set:min")]
    [InlineData("exit:set:min")]
    [InlineData("default:max")]
    [InlineData("get:max")]
    [InlineData("enter:set:max")]
    [InlineData("exit:set:max")]
    [InlineData("default:size")]
    [InlineData("derived-constructor")]
    public void ConstructorExtensionFailures_PropagateTheOriginalExceptionAndKeepTheObservedPartialState(string point)
        => OnSta(() =>
        {
            Settings settings = Configure(new Size(83, 47), "fixed");
            var failure = new InvalidOperationException("constructor extension");
            settings.AtPoint = (_, observed) => { if (observed == point) { throw failure; } };
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => new TraceControl()));
            settings.AtPoint = null;
            using TraceControl control = settings.Observed!;
            Assert.Equal(point == "derived-constructor" ? new Size(83, 47) : Size.Empty, control.Size);
            Assert.Equal(control.Size, control.ClientSize);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(point, settings.Calls[^1].Point);
        });

    [Theory]
    [InlineData("enter:core", 0, 0)]
    [InlineData("exit:core", 31, 17)]
    [InlineData("notify:SizeChanged", 31, 17)]
    [InlineData("notify:Resize", 31, 17)]
    [InlineData("notify:ClientSizeChanged", 31, 17)]
    public void ConstructorConstraintFailures_PreserveTheCommittedMinimumAndDoNotReachDefaultSize(string point, int width, int height)
        => OnSta(() =>
        {
            Settings settings = Configure(new Size(83, 47), "fixed");
            settings.EmptyGetters = true;
            var failure = new InvalidOperationException("constructor constraints");
            settings.AtPoint = (_, observed) => { if (observed == point) { throw failure; } };
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => new TraceControl()));
            settings.AtPoint = null;
            using TraceControl control = settings.Observed!;
            Assert.Equal(new Size(width, height), control.Size);
            Assert.Equal(new Size(width, height), control.ClientSize);
            Assert.DoesNotContain(settings.Calls, call => call.Point == "default:size");
            settings.EmptyGetters = false;
            settings.Minimum = new Size(53, 37);
            Assert.Equal(new Size(31, 17), control.MinimumSize);
            Assert.False(control.IsHandleCreated);
        });

    public static IEnumerable<object[]> ContextCases()
    {
        foreach (bool autoInstall in new[] { false, true })
        {
            foreach (string context in new[] { "null", "base", "custom" })
            {
                foreach (string point in new[] { "none", "default:min", "default:max", "default:size", "derived-constructor" })
                {
                    yield return [autoInstall, context, point];
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(ContextCases))]
    public void SynchronizationContext_InstallsAfterDefaultInitializationAndBeforeTheDerivedConstructor(
        bool autoInstall, string context, string point)
        => OnSta(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = autoInstall;
            SynchronizationContext? previous = context == "custom" ? new CustomContext()
                : context == "base" ? new SynchronizationContext() : null;
            SynchronizationContext.SetSynchronizationContext(previous);
            Settings settings = Configure(new Size(83, 47), "fixed");
            var failure = new InvalidOperationException("constructor context");
            if (point != "none") { settings.AtPoint = (_, observed) => { if (observed == point) { throw failure; } }; }
            TraceControl? control = null;
            if (point == "none") { control = new TraceControl(); }
            else { Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => new TraceControl())); }
            settings.AtPoint = null;
            using (control ?? settings.Observed!)
            {
                foreach (Call call in settings.Calls.Where(call => call.Point.StartsWith("default:", StringComparison.Ordinal)))
                {
                    Assert.Same(previous, call.Context);
                }
                bool installed = autoInstall && context != "custom" && point is "none" or "derived-constructor";
                if (installed) { Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current); }
                else { Assert.Same(previous, SynchronizationContext.Current); }
                if (point is "none" or "derived-constructor")
                {
                    Assert.Same(SynchronizationContext.Current, Assert.Single(settings.Calls, call => call.Point == "derived-constructor").Context);
                }
            }
        });

    private static Settings Configure(Size size, string profile = "empty", string border = "none")
    {
        Current = new Settings
        {
            Default = size,
            Border = border,
            Minimum = profile switch { "fixed" => new(31, 17), "conflict" => new(101, 59), "negative" => new(-7, -9), "zero-axis" => new(0, 17), _ => Size.Empty },
            Maximum = profile switch { "fixed" => new(101, 59), "conflict" => new(31, 17), "negative" => new(-3, -5), "zero-axis" => new(101, 0), _ => Size.Empty }
        };
        return Current;
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            SynchronizationContext.SetSynchronizationContext(null);
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally { Current = null; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The constructor default check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class CustomContext : SynchronizationContext { }

    private sealed class Settings
    {
        internal Size Default;
        internal Size Minimum;
        internal Size Maximum;
        internal string Border = "none";
        internal bool UseBaseSize;
        internal bool SkipMinimum;
        internal bool SkipMaximum;
        internal bool EmptyGetters;
        internal bool ChangingDefaults;
        internal int MinimumReads;
        internal int MaximumReads;
        internal TraceControl? Observed;
        internal readonly List<Call> Calls = [];
        internal Action<TraceControl, string>? AtPoint;
    }

    private sealed record Call(string Point, Rectangle Bounds, Size Client, bool Created, SynchronizationContext? Context,
        Size? Value = null, BoundsSpecified? Specified = null);

    private sealed class TraceControl : Control
    {
        internal TraceControl() => Point("derived-constructor");
        internal Size BaseDefaultSize => base.DefaultSize;

        protected override Size DefaultSize
        {
            get { Point("default:size"); return Current!.UseBaseSize ? base.DefaultSize : Current.Default; }
        }

        protected override Size DefaultMinimumSize
        {
            get
            {
                Settings settings = Current!;
                settings.MinimumReads++;
                Point("default:min");
                return settings.ChangingDefaults && settings.MinimumReads > 1
                    ? settings.MinimumReads == 2 ? new Size(43, 29) : new Size(47, 31) : settings.Minimum;
            }
        }

        protected override Size DefaultMaximumSize
        {
            get
            {
                Settings settings = Current!;
                settings.MaximumReads++;
                Point("default:max");
                return settings.ChangingDefaults && settings.MaximumReads > 1
                    ? settings.MaximumReads == 2 ? new Size(127, 67) : new Size(131, 71) : settings.Maximum;
            }
        }

        public override Size MinimumSize
        {
            get { Point("get:min"); return Current!.EmptyGetters ? Size.Empty : base.MinimumSize; }
            set { Point("enter:set:min", value); if (!Current!.SkipMinimum) { base.MinimumSize = value; } Point("exit:set:min", value); }
        }

        public override Size MaximumSize
        {
            get { Point("get:max"); return Current!.EmptyGetters ? Size.Empty : base.MaximumSize; }
            set { Point("enter:set:max", value); if (!Current!.SkipMaximum) { base.MaximumSize = value; } Point("exit:set:max", value); }
        }

        protected override uint Style => base.Style | (Current!.Border is "border" or "both" ? 0x00800000u : 0u);
        protected override uint ExStyle => base.ExStyle | (Current!.Border is "edge" or "both" ? 0x00000200u : 0u);

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Point("enter:core", new Size(width, height), specified);
            base.SetBoundsCore(x, y, width, height, specified);
            Point("exit:core", new Size(width, height), specified);
        }

        protected override void OnBoundsChanged() { }
        protected override void OnLocationChanged(EventArgs e) { Point("notify:LocationChanged"); base.OnLocationChanged(e); }
        protected override void OnMove(EventArgs e) { Point("notify:Move"); base.OnMove(e); }
        protected override void OnSizeChanged(EventArgs e) { Point("notify:SizeChanged"); base.OnSizeChanged(e); }
        protected override void OnResize(EventArgs e) { Point("notify:Resize"); base.OnResize(e); }
        protected override void OnClientSizeChanged(EventArgs e) { Point("notify:ClientSizeChanged"); base.OnClientSizeChanged(e); }
        protected override void OnHandleCreated() { Point("HandleCreated"); base.OnHandleCreated(); }

        private void Point(string point, Size? value = null, BoundsSpecified? specified = null)
        {
            Settings settings = Current!;
            settings.Observed = this;
            settings.Calls.Add(new Call(point, Bounds, ClientSize, IsHandleCreated, SynchronizationContext.Current, value, specified));
            settings.AtPoint?.Invoke(this, point);
        }
    }
}
