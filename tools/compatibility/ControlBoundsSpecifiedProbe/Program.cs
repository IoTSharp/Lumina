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
        RecordMetadata();
        RunCases("uncreated", native: false);
        RunCases("native", native: true);
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static void RecordMetadata()
    {
        Record("enum-flags", typeof(BoundsSpecified).IsDefined(typeof(FlagsAttribute), inherit: false));
        Record("enum-underlying", Enum.GetUnderlyingType(typeof(BoundsSpecified)).Name);
        Record("enum-values", string.Join(";", Enum.GetValues<BoundsSpecified>().Select(value => value + ":" + (int)value)));
        MethodInfo four = typeof(Control).GetMethod(nameof(Control.SetBounds), [typeof(int), typeof(int), typeof(int), typeof(int)])!;
        MethodInfo five = typeof(Control).GetMethod(nameof(Control.SetBounds), [typeof(int), typeof(int), typeof(int), typeof(int), typeof(BoundsSpecified)])!;
        MethodInfo core = typeof(Control).GetMethod("SetBoundsCore", BindingFlags.Instance | BindingFlags.NonPublic,
            [typeof(int), typeof(int), typeof(int), typeof(int), typeof(BoundsSpecified)])!;
        RecordMethod("setbounds-four", four);
        RecordMethod("setbounds-five", five);
        RecordMethod("setboundscore", core);

        static void RecordMethod(string name, MethodInfo method)
        {
            Record(name + "-signature", method.ReturnType.Name + ":" + string.Join(",", method.GetParameters().Select(parameter => parameter.ParameterType.Name + " " + parameter.Name)));
            Record(name + "-visibility", $"public:{method.IsPublic},family:{method.IsFamily},virtual:{method.IsVirtual},final:{method.IsFinal}");
        }
    }

    private static void RunCases(string prefix, bool native)
    {
        using var form = native ? new Form { Text = "Lumina specified bounds probe", Width = 320, Height = 240 } : null;
        if (native) { form!.Show(); }
        int[] masks = [.. Enumerable.Range(0, 32), -1, int.MinValue, int.MaxValue];
        foreach (int mask in masks)
        {
            using TraceControl control = Create(form);
            string name = prefix + "-mask-" + mask;
            Observe(name + "-changed", control, () => control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask), form);
            Observe(name + "-repeat", control, () => control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask), form);
            Observe(name + "-new-ignored-values", control, () => control.SetBounds(
                (mask & 1) == 0 ? 199 : control.Left,
                (mask & 2) == 0 ? 211 : control.Top,
                (mask & 4) == 0 ? 223 : control.Width,
                (mask & 8) == 0 ? 227 : control.Height, (BoundsSpecified)mask), form);
        }

        foreach (int mask in new[] { 0, 1, 4, 8, 12, 15, 16, 17, 31, -1 })
        {
            using TraceControl control = Create(form);
            string name = prefix + "-negative-mask-" + mask;
            Observe(name + "-changed", control, () => control.SetBounds(-7, 9, -3, -5, (BoundsSpecified)mask), form);
            Observe(name + "-repeat", control, () => control.SetBounds(-7, 9, -3, -5, (BoundsSpecified)mask), form);
            Observe(name + "-restored", control, () => control.SetBounds(11, 13, 73, 37, BoundsSpecified.All), form);
        }

        (string Name, Action<TraceControl> Set)[] setters =
        [
            ("four", control => control.SetBounds(x: 23, y: 29, width: 83, height: 47)),
            ("five", control => control.SetBounds(x: 23, y: 29, width: 83, height: 47, specified: BoundsSpecified.All)),
            ("left", control => control.Left = 23),
            ("top", control => control.Top = 29),
            ("location", control => control.Location = new Point(23, 29)),
            ("width", control => control.Width = 83),
            ("height", control => control.Height = 47),
            ("size", control => control.Size = new Size(83, 47)),
            ("bounds", control => control.Bounds = new Rectangle(23, 29, 83, 47))
        ];
        foreach ((string name, Action<TraceControl> setter) in setters)
        {
            using TraceControl control = Create(form);
            Observe(prefix + "-setter-" + name + "-changed", control, () => setter(control), form);
            Observe(prefix + "-setter-" + name + "-repeat", control, () => setter(control), form);
        }

        foreach (int mask in new[] { 0, 1, 3, 4, 8, 12, 15, 16, 17, -1 })
        {
            using TraceControl control = Create(form);
            string name = prefix + "-direct-core-" + mask;
            Observe(name + "-changed", control, () => control.CallCore(23, 29, 83, 47, (BoundsSpecified)mask), form);
            Observe(name + "-repeat", control, () => control.CallCore(23, 29, 83, 47, (BoundsSpecified)mask), form);
        }

        foreach (string behavior in new[] { "skip", "transform", "none", "throw" })
        {
            foreach (int mask in new[] { 0, 1, 4, 15, 17, -1 })
            {
                using TraceControl control = Create(form);
                control.Behavior = behavior;
                string name = prefix + "-override-" + behavior + "-" + mask;
                Observe(name + "-changed", control, () => control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask), form);
                Observe(name + "-repeat", control, () => control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask), form);
            }
        }

        if (!native)
        {
            using TraceControl control = Create(null);
            control.SetBounds(-7, 9, -3, -5);
            foreach ((string name, Action<TraceControl> setter) in setters)
            {
                control.SetBounds(-7, 9, -3, -5);
                Observe(prefix + "-negative-preservation-" + name, control, () => setter(control), null);
            }
        }
    }

    private static TraceControl Create(Form? form)
    {
        var control = new TraceControl { Bounds = new Rectangle(11, 13, 73, 37) };
        if (form is not null) { form.Controls.Add(control); }
        control.Calls.Clear();
        return control;
    }

    private static void Observe(string name, TraceControl control, Action action, Form? form)
    {
        control.Calls.Clear();
        nint previousHandle = control.IsHandleCreated ? control.Handle : 0;
        string exception = "none";
        try { action(); }
        catch (InvalidOperationException error) when (error.Message == "probe override") { exception = error.GetType().Name; }
        Record(name + "-calls", string.Join(";", control.Calls));
        Record(name + "-bounds", Format(control.Bounds));
        Record(name + "-exception", exception);
        Record(name + "-handle-preserved", control.IsHandleCreated == (previousHandle != 0)
            && (previousHandle == 0 || control.Handle == previousHandle));
        if (form is not null)
        {
            if (!GetWindowRect(control.Handle, out NativeRect rectangle)) { throw new InvalidOperationException("GetWindowRect failed."); }
            var point = new NativePoint { X = rectangle.Left, Y = rectangle.Top };
            if (!ScreenToClient(form.Handle, ref point)) { throw new InvalidOperationException("ScreenToClient failed."); }
            Record(name + "-native-bounds", $"{point.X},{point.Y},{rectangle.Right - rectangle.Left},{rectangle.Bottom - rectangle.Top}");
        }
    }

    private static string Format(Rectangle bounds) => $"{bounds.X},{bounds.Y},{bounds.Width},{bounds.Height}";
    private static void Record(string name, object? value)
    {
        string result = name + "=" + (value?.ToString() ?? "null");
        Results.Add(result);
        Console.Error.WriteLine(result);
    }

    private sealed class TraceControl : Control
    {
        internal readonly List<string> Calls = [];
        internal string Behavior = "pass";

        public void CallCore(int x, int y, int width, int height, BoundsSpecified specified)
            => SetBoundsCore(x, y, width, height, specified);

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Calls?.Add($"enter:{x},{y},{width},{height},{(int)specified}:old:{Format(Bounds)}");
            if (Behavior == "throw") { throw new InvalidOperationException("probe override"); }
            if (Behavior == "transform") { x += 2; y += 3; width += 5; height += 7; }
            if (Behavior != "skip")
            {
                base.SetBoundsCore(x, y, width, height, Behavior == "none" ? BoundsSpecified.None : specified);
            }
            Calls?.Add("exit:" + Format(Bounds));
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
