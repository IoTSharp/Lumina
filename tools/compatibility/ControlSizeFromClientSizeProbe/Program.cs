using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
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

    [STAThread]
    private static void Main()
    {
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
        }
#endif
        Application.ThreadException += (_, e) => NativeException = e.Exception;
        RecordMetadata();
        foreach (string border in new[] { "none", "border", "edge", "both" })
        {
            RunCases(border, native: false);
            RunCases(border, native: true);
        }
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    [DynamicDependency("SizeFromClientSize(System.Drawing.Size)", typeof(Control))]
    [DynamicDependency("SetClientSizeCore(System.Int32,System.Int32)", typeof(Control))]
    private static void RecordMetadata()
    {
        foreach ((string name, Type[] parameters) in new[]
        {
            ("SizeFromClientSize", new[] { typeof(Size) }),
            ("SetClientSizeCore", new[] { typeof(int), typeof(int) })
        })
        {
            MethodInfo method = typeof(Control).GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic, parameters)!;
            Record(name + "-signature", method.ReturnType.Name + ":" + string.Join(",", method.GetParameters().Select(parameter => parameter.ParameterType.Name + " " + parameter.Name)));
            Record(name + "-metadata", $"public:{method.IsPublic},family:{method.IsFamily},family-or-assembly:{method.IsFamilyOrAssembly},static:{method.IsStatic},virtual:{method.IsVirtual},final:{method.IsFinal},abstract:{method.IsAbstract},new-slot:{(method.Attributes & MethodAttributes.NewSlot) != 0}");
            Record(name + "-base", method.GetBaseDefinition().DeclaringType!.Name + "." + method.GetBaseDefinition().Name);
        }
    }

    private static void RunCases(string border, bool native)
    {
        string prefix = border + (native ? "-native" : "-uncreated");
        using var form = native ? new Form { Text = "Lumina size from client probe", Width = 320, Height = 240 } : null;
        if (native) { form!.Show(); }

        Size[] inputs =
        [
            new(83, 47), Size.Empty, new(-3, -5), new(-3, 37), new(73, -5), new(1, 1),
            new(int.MinValue, -1), new(-1, int.MinValue), new(int.MaxValue, int.MaxValue),
            new(int.MinValue, int.MaxValue), new(int.MaxValue - 1, int.MinValue + 1)
        ];
        foreach (Size input in inputs)
        {
            foreach (string mode in new[] { "base", "fixed", "adjust" })
            {
                using TraceControl control = Create(form, border);
                control.Mode = mode;
                string name = prefix + "-direct-" + mode + "-" + Format(input);
                Observe(name, control, () => control.CallConversion(input));
                Observe(name + "-repeat", control, () => control.CallConversion(input));
                Observe(name + "-base", control, () => control.CallBaseConversion(input));
            }
        }

        foreach (string mode in new[] { "base", "fixed", "adjust" })
        {
            foreach (string entry in new[] { "client", "core", "size" })
            {
                using TraceControl control = Create(form, border);
                control.Mode = mode;
                string name = prefix + "-route-" + mode + "-" + entry;
                Observe(name, control, () => Apply(control, entry));
                Observe(name + "-repeat", control, () => Apply(control, entry));
                Observe(name + "-direct-after", control, () => control.CallConversion(new Size(83, 47)));
            }
        }

        foreach (string point in new[] { "enter:conversion", "exit:conversion" })
        {
            foreach (string entry in new[] { "direct", "base", "client", "core" })
            {
                using TraceControl control = Create(form, border);
                control.ActionPoint = point;
                control.AtPoint = () => throw control.Failure;
                string name = prefix + "-throw-" + point + "-" + entry;
                Observe(name, control, () => Apply(control, entry));
                Observe(name + "-repeat", control, () => Apply(control, entry));
            }

            foreach (string entry in new[] { "direct", "client", "core" })
            {
                foreach (string nested in new[] { "client", "size", "direct" })
                {
                    using TraceControl control = Create(form, border);
                    control.ActionPoint = point;
                    control.AtPoint = () =>
                    {
                        if (nested == "client") { control.ClientSize = new Size(97, 61); }
                        else if (nested == "size") { control.Size = new Size(97, 61); }
                        else { control.Calls.Add("nested-result:" + Format(control.CallConversion(new Size(97, 61)))); }
                    };
                    string name = prefix + "-reenter-" + point + "-" + entry + "-" + nested;
                    Observe(name, control, () => Apply(control, entry));
                    Observe(name + "-after", control, () => control.CallBaseConversion(new Size(83, 47)));
                }
            }
        }

        // A client cache may differ from both outer and native dimensions after a
        // negative or reentrant request. Conversion must be observed in that state.
        foreach (string cache in new[] { "negative", "reentrant" })
        {
            using TraceControl control = Create(form, border);
            if (cache == "negative") { control.ClientSize = new Size(-3, -5); }
            else
            {
                control.ActionPoint = "event:SizeChanged";
                control.AtPoint = () => control.Size = new Size(97, 61);
                control.ClientSize = new Size(83, 47);
            }
            Observe(prefix + "-cache-" + cache, control, () => control.CallConversion(new Size(83, 47)));
        }
    }

    private static Size? Apply(TraceControl control, string entry)
    {
        var input = new Size(83, 47);
        if (entry == "direct") { return control.CallConversion(input); }
        if (entry == "base") { return control.CallBaseConversion(input); }
        if (entry == "client") { control.ClientSize = input; }
        else if (entry == "core") { control.CallClientCore(input.Width, input.Height); }
        else { control.Size = input; }
        return null;
    }

    private static TraceControl Create(Form? form, string border)
    {
        var control = new TraceControl(border) { Bounds = Initial };
        if (form is not null) { form.Controls.Add(control); }
        control.Calls.Clear();
        return control;
    }

    private static void Observe(string name, TraceControl control, Func<Size?> action)
    {
        control.Calls.Clear();
        NativeException = null;
        nint previousHandle = control.IsHandleCreated ? control.Handle : 0;
        string before = State(control);
        Exception? thrown = null;
        Size? result = null;
        try { result = action(); }
        catch (Exception error) { thrown = error; }
        Record(name + "-before", before);
        Record(name + "-result", result is Size size ? Format(size) : "none");
        Record(name + "-calls", string.Join(";", control.Calls));
        Record(name + "-state", State(control));
        Record(name + "-exception", thrown is null ? "none" : ReferenceEquals(thrown, control.Failure) ? "original" : thrown.GetType().Name);
        Record(name + "-thread-exception", NativeException is null ? "none" : ReferenceEquals(NativeException, control.Failure) ? "original" : NativeException.GetType().Name);
        Record(name + "-handle-preserved", control.IsHandleCreated == (previousHandle != 0)
            && (previousHandle == 0 || control.Handle == previousHandle));
    }

    private static string State(Control control)
    {
        string state = "bounds:" + Format(control.Bounds) + "|client:" + Format(control.ClientRectangle)
            + "|client-size:" + Format(control.ClientSize) + "|handle:" + control.IsHandleCreated;
        if (control.IsHandleCreated)
        {
            if (!GetWindowRect(control.Handle, out NativeRect window)) { throw new Win32Exception(Marshal.GetLastWin32Error(), "GetWindowRect failed."); }
            if (!GetClientRect(control.Handle, out NativeRect client)) { throw new Win32Exception(Marshal.GetLastWin32Error(), "GetClientRect failed."); }
            state += "|native-window:" + (window.Right - window.Left) + "," + (window.Bottom - window.Top)
                + "|native-client:" + Format(new Rectangle(client.Left, client.Top, client.Right - client.Left, client.Bottom - client.Top));
        }
        return state;
    }

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
        internal readonly InvalidOperationException Failure = new("probe size from client");
        private readonly string _border;
        internal string Mode = "base";
        internal string? ActionPoint;
        internal Action? AtPoint;

        internal TraceControl(string border)
        {
            _border = border;
            foreach (string name in new[] { "LocationChanged", "Move", "SizeChanged", "Resize", "ClientSizeChanged" })
            {
                EventHandler handler = (sender, e) =>
                {
                    Calls?.Add($"sender:{name}:{ReferenceEquals(sender, this)}");
                    Point("event:" + name);
                };
                if (name == "LocationChanged") { LocationChanged += handler; }
                else if (name == "Move") { Move += handler; }
                else if (name == "SizeChanged") { SizeChanged += handler; }
                else if (name == "Resize") { Resize += handler; }
                else { ClientSizeChanged += handler; }
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

        internal Size CallConversion(Size clientSize) => SizeFromClientSize(clientSize);
        internal Size CallBaseConversion(Size clientSize) => base.SizeFromClientSize(clientSize);
        internal void CallClientCore(int x, int y) => SetClientSizeCore(x, y);

        protected override Size SizeFromClientSize(Size clientSize)
        {
            Point("enter:conversion", Format(clientSize));
            Size result = Mode == "fixed" ? new Size(19, 23) : base.SizeFromClientSize(clientSize);
            if (Mode == "adjust") { result = new Size(unchecked(result.Width + 5), unchecked(result.Height + 7)); }
            Point("exit:conversion", Format(result));
            return result;
        }

        protected override void SetClientSizeCore(int x, int y)
        {
            Point("enter:client-core", $"{x},{y}");
            base.SetClientSizeCore(x, y);
            Point("exit:client-core", $"{x},{y}");
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Point("enter:bounds-core", $"{x},{y},{width},{height},{(int)specified}");
            base.SetBoundsCore(x, y, width, height, specified);
            Point("exit:bounds-core", $"{x},{y},{width},{height},{(int)specified}");
        }

        private void Point(string point, string? arguments = null)
        {
            Calls?.Add(point + ":" + (arguments is null ? "" : arguments + ":") + State(this));
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
