using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    private static readonly List<string> Results = [];
    private static readonly InvalidOperationException Failure = new("constructor default probe");
    [ThreadStatic] private static Scenario? Current;
    [ThreadStatic] private static List<string>? Calls;
    [ThreadStatic] private static string? ActionPoint;
    [ThreadStatic] private static int MinimumReads;
    [ThreadStatic] private static int MaximumReads;

    [STAThread]
    private static void Main()
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
        }
#endif
        RecordMetadata();
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        using (var control = new Control())
        {
            Record("base-initial", State(control));
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
            Record("base-after-reads", State(control));
            Record("base-read-notifications", notifications.ToString(CultureInfo.InvariantCulture));
        }

        Size[] sizes = [new(83, 47), Size.Empty, new(0, 47), new(83, 0), new(-3, -5),
            new(-3, 37), new(73, -5), new(1, 1), new(int.MinValue, -1),
            new(-1, int.MinValue), new(int.MaxValue, int.MaxValue),
            new(int.MinValue, int.MaxValue), new(int.MaxValue - 1, int.MinValue + 1)];
        foreach (string border in new[] { "none", "border", "edge", "both" })
        {
            foreach (Size size in sizes)
            {
                foreach (string profile in new[] { "empty", "fixed", "conflict", "negative", "zero-axis" })
                {
                    Observe(new Scenario($"initial-{border}-{profile}-{Format(size)}", size, profile, border));
                }
            }
        }

        foreach (string profile in new[] { "empty", "fixed", "negative" })
        {
            foreach (string mode in new[] { "base-size", "skip-min-setter", "skip-max-setter", "skip-core", "fixed-getters", "empty-getters", "changing-defaults" })
            {
                Observe(new Scenario($"mode-{profile}-{mode}", new(83, 47), profile, Mode: mode));
            }
        }

        string[] points = ["default:min", "get:min", "enter:set:min", "exit:set:min", "default:max", "get:max",
            "enter:set:max", "exit:set:max", "default:size", "enter:core", "exit:core", "enter:LocationChanged",
            "enter:Move", "enter:SizeChanged", "enter:Resize", "enter:ClientSizeChanged", "derived-constructor"];
        foreach (string point in points)
        {
            string mode = point is "enter:core" or "exit:core" || point.StartsWith("enter:", StringComparison.Ordinal)
                && point is not "enter:set:min" and not "enter:set:max" ? "empty-getters" : "normal";
            Observe(new Scenario("throw-" + point, new(83, 47), "fixed", Mode: mode, Action: "throw", Point: point));
            foreach (string action in new[] { "bounds", "client" })
            {
                foreach (Size size in new[] { new Size(83, 47), Size.Empty, new Size(0, 47), new Size(-3, -5) })
                {
                    Observe(new Scenario($"reenter-{point}-{action}-{Format(size)}", size, "fixed", Mode: mode, Action: action, Point: point));
                }
            }
        }

        foreach (bool autoInstall in new[] { false, true })
        {
            foreach (string context in new[] { "null", "base", "custom" })
            {
                foreach (string? point in new string?[] { null, "default:min", "default:max", "default:size", "derived-constructor" })
                {
                    Exception? error = null;
                    var thread = new Thread(() =>
                    {
                        try
                        {
                            WindowsFormsSynchronizationContext.AutoInstall = autoInstall;
                            SynchronizationContext.SetSynchronizationContext(context == "custom" ? new CustomContext()
                                : context == "base" ? new SynchronizationContext() : null);
                            Observe(new Scenario($"context-{autoInstall}-{context}-{point ?? "normal"}", new(83, 47), "fixed", Action: point is null ? "none" : "throw", Point: point));
                        }
                        catch (Exception exception) { error = exception; }
                    });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    if (!thread.Join(TimeSpan.FromSeconds(12))) { throw new TimeoutException("Constructor context case did not finish."); }
                    if (error is not null) { ExceptionDispatchInfo.Capture(error).Throw(); }
                }
            }
        }
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    [DynamicDependency("get_DefaultSize()", typeof(Control))]
    private static void RecordMetadata()
    {
        PropertyInfo property = typeof(Control).GetProperty("DefaultSize", BindingFlags.Instance | BindingFlags.NonPublic)!;
        MethodInfo getter = property.GetMethod!;
        Record("DefaultSize-type", property.PropertyType.Name);
        Record("DefaultSize-setter", property.SetMethod is null ? "none" : "present");
        Record("DefaultSize-index", property.GetIndexParameters().Length.ToString(CultureInfo.InvariantCulture));
        Record("DefaultSize-attributes", string.Join(";", property.CustomAttributes.Select(a => a.AttributeType.Name).Order(StringComparer.Ordinal)));
        Record("DefaultSize-signature", getter.ReturnType.Name + ":" + string.Join(",", getter.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name)));
        Record("DefaultSize-getter", $"public:{getter.IsPublic},family:{getter.IsFamily},family-or-assembly:{getter.IsFamilyOrAssembly},static:{getter.IsStatic},virtual:{getter.IsVirtual},final:{getter.IsFinal},abstract:{getter.IsAbstract},new-slot:{(getter.Attributes & MethodAttributes.NewSlot) != 0},base-self:{getter.GetBaseDefinition() == getter}");
        ConstructorInfo constructor = typeof(Control).GetConstructor(Type.EmptyTypes)!;
        Record("constructor-metadata", $"public:{constructor.IsPublic},static:{constructor.IsStatic},parameters:{constructor.GetParameters().Length}");
    }

    private static void Observe(Scenario scenario)
    {
        Current = scenario;
        Calls = [];
        ActionPoint = scenario.Point;
        MinimumReads = 0;
        MaximumReads = 0;
        TraceControl? control = null;
        Exception? failure = null;
        try { control = new TraceControl(); }
        catch (Exception exception) { failure = exception; }
        Record(scenario.Name + "-calls", string.Join(";", Calls));
        Record(scenario.Name + "-exception", failure is null ? "none" : ReferenceEquals(failure, Failure) ? "original" : failure.GetType().Name);
        Calls = null;
        ActionPoint = null;
        Record(scenario.Name + "-context", Context());
        if (control is not null)
        {
            Record(scenario.Name + "-state", State(control));
            Record(scenario.Name + "-constraints", "min:" + Format(control.MinimumSize) + "|max:" + Format(control.MaximumSize));
            Record(scenario.Name + "-base-default", Format(control.ReadBaseDefaultSize()));
            using (control) { }
        }
        Current = null;
    }

    private static void Point(Control control, string point, string? arguments = null)
    {
        if (Calls is null) { return; }
        Calls.Add(point + (arguments is null ? "" : ":" + arguments) + ":" + State(control));
        if (ActionPoint != point) { return; }
        ActionPoint = null;
        switch (Current!.Action)
        {
            case "throw": throw Failure;
            case "bounds": control.Bounds = new Rectangle(7, 9, 41, 23); break;
            case "client": control.ClientSize = new Size(19, 11); break;
        }
    }

    private static string State(Control control) => "bounds:" + Format(control.Bounds)
        + "|size:" + Format(control.Size) + "|client:" + Format(control.ClientSize)
        + "|client-rectangle:" + Format(control.ClientRectangle) + "|handle:" + control.IsHandleCreated
        + "|context:" + Context();
    private static string Context() => SynchronizationContext.Current is null ? "null"
        : SynchronizationContext.Current is WindowsFormsSynchronizationContext ? "forms"
        : SynchronizationContext.Current is CustomContext ? "custom" : SynchronizationContext.Current.GetType().Name;
    private static string Format(Size size) => $"{size.Width},{size.Height}";
    private static string Format(Rectangle rectangle) => $"{rectangle.X},{rectangle.Y},{rectangle.Width},{rectangle.Height}";
    private static void Record(string name, string value) => Results.Add(name + "=" + value);

    private sealed record Scenario(string Name, Size Default, string Profile = "empty", string Border = "none",
        string Mode = "normal", string Action = "none", string? Point = null)
    {
        internal Size Minimum => Profile switch
        {
            "fixed" => new(31, 17), "conflict" => new(101, 59), "negative" => new(-7, -9),
            "zero-axis" => new(0, 17), _ => Size.Empty
        };
        internal Size Maximum => Profile switch
        {
            "fixed" => new(101, 59), "conflict" => new(31, 17), "negative" => new(-3, -5),
            "zero-axis" => new(101, 0), _ => Size.Empty
        };
    }

    private sealed class CustomContext : SynchronizationContext { }

    private sealed class TraceControl : Control
    {
        internal TraceControl() => Point(this, "derived-constructor");
        internal Size ReadBaseDefaultSize() => base.DefaultSize;

        protected override Size DefaultSize
        {
            get
            {
                Point(this, "default:size");
                Size value = Current!.Mode == "base-size" ? base.DefaultSize : Current.Default;
                Point(this, "return:default:size", Format(value));
                return value;
            }
        }

        protected override Size DefaultMinimumSize
        {
            get
            {
                MinimumReads++;
                Point(this, "default:min");
                Size value = Current!.Mode == "changing-defaults" && MinimumReads > 1
                    ? MinimumReads == 2 ? new(43, 29) : new(47, 31) : Current.Minimum;
                Point(this, "return:default:min", Format(value));
                return value;
            }
        }

        protected override Size DefaultMaximumSize
        {
            get
            {
                MaximumReads++;
                Point(this, "default:max");
                Size value = Current!.Mode == "changing-defaults" && MaximumReads > 1
                    ? MaximumReads == 2 ? new(127, 67) : new(131, 71) : Current.Maximum;
                Point(this, "return:default:max", Format(value));
                return value;
            }
        }

        public override Size MinimumSize
        {
            get { Point(this, "get:min"); return Current!.Mode == "empty-getters" ? Size.Empty : Current.Mode == "fixed-getters" ? new Size(31, 17) : base.MinimumSize; }
            set
            {
                Point(this, "enter:set:min", Format(value));
                if (Current!.Mode != "skip-min-setter") { base.MinimumSize = value; }
                Point(this, "exit:set:min", Format(value));
            }
        }

        public override Size MaximumSize
        {
            get { Point(this, "get:max"); return Current!.Mode == "empty-getters" ? Size.Empty : Current.Mode == "fixed-getters" ? new Size(101, 59) : base.MaximumSize; }
            set
            {
                Point(this, "enter:set:max", Format(value));
                if (Current!.Mode != "skip-max-setter") { base.MaximumSize = value; }
                Point(this, "exit:set:max", Format(value));
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            string arguments = $"{x},{y},{width},{height},{(int)specified}";
            Point(this, "enter:core", arguments);
            if (Current!.Mode != "skip-core") { base.SetBoundsCore(x, y, width, height, specified); }
            Point(this, "exit:core", arguments);
        }

        protected override void OnLocationChanged(EventArgs e) { Point(this, "enter:LocationChanged"); base.OnLocationChanged(e); Point(this, "exit:LocationChanged"); }
        protected override void OnMove(EventArgs e) { Point(this, "enter:Move"); base.OnMove(e); Point(this, "exit:Move"); }
        protected override void OnSizeChanged(EventArgs e) { Point(this, "enter:SizeChanged"); base.OnSizeChanged(e); Point(this, "exit:SizeChanged"); }
        protected override void OnResize(EventArgs e) { Point(this, "enter:Resize"); base.OnResize(e); Point(this, "exit:Resize"); }
        protected override void OnClientSizeChanged(EventArgs e) { Point(this, "enter:ClientSizeChanged"); base.OnClientSizeChanged(e); Point(this, "exit:ClientSizeChanged"); }

#if REFERENCE
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parameters = base.CreateParams;
                if (Current!.Border is "border" or "both") { parameters.Style |= 0x00800000; }
                if (Current.Border is "edge" or "both") { parameters.ExStyle |= 0x00000200; }
                return parameters;
            }
        }
        protected override void OnHandleCreated(EventArgs e) { Point(this, "HandleCreated"); base.OnHandleCreated(e); }
#else
        protected override uint Style => base.Style | (Current!.Border is "border" or "both" ? 0x00800000u : 0u);
        protected override uint ExStyle => base.ExStyle | (Current!.Border is "edge" or "both" ? 0x00000200u : 0u);
        protected override void OnHandleCreated() { Point(this, "HandleCreated"); base.OnHandleCreated(); }
        protected override void OnBoundsChanged() { }
#endif
    }
}
