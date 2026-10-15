using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

internal static class Program
{
    private static readonly List<string> Results = [];
    private static readonly Rectangle Initial = new(11, 13, 73, 37);
    private static Exception? NativeException;
    private static List<string>? ConstructorCalls;
    private static string? ConstructorActionPoint;
    private static readonly InvalidOperationException ConstructorFailure = new("probe constructor defaults");

    [STAThread]
    private static void Main(string[] args)
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
        }
#endif
        if (args is ["--constructors"])
        {
            RunConstructorCases();
            Console.WriteLine(string.Join(Environment.NewLine, Results));
            return;
        }
        if (args.Length != 0) { throw new ArgumentException("The supported optional argument is --constructors.", nameof(args)); }
        Application.ThreadException += (_, e) => NativeException = e.Exception;
        RecordMetadata();
        foreach (string border in new[] { "none", "border", "edge", "both" })
        {
            RunCases(border, native: false);
            RunCases(border, native: true);
        }
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    [DynamicDependency("get_MinimumSize()", typeof(Control))]
    [DynamicDependency("set_MinimumSize(System.Drawing.Size)", typeof(Control))]
    [DynamicDependency("get_MaximumSize()", typeof(Control))]
    [DynamicDependency("set_MaximumSize(System.Drawing.Size)", typeof(Control))]
    [DynamicDependency("get_DefaultMinimumSize()", typeof(Control))]
    [DynamicDependency("get_DefaultMaximumSize()", typeof(Control))]
    private static void RecordMetadata()
    {
        foreach (string name in new[] { "MinimumSize", "MaximumSize", "DefaultMinimumSize", "DefaultMaximumSize" })
        {
            PropertyInfo property = typeof(Control).GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;
            Record(name + "-type", property.PropertyType.Name);
            foreach (MethodInfo method in property.GetAccessors(nonPublic: true))
            {
                Record(method.Name + "-signature", method.ReturnType.Name + ":" + string.Join(",", method.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name)));
                Record(method.Name + "-metadata", $"public:{method.IsPublic},family:{method.IsFamily},family-or-assembly:{method.IsFamilyOrAssembly},static:{method.IsStatic},virtual:{method.IsVirtual},final:{method.IsFinal},new-slot:{(method.Attributes & MethodAttributes.NewSlot) != 0}");
            }
            Record(name + "-category", property.GetCustomAttribute<CategoryAttribute>()?.Category ?? "none");
            Record(name + "-description", property.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "none");
            Record(name + "-localizable", property.GetCustomAttribute<LocalizableAttribute>()?.IsLocalizable.ToString() ?? "none");
            Record(name + "-default", AttributeValue(property.GetCustomAttribute<DefaultValueAttribute>()?.Value));
            Record(name + "-ambient", AmbientMetadata(property));
            Record(name + "-browsable", property.GetCustomAttribute<BrowsableAttribute>()?.Browsable.ToString() ?? "none");
            Record(name + "-editor", property.GetCustomAttribute<EditorBrowsableAttribute>()?.State.ToString() ?? "none");
            Record(name + "-serialization", property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()?.Visibility.ToString() ?? "none");
        }
    }

    private static string AttributeValue(object? value) => value is Size size ? Format(size) : value?.ToString() ?? "none";

    private static string AmbientMetadata(PropertyInfo property)
    {
        // Read the declared Type/string arguments without asking AmbientValueAttribute
        // to instantiate a converter through a runtime-only reflection path.
        CustomAttributeData? attribute = property.CustomAttributes.SingleOrDefault(data => data.AttributeType == typeof(AmbientValueAttribute));
        return attribute is null ? "none" : string.Join(";", attribute.ConstructorArguments.Select(argument =>
            argument.Value is Type type ? type.Name : argument.Value?.ToString() ?? "null"));
    }

    private static void RunConstructorCases()
    {
        // This separate evidence group records the known constructor/DefaultSize
        // gap for Control-01b. It is excluded from the current runtime comparison.
        foreach (string kind in new[] { "empty", "fixed", "negative" })
        {
            foreach (string? point in new string?[] { null, "default:min", "default:max", "get:min", "get:max", "set:min", "set:max" })
            {
                string name = "constructor-" + kind + "-" + (point ?? "normal");
                ConstructorCalls = [];
                ConstructorActionPoint = point;
                ConstructorTraceControl? control = null;
                Exception? failure = null;
                try
                {
                    control = kind == "fixed" ? new FixedConstructorControl()
                        : kind == "negative" ? new NegativeConstructorControl() : new ConstructorTraceControl();
                }
                catch (Exception error) { failure = error; }
                string calls = string.Join(";", ConstructorCalls);
                ConstructorCalls = null;
                ConstructorActionPoint = null;
                Record(name + "-calls", calls);
                Record(name + "-exception", failure is null ? "none" : ReferenceEquals(failure, ConstructorFailure) ? "original" : failure.GetType().Name);
                if (control is not null)
                {
                    using (control)
                    {
                        Record(name + "-constraints", "min:" + Format(control.MinimumSize) + "|max:" + Format(control.MaximumSize));
                        Record(name + "-state", BoundsState(control));
                    }
                }
            }
        }
    }

    private static void ConstructorPoint(Control control, string point, Size? size = null)
    {
        if (ConstructorCalls is null) { return; }
        ConstructorCalls.Add(point + ":" + (size is Size value ? Format(value) + ":" : "") + BoundsState(control));
        if (ConstructorActionPoint == point)
        {
            ConstructorActionPoint = null;
            throw ConstructorFailure;
        }
    }

    private class ConstructorTraceControl : Control
    {
        internal ConstructorTraceControl() => ConstructorPoint(this, "derived-constructor");
        public override Size MinimumSize
        {
            get { ConstructorPoint(this, "get:min"); return base.MinimumSize; }
            set { ConstructorPoint(this, "set:min", value); base.MinimumSize = value; }
        }
        public override Size MaximumSize
        {
            get { ConstructorPoint(this, "get:max"); return base.MaximumSize; }
            set { ConstructorPoint(this, "set:max", value); base.MaximumSize = value; }
        }
        protected override Size DefaultMinimumSize { get { ConstructorPoint(this, "default:min"); return Size.Empty; } }
        protected override Size DefaultMaximumSize { get { ConstructorPoint(this, "default:max"); return Size.Empty; } }
#if !REFERENCE
        protected override void OnBoundsChanged() { }
#endif
    }

    private sealed class FixedConstructorControl : ConstructorTraceControl
    {
        protected override Size DefaultMinimumSize { get { ConstructorPoint(this, "default:min"); return new Size(31, 17); } }
        protected override Size DefaultMaximumSize { get { ConstructorPoint(this, "default:max"); return new Size(101, 59); } }
    }

    private sealed class NegativeConstructorControl : ConstructorTraceControl
    {
        protected override Size DefaultMinimumSize { get { ConstructorPoint(this, "default:min"); return new Size(-7, -9); } }
        protected override Size DefaultMaximumSize { get { ConstructorPoint(this, "default:max"); return new Size(-3, -5); } }
    }

    private static void RunCases(string border, bool native)
    {
        string prefix = border + (native ? "-native" : "-uncreated");
        using var form = native ? new Form { Text = "Lumina size constraints probe", Width = 320, Height = 240 } : null;
        if (native) { form!.Show(); }

        using (TraceControl control = Create(form, border))
        {
            Observe(prefix + "-defaults", control, () => { });
        }

        Size[] values = [new(83, 47), Size.Empty, new(-3, -5), new(-3, 37), new(73, -5), new(0, 47), new(83, 0)];
        foreach (string property in new[] { "min", "max" })
        {
            foreach (Size value in values)
            {
                using TraceControl control = Create(form, border);
                string name = prefix + "-assign-" + property + "-" + Format(value);
                Observe(name, control, () => Assign(control, property, value));
                Observe(name + "-repeat", control, () => Assign(control, property, value));
                Observe(name + "-negative-size", control, () => control.Size = new Size(-7, -9));
                Observe(name + "-cancel", control, () => Assign(control, property, Size.Empty));
                Observe(name + "-after-cancel", control, () => control.Size = new Size(-7, -9));
            }
        }

        foreach (string order in new[] { "min-max", "max-min" })
        {
            foreach ((Size minimum, Size maximum) in new[]
            {
                (new Size(101, 61), new Size(43, 29)), (new Size(101, 0), new Size(0, 29)),
                (new Size(-3, 61), new Size(43, -5)), (new Size(-3, -5), new Size(-7, -9))
            })
            {
                using TraceControl control = Create(form, border);
                string name = prefix + "-conflict-" + order + "-" + Format(minimum) + "-" + Format(maximum);
                Observe(name + "-first", control, () => Assign(control, order == "min-max" ? "min" : "max", order == "min-max" ? minimum : maximum));
                Observe(name + "-second", control, () => Assign(control, order == "min-max" ? "max" : "min", order == "min-max" ? maximum : minimum));
                Observe(name + "-bounds", control, () => control.Bounds = new Rectangle(17, 19, 211, 151));
                Observe(name + "-cancel-min", control, () => control.MinimumSize = Size.Empty);
                Observe(name + "-cancel-max", control, () => control.MaximumSize = Size.Empty);
            }
        }

        string[] entries = ["bounds", "size", "width", "height", "set4", "set5-size", "set5-x", "set5-none", "set5-unknown", "core", "client", "client-core"];
        foreach (string profile in new[] { "none", "min", "max", "both" })
        {
            using (TraceControl control = Create(form, border))
            {
                SetProfile(control, profile);
                Observe(prefix + "-core-equal-" + profile, control, () => control.CallCore(control.Left, control.Top, control.Width, control.Height, BoundsSpecified.All));
            }
            foreach (string entry in entries)
            {
                foreach (Size input in new[] { new Size(19, 11), new Size(97, 61), new Size(-3, -5) })
                {
                    using TraceControl control = Create(form, border);
                    SetProfile(control, profile);
                    string name = prefix + "-route-" + profile + "-" + entry + "-" + Format(input);
                    Observe(name, control, () => Apply(control, entry, input));
                    Observe(name + "-repeat", control, () => Apply(control, entry, input));
                }
            }
        }

        foreach (string mode in new[] { "fixed", "conflict", "negative" })
        {
            foreach (string entry in new[] { "min", "max", "size", "core", "client-core" })
            {
                using TraceControl control = Create(form, border);
                control.GetterMode = mode;
                string name = prefix + "-getter-" + mode + "-" + entry;
                Observe(name, control, () => Apply(control, entry, new Size(97, 61)));
                Observe(name + "-repeat", control, () => Apply(control, entry, new Size(97, 61)));
            }
        }

        foreach (string mode in new[] { "fixed", "negative" })
        {
            using TraceControl control = Create(form, border);
            control.DefaultMode = mode;
            string name = prefix + "-default-" + mode;
            Observe(name + "-read", control, () => { _ = control.MinimumSize; _ = control.MaximumSize; });
            Observe(name + "-size", control, () => control.Size = new Size(19, 11));
            Observe(name + "-assign-min", control, () => control.MinimumSize = new Size(83, 47));
            Observe(name + "-assign-max", control, () => control.MaximumSize = new Size(97, 61));
            control.DefaultMode = "base";
            Observe(name + "-defaults-changed", control, () => { _ = control.MinimumSize; _ = control.MaximumSize; });
            Observe(name + "-cancel-min", control, () => control.MinimumSize = Size.Empty);
            Observe(name + "-cancel-max", control, () => control.MaximumSize = Size.Empty);
            control.DefaultMode = mode;
            Observe(name + "-defaults-restored", control, () => { _ = control.MinimumSize; _ = control.MaximumSize; });
        }

        foreach (string mode in new[] { "setter-skip", "core-skip" })
        {
            using TraceControl control = Create(form, border);
            control.SetterSkip = mode == "setter-skip";
            control.CoreSkip = mode == "core-skip";
            Observe(prefix + "-" + mode + "-min", control, () => control.MinimumSize = new Size(83, 47));
            Observe(prefix + "-" + mode + "-max", control, () => control.MaximumSize = new Size(43, 29));
            Observe(prefix + "-" + mode + "-size", control, () => control.Size = new Size(19, 11));
            Observe(prefix + "-" + mode + "-client", control, () => control.ClientSize = new Size(19, 11));
        }

        string[] points = ["get:min", "get:max", "default:min", "default:max", "enter:set:min", "exit:set:min", "enter:set:max", "exit:set:max", "enter:core", "exit:core", "event:Resize", "event:SizeChanged", "event:ClientSizeChanged"];
        foreach (string point in points)
        {
            foreach (string entry in new[] { "min", "max", "size", "core", "client-core" })
            {
                using TraceControl control = Create(form, border);
                SetProfile(control, "both");
                control.ActionPoint = point;
                control.AtPoint = () => throw control.Failure;
                string name = prefix + "-throw-" + point + "-" + entry;
                Observe(name, control, () => Apply(control, entry, new Size(101, 61)));
                Observe(name + "-after", control, () => Apply(control, entry, new Size(101, 61)));
            }
        }

        foreach (string point in new[] { "get:min", "get:max", "enter:core", "exit:core", "event:SizeChanged", "event:ClientSizeChanged" })
        {
            foreach (string entry in new[] { "min", "max", "client-core" })
            {
                foreach (string nested in new[] { "min", "max", "size" })
                {
                    using TraceControl control = Create(form, border);
                    SetProfile(control, "both");
                    control.ActionPoint = point;
                    control.AtPoint = () => Apply(control, nested, new Size(47, 31));
                    string name = prefix + "-reenter-" + point + "-" + entry + "-" + nested;
                    Observe(name, control, () => Apply(control, entry, new Size(101, 61)));
                    Observe(name + "-after", control, () => control.SetBounds(23, 29, control.Width, control.Height, BoundsSpecified.Location));
                }
            }
        }
    }

    private static void SetProfile(TraceControl control, string profile)
    {
        if (profile is "min" or "both") { control.MinimumSize = new Size(41, 23); }
        if (profile is "max" or "both") { control.MaximumSize = new Size(83, 47); }
        control.Calls.Clear();
    }

    private static void Assign(TraceControl control, string property, Size value)
    {
        if (property == "min") { control.MinimumSize = value; }
        else { control.MaximumSize = value; }
    }

    private static void Apply(TraceControl control, string entry, Size input)
    {
        if (entry is "min" or "max") { Assign(control, entry, input); }
        else if (entry == "bounds") { control.Bounds = new Rectangle(17, 19, input.Width, input.Height); }
        else if (entry == "size") { control.Size = input; }
        else if (entry == "width") { control.Width = input.Width; }
        else if (entry == "height") { control.Height = input.Height; }
        else if (entry == "set4") { control.SetBounds(17, 19, input.Width, input.Height); }
        else if (entry == "set5-size") { control.SetBounds(17, 19, input.Width, input.Height, BoundsSpecified.Size); }
        else if (entry == "set5-x") { control.SetBounds(17, 19, input.Width, input.Height, BoundsSpecified.X); }
        else if (entry == "set5-none") { control.SetBounds(17, 19, input.Width, input.Height, BoundsSpecified.None); }
        else if (entry == "set5-unknown") { control.SetBounds(17, 19, input.Width, input.Height, (BoundsSpecified)257); }
        else if (entry == "core") { control.CallCore(17, 19, input.Width, input.Height, BoundsSpecified.None); }
        else if (entry == "client") { control.ClientSize = input; }
        else { control.CallClientCore(input.Width, input.Height); }
    }

    private static TraceControl Create(Form? form, string border)
    {
        var control = new TraceControl(border) { Bounds = Initial };
        if (form is not null) { form.Controls.Add(control); }
        control.Calls.Clear();
        return control;
    }

    private static void Observe(string name, TraceControl control, Action action)
    {
        control.Calls.Clear();
        NativeException = null;
        nint previousHandle = control.IsHandleCreated ? control.Handle : 0;
        string before = State(control);
        Exception? thrown = null;
        try { action(); }
        catch (Exception error) { thrown = error; }
        Record(name + "-before", before);
        Record(name + "-calls", string.Join(";", control.Calls));
        Record(name + "-state", State(control));
        Record(name + "-exception", ExceptionValue(thrown, control));
        Record(name + "-thread-exception", ExceptionValue(NativeException, control));
        Record(name + "-handle-preserved", control.IsHandleCreated == (previousHandle != 0)
            && (previousHandle == 0 || control.Handle == previousHandle));
    }

    private static string ExceptionValue(Exception? exception, TraceControl control)
        => exception is null ? "none" : ReferenceEquals(exception, control.Failure) ? "original"
            : exception.GetType().Name + (exception is ArgumentException argument ? ":" + argument.ParamName : "");

    private static string State(TraceControl control)
    {
        control.Suppress++;
        try
        {
            string state = BoundsState(control) + "|min:" + Format(control.MinimumSize) + "|max:" + Format(control.MaximumSize)
                + "|base-min:" + Format(control.BaseMinimum) + "|base-max:" + Format(control.BaseMaximum);
            if (control.IsHandleCreated)
            {
                if (!GetWindowRect(control.Handle, out NativeRect window)) { throw new Win32Exception(Marshal.GetLastWin32Error(), "GetWindowRect failed."); }
                if (!GetClientRect(control.Handle, out NativeRect client)) { throw new Win32Exception(Marshal.GetLastWin32Error(), "GetClientRect failed."); }
                state += "|native-window:" + (window.Right - window.Left) + "," + (window.Bottom - window.Top)
                    + "|native-client:" + Format(new Rectangle(client.Left, client.Top, client.Right - client.Left, client.Bottom - client.Top));
            }
            return state;
        }
        finally { control.Suppress--; }
    }

    private static string BoundsState(Control control) => "bounds:" + Format(control.Bounds) + "|client:" + Format(control.ClientRectangle)
        + "|client-size:" + Format(control.ClientSize) + "|handle:" + control.IsHandleCreated;
    private static string Format(Rectangle rectangle) => $"{rectangle.X},{rectangle.Y},{rectangle.Width},{rectangle.Height}";
    private static string Format(Size size) => $"{size.Width},{size.Height}";
    private static void Record(string name, object? value)
    {
        string result = name + "=" + (value?.ToString() ?? "null");
        Results.Add(result);
        Console.Error.WriteLine(result);
    }

    private sealed class TraceControl : Control
    {
        internal readonly List<string> Calls = [];
        internal readonly InvalidOperationException Failure = new("probe size constraints");
        private readonly string _border;
        internal string GetterMode = "base";
        internal string DefaultMode = "base";
        internal bool SetterSkip;
        internal bool CoreSkip;
        internal int Suppress;
        internal string? ActionPoint;
        internal Action? AtPoint;
        internal Size BaseMinimum => base.MinimumSize;
        internal Size BaseMaximum => base.MaximumSize;

        internal TraceControl(string border)
        {
            _border = border;
            Move += (_, _) => Point("event:Move");
            LocationChanged += (_, _) => Point("event:LocationChanged");
            Resize += (_, _) => Point("event:Resize");
            SizeChanged += (_, _) => Point("event:SizeChanged");
            ClientSizeChanged += (_, _) => Point("event:ClientSizeChanged");
        }

        public override Size MinimumSize
        {
            get
            {
                Point("get:min");
                Size result = GetterMode == "fixed" ? new Size(41, 23) : GetterMode == "conflict" ? new Size(101, 61)
                    : GetterMode == "negative" ? new Size(-7, -9) : base.MinimumSize;
                Log("value:min:" + Format(result));
                return result;
            }
            set { Point("enter:set:min", Format(value)); if (!SetterSkip) { base.MinimumSize = value; } Point("exit:set:min", Format(value)); }
        }

        public override Size MaximumSize
        {
            get
            {
                Point("get:max");
                Size result = GetterMode == "fixed" ? new Size(83, 47) : GetterMode == "conflict" ? new Size(43, 29)
                    : GetterMode == "negative" ? new Size(-3, -5) : base.MaximumSize;
                Log("value:max:" + Format(result));
                return result;
            }
            set { Point("enter:set:max", Format(value)); if (!SetterSkip) { base.MaximumSize = value; } Point("exit:set:max", Format(value)); }
        }

        protected override Size DefaultMinimumSize
        {
            get
            {
                Point("default:min");
                return DefaultMode == "fixed" ? new Size(31, 17) : DefaultMode == "negative" ? new Size(-7, -9) : base.DefaultMinimumSize;
            }
        }

        protected override Size DefaultMaximumSize
        {
            get
            {
                Point("default:max");
                return DefaultMode == "fixed" ? new Size(101, 59) : DefaultMode == "negative" ? new Size(-3, -5) : base.DefaultMaximumSize;
            }
        }

#if REFERENCE
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams value = base.CreateParams;
                if (_border is "border" or "both") { value.Style |= 0x00800000; }
                if (_border is "edge" or "both") { value.ExStyle |= 0x00000200; }
                return value;
            }
        }
#else
        protected override uint Style => base.Style | (_border is "border" or "both" ? 0x00800000u : 0u);
        protected override uint ExStyle => base.ExStyle | (_border is "edge" or "both" ? 0x00000200u : 0u);
        protected override void OnBoundsChanged() { }
#endif

        internal void CallCore(int x, int y, int width, int height, BoundsSpecified specified) => SetBoundsCore(x, y, width, height, specified);
        internal void CallClientCore(int x, int y) => SetClientSizeCore(x, y);

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Point("enter:core", $"{x},{y},{width},{height},{(int)specified}");
            if (!CoreSkip) { base.SetBoundsCore(x, y, width, height, specified); }
            Point("exit:core", $"{x},{y},{width},{height},{(int)specified}");
        }

        protected override void SetClientSizeCore(int x, int y)
        {
            Point("enter:client-core", $"{x},{y}");
            base.SetClientSizeCore(x, y);
            Point("exit:client-core", $"{x},{y}");
        }

        private void Log(string text) { if (Suppress == 0) { Calls?.Add(text); } }
        private void Point(string point, string? arguments = null)
        {
            if (Suppress != 0) { return; }
            Log(point + ":" + (arguments is null ? "" : arguments + ":") + BoundsState(this));
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
    private static extern bool GetWindowRect(nint handle, out NativeRect rectangle);
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetClientRect(nint handle, out NativeRect rectangle);
}
