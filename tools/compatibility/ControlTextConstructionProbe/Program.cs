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
    private static readonly InvalidOperationException Failure = new("text construction probe");
    [ThreadStatic] private static Scenario? Current;
    [ThreadStatic] private static List<string>? Calls;
    [ThreadStatic] private static string? ActionPoint;
    [ThreadStatic] private static TraceControl? Observed;
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

        string?[] texts = [null, "", "plain", "virtual-value", "中文🙂", "line\r\nnext\n", "\0", "before\0after"];
        Rectangle[] rectangles = [new(7, 9, 83, 47), Rectangle.Empty, new(0, 0, 0, 47), new(0, 0, 83, 0),
            new(-7, -9, -3, -5), new(-1, 1, -3, 37), new(1, -1, 73, -5), new(1, 1, 1, 1),
            new(int.MinValue, int.MaxValue, int.MinValue, -1), new(int.MaxValue, int.MinValue, -1, int.MinValue),
            new(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue),
            new(int.MinValue, int.MinValue, int.MinValue, int.MaxValue),
            new(int.MaxValue - 1, int.MinValue + 1, int.MaxValue - 1, int.MinValue + 1)];
        foreach (string? text in texts)
        {
            using (var control = new Control(text)) { Record("base-text-" + Encode(text), State(control)); }
            foreach (Rectangle rectangle in rectangles)
            {
                using var control = new Control(text, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                Record("base-bounds-" + Encode(text) + "-" + Format(rectangle), State(control));
            }
        }

        foreach (bool withBounds in new[] { false, true })
        {
            foreach (string? text in texts)
            {
                foreach (string profile in new[] { "empty", "fixed", "conflict", "negative", "zero-axis" })
                {
                    Observe(new Scenario($"normal-{withBounds}-{profile}-{Encode(text)}", withBounds, text,
                        new(7, 9, 83, 47), Profile: profile));
                }
                foreach (string mode in new[] { "skip-text-setter", "fixed-text-getter", "null-text-getter", "text-suffix",
                    "skip-text-notification", "skip-core", "skip-min-setter", "skip-max-setter", "empty-getters", "changing-defaults", "hidden-text" })
                {
                    Observe(new Scenario($"mode-{withBounds}-{mode}-{Encode(text)}", withBounds, text,
                        new(7, 9, 83, 47), Profile: "fixed", Mode: mode));
                }
            }
            foreach (string border in new[] { "none", "border", "edge", "both" })
            {
                foreach (Rectangle rectangle in rectangles)
                {
                    foreach (Size size in new[] { Size.Empty, new Size(83, 47), new Size(0, 47), new Size(-3, -5) })
                    {
                        Observe(new Scenario($"geometry-{withBounds}-{border}-{Format(rectangle)}-{Format(size)}",
                            withBounds, "geometry", rectangle, Default: size, Border: border));
                    }
                }
            }
        }

        string[] points = ["default:min", "default:max", "default:size", "enter:set:text", "get:text", "exit:set:text",
            "enter:TextChanged", "event:TextChanged", "exit:TextChanged", "enter:core", "exit:core",
            "enter:LocationChanged", "enter:Move", "enter:SizeChanged", "enter:Resize", "enter:ClientSizeChanged", "derived-constructor"];
        foreach (bool withBounds in new[] { false, true })
        {
            foreach (string point in points)
            {
                Observe(new Scenario($"throw-{withBounds}-{point}", withBounds, "outer", new(7, 9, 83, 47),
                    Profile: "fixed", Action: "throw", Point: point));
                foreach (string action in new[] { "text", "bounds", "client" })
                {
                    Observe(new Scenario($"reenter-{withBounds}-{point}-{action}", withBounds, "outer", new(7, 9, 83, 47),
                        Profile: "fixed", Action: action, Point: point));
                }
            }
        }

        foreach (bool autoInstall in new[] { false, true })
        {
            foreach (string context in new[] { "null", "base", "custom" })
            {
                foreach (bool withBounds in new[] { false, true })
                {
                    foreach (string? point in new string?[] { null, "default:size", "enter:set:text", "get:text", "enter:TextChanged", "enter:core", "derived-constructor" })
                    {
                        Exception? failure = null;
                        var thread = new Thread(() =>
                        {
                            try
                            {
                                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                                WindowsFormsSynchronizationContext.AutoInstall = autoInstall;
                                SynchronizationContext.SetSynchronizationContext(context == "custom" ? new CustomContext()
                                    : context == "base" ? new SynchronizationContext() : null);
                                Observe(new Scenario($"context-{autoInstall}-{context}-{withBounds}-{point ?? "normal"}",
                                    withBounds, "context", new(7, 9, 83, 47), Profile: "fixed",
                                    Action: point is null ? "none" : "throw", Point: point));
                            }
                            catch (Exception exception) { failure = exception; }
                        }) { IsBackground = true };
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                        if (!thread.Join(TimeSpan.FromSeconds(12))) { throw new TimeoutException("Text constructor context case did not finish."); }
                        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
                    }
                }
            }
        }
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    [DynamicDependency("#ctor(System.String)", typeof(Control))]
    [DynamicDependency("#ctor(System.String,System.Int32,System.Int32,System.Int32,System.Int32)", typeof(Control))]
    [DynamicDependency("get_Text()", typeof(Control))]
    [DynamicDependency("set_Text(System.String)", typeof(Control))]
    private static void RecordMetadata()
    {
        foreach (Type[] parameters in new[] { new[] { typeof(string) }, new[] { typeof(string), typeof(int), typeof(int), typeof(int), typeof(int) } })
        {
            ConstructorInfo constructor = typeof(Control).GetConstructor(parameters)!;
            string name = "constructor-" + parameters.Length;
            CallingConventions calling = constructor.CallingConvention;
            const CallingConventions knownCalling = CallingConventions.Standard | CallingConventions.VarArgs
                | CallingConventions.HasThis | CallingConventions.ExplicitThis;
            // Native AOT reports HasThis without CoreCLR's additional Standard
            // bit. Compare the actual instance/varargs contract and retain each
            // runtime's unmodified encoding in stderr; never manufacture a bit.
            Record(name + "-flags", $"public:{constructor.IsPublic},static:{constructor.IsStatic},constructor:{constructor.IsConstructor},instance:{!constructor.IsStatic},has-this:{(calling & CallingConventions.HasThis) != 0},explicit-this:{(calling & CallingConventions.ExplicitThis) != 0},varargs:{(calling & CallingConventions.VarArgs) != 0},unknown-calling-flags:{(int)(calling & ~knownCalling)}");
            Console.Error.WriteLine(name + "-calling-convention-encoding=raw:" + calling + "|value:" + (int)calling
                + "|standard-bit:" + ((calling & CallingConventions.Standard) != 0));
            // The compiler may encode the same nullable context on the type or
            // on an individual constructor. Keep its raw placement as evidence
            // while comparing the parameter's effective source contract below.
            Record(name + "-attributes", Attributes(constructor.CustomAttributes.Where(attribute =>
                attribute.AttributeType.FullName != "System.Runtime.CompilerServices.NullableContextAttribute")));
            Record(name + "-parameters", string.Join(";", constructor.GetParameters().Select(p =>
                $"{p.Position}:{p.ParameterType.Name}:{p.Name}:flags:{(int)p.Attributes}:optional:{p.IsOptional}:default:{p.HasDefaultValue}:attributes:{Attributes(p.CustomAttributes)}")));
            (byte flag, string source) = TextNullability(constructor);
            Record(name + "-text-effective-nullability", flag switch
            {
                0 => "oblivious", 1 => "not-null", 2 => "nullable", _ => throw new InvalidOperationException("Unexpected nullable metadata flag.")
            });
            Console.Error.WriteLine(name + "-nullable-encoding=constructor-attributes:" + Attributes(constructor.CustomAttributes)
                + "|text-attributes:" + Attributes(constructor.GetParameters()[0].CustomAttributes)
                + "|type-contexts:" + string.Join(";", DeclaringTypes(constructor.DeclaringType).Select(type =>
                    type.Name + ":" + (NullableFlag(type.CustomAttributes, "NullableContextAttribute")?.ToString(CultureInfo.InvariantCulture) ?? "none")))
                + "|effective:" + flag + "|source:" + source);
        }
        PropertyInfo property = typeof(Control).GetProperty(nameof(Control.Text))!;
        Record("Text-type", property.PropertyType.Name);
        Record("Text-index", property.GetIndexParameters().Length.ToString(CultureInfo.InvariantCulture));
        foreach (MethodInfo method in new[] { property.GetMethod!, property.SetMethod! })
        {
            Record(method.Name + "-flags", $"public:{method.IsPublic},static:{method.IsStatic},virtual:{method.IsVirtual},final:{method.IsFinal},abstract:{method.IsAbstract},new-slot:{(method.Attributes & MethodAttributes.NewSlot) != 0},base-self:{method.GetBaseDefinition() == method}");
        }
    }

    private static string Attributes(IEnumerable<CustomAttributeData> attributes) => string.Join(";", attributes.Select(a =>
        a.AttributeType.Name + "(" + string.Join(",", a.ConstructorArguments.Select(v => v.Value?.ToString() ?? "null")) + ")").Order(StringComparer.Ordinal));

    private static (byte Flag, string Source) TextNullability(ConstructorInfo constructor)
    {
        byte? parameter = NullableFlag(constructor.GetParameters()[0].CustomAttributes, "NullableAttribute");
        if (parameter is not null) { return (parameter.Value, "parameter"); }
        byte? method = NullableFlag(constructor.CustomAttributes, "NullableContextAttribute");
        if (method is not null) { return (method.Value, "constructor"); }
        foreach (Type type in DeclaringTypes(constructor.DeclaringType))
        {
            byte? context = NullableFlag(type.CustomAttributes, "NullableContextAttribute");
            if (context is not null) { return (context.Value, "type:" + type.Name); }
        }
        return (0, "none");
    }

    private static IEnumerable<Type> DeclaringTypes(Type? type)
    {
        for (; type is not null; type = type.DeclaringType) { yield return type; }
    }

    private static byte? NullableFlag(IEnumerable<CustomAttributeData> attributes, string name)
    {
        CustomAttributeData? attribute = attributes.FirstOrDefault(data => data.AttributeType.FullName == "System.Runtime.CompilerServices." + name);
        if (attribute is null || attribute.ConstructorArguments.Count == 0) { return null; }
        object? value = attribute.ConstructorArguments[0].Value;
        if (value is IEnumerable<CustomAttributeTypedArgument> flags) { value = flags.FirstOrDefault().Value; }
        return value is byte flag ? flag : null;
    }

    private static void Observe(Scenario scenario)
    {
        Current = scenario;
        Calls = [];
        ActionPoint = scenario.Point;
        Observed = null;
        MinimumReads = 0;
        MaximumReads = 0;
        TraceControl? control = null;
        Exception? failure = null;
        try
        {
            Rectangle r = scenario.Rectangle;
            control = scenario.Mode == "hidden-text"
                ? scenario.WithBounds ? new HiddenTextControl(scenario.Text, r.X, r.Y, r.Width, r.Height) : new HiddenTextControl(scenario.Text)
                : scenario.WithBounds ? new TraceControl(scenario.Text, r.X, r.Y, r.Width, r.Height) : new TraceControl(scenario.Text);
        }
        catch (Exception exception) { failure = exception; }
        Record(scenario.Name + "-calls", string.Join(";", Calls));
        Record(scenario.Name + "-exception", failure is null ? "none" : ReferenceEquals(failure, Failure) ? "original" : failure.GetType().Name);
        Calls = null;
        ActionPoint = null;
        Record(scenario.Name + "-context", Context());
        TraceControl? partial = control ?? Observed;
        if (partial is not null)
        {
            Record(scenario.Name + "-state", State(partial));
            Record(scenario.Name + "-virtual-text", Encode(partial.Text));
            using (partial) { }
        }
        Current = null;
    }

    private static void Point(TraceControl control, string point, string? arguments = null)
    {
        if (Calls is null) { return; }
        if (Observed is null)
        {
            Observed = control;
            control.TextChanged += (_, _) => Point(control, "event:TextChanged");
            control.LocationChanged += (_, _) => Point(control, "event:LocationChanged");
            control.Move += (_, _) => Point(control, "event:Move");
            control.SizeChanged += (_, _) => Point(control, "event:SizeChanged");
            control.Resize += (_, _) => Point(control, "event:Resize");
            control.ClientSizeChanged += (_, _) => Point(control, "event:ClientSizeChanged");
            control.HandleCreated += (_, _) => Point(control, "event:HandleCreated");
        }
        Calls.Add(point + (arguments is null ? "" : ":" + arguments) + ":" + State(control));
        if (ActionPoint != point) { return; }
        ActionPoint = null;
        switch (Current!.Action)
        {
            case "throw": throw Failure;
            case "text": control.Text = "reentered"; break;
            case "bounds": control.Bounds = new Rectangle(17, 19, 41, 23); break;
            case "client": control.ClientSize = new Size(19, 11); break;
        }
    }

    private static string State(Control control) => "text:" + Encode(control is TraceControl trace ? trace.BaseText : control.Text)
        + "|bounds:" + Format(control.Bounds) + "|client:" + Format(control.ClientSize)
        + "|client-rectangle:" + Format(control.ClientRectangle) + "|handle:" + control.IsHandleCreated
        + "|context:" + Context();
    private static string Context() => SynchronizationContext.Current is null ? "null"
        : SynchronizationContext.Current is WindowsFormsSynchronizationContext ? "forms"
        : SynchronizationContext.Current is CustomContext ? "custom" : SynchronizationContext.Current.GetType().Name;
    private static string Encode(string? value) => value is null ? "null" : value.Length + ":" + Convert.ToHexString(Encoding.Unicode.GetBytes(value));
    private static string Format(Size size) => $"{size.Width},{size.Height}";
    private static string Format(Rectangle rectangle) => $"{rectangle.X},{rectangle.Y},{rectangle.Width},{rectangle.Height}";
    private static void Record(string name, string value) => Results.Add(name + "=" + value);

    private sealed record Scenario(string Name, bool WithBounds, string? Text, Rectangle Rectangle,
        Size Default = default, string Profile = "empty", string Border = "none", string Mode = "normal",
        string Action = "none", string? Point = null)
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

    private class TraceControl : Control
    {
        internal TraceControl(string? text) : base(text) => Point(this, "derived-constructor");
        internal TraceControl(string? text, int left, int top, int width, int height) : base(text, left, top, width, height)
            => Point(this, "derived-constructor");
        internal string BaseText => base.Text;

        [AllowNull]
        public override string Text
        {
            get
            {
                Point(this, "get:text");
                return Current!.Mode == "fixed-text-getter" ? "virtual-value"
                    : Current.Mode == "null-text-getter" ? null! : base.Text;
            }
            set
            {
                Point(this, "enter:set:text", Encode(value));
                if (Current!.Mode != "skip-text-setter") { base.Text = Current.Mode == "text-suffix" ? value + "-suffix" : value; }
                Point(this, "exit:set:text", Encode(value));
            }
        }

        protected override Size DefaultSize { get { Point(this, "default:size"); return Current!.Default; } }
        protected override Size DefaultMinimumSize
        {
            get
            {
                MinimumReads++;
                Point(this, "default:min");
                return Current!.Mode == "changing-defaults" && MinimumReads > 1
                    ? MinimumReads == 2 ? new Size(43, 29) : new Size(47, 31) : Current.Minimum;
            }
        }
        protected override Size DefaultMaximumSize
        {
            get
            {
                MaximumReads++;
                Point(this, "default:max");
                return Current!.Mode == "changing-defaults" && MaximumReads > 1
                    ? MaximumReads == 2 ? new Size(127, 67) : new Size(131, 71) : Current.Maximum;
            }
        }
        public override Size MinimumSize
        {
            get { Point(this, "get:min"); return Current!.Mode == "empty-getters" ? Size.Empty : base.MinimumSize; }
            set { Point(this, "enter:set:min", Format(value)); if (Current!.Mode != "skip-min-setter") { base.MinimumSize = value; } Point(this, "exit:set:min", Format(value)); }
        }
        public override Size MaximumSize
        {
            get { Point(this, "get:max"); return Current!.Mode == "empty-getters" ? Size.Empty : base.MaximumSize; }
            set { Point(this, "enter:set:max", Format(value)); if (Current!.Mode != "skip-max-setter") { base.MaximumSize = value; } Point(this, "exit:set:max", Format(value)); }
        }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            string arguments = $"{x},{y},{width},{height},{(int)specified}";
            Point(this, "enter:core", arguments);
            if (Current!.Mode != "skip-core") { base.SetBoundsCore(x, y, width, height, specified); }
            Point(this, "exit:core", arguments);
        }
        protected override void OnTextChanged(EventArgs e) { Point(this, "enter:TextChanged"); if (Current!.Mode != "skip-text-notification") { base.OnTextChanged(e); } Point(this, "exit:TextChanged"); }
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

    private sealed class HiddenTextControl : TraceControl
    {
        internal HiddenTextControl(string? text) : base(text) { }
        internal HiddenTextControl(string? text, int left, int top, int width, int height) : base(text, left, top, width, height) { }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get => throw Failure; set => throw Failure; }
    }
}
