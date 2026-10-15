using System.ComponentModel;
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
    private static readonly string[] Notifications = ["LocationChanged", "Move", "SizeChanged", "Resize"];
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
        RunCases("uncreated", native: false);
        RunCases("native", native: true);
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static void RecordMetadata()
    {
        foreach (string name in Notifications)
        {
            EventInfo notification = typeof(Control).GetEvent(name)!;
            Record(name + "-event", notification.EventHandlerType!.Name);
            RecordMethod(name + "-add", notification.AddMethod!);
            RecordMethod(name + "-remove", notification.RemoveMethod!);
            Record(name + "-browsable", notification.GetCustomAttribute<BrowsableAttribute>()?.Browsable ?? true);
            Record(name + "-editor", notification.GetCustomAttribute<EditorBrowsableAttribute>()?.State ?? EditorBrowsableState.Always);
            MethodInfo method = typeof(Control).GetMethod("On" + name, BindingFlags.Instance | BindingFlags.NonPublic,
                [typeof(EventArgs)])!;
            RecordMethod("On" + name, method);
        }

        static void RecordMethod(string name, MethodInfo method)
        {
            Record(name + "-signature", method.ReturnType.Name + ":" + string.Join(",", method.GetParameters().Select(parameter => parameter.ParameterType.Name + " " + parameter.Name)));
            Record(name + "-visibility", $"public:{method.IsPublic},family:{method.IsFamily},virtual:{method.IsVirtual},final:{method.IsFinal}");
        }
    }

    private static void RunCases(string prefix, bool native)
    {
        using var form = native ? new Form { Text = "Lumina bounds notifications probe", Width = 320, Height = 240 } : null;
        if (native) { form!.Show(); }

        (string Name, Action<TraceControl> Set)[] setters =
        [
            ("left", control => control.Left = 23),
            ("top", control => control.Top = 29),
            ("location", control => control.Location = new Point(23, 29)),
            ("width", control => control.Width = 83),
            ("height", control => control.Height = 47),
            ("size", control => control.Size = new Size(83, 47)),
            ("bounds", control => control.Bounds = new Rectangle(23, 29, 83, 47)),
            ("four", control => control.SetBounds(23, 29, 83, 47)),
            ("five", control => control.SetBounds(23, 29, 83, 47, BoundsSpecified.All))
        ];
        foreach ((string name, Action<TraceControl> setter) in setters)
        {
            using TraceControl control = Create(form);
            Observe(prefix + "-setter-" + name, control, () => setter(control), form);
            Observe(prefix + "-setter-" + name + "-repeat", control, () => setter(control), form);
        }

        int[] masks = [.. Enumerable.Range(0, 32), -1, int.MinValue, int.MaxValue];
        foreach (int mask in masks)
        {
            using TraceControl control = Create(form);
            string name = prefix + "-mask-" + mask;
            Observe(name + "-changed", control, () => control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask), form);
            Observe(name + "-repeat", control, () => control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask), form);
            Observe(name + "-ignored", control, () => control.SetBounds(
                (mask & 1) == 0 ? 199 : control.Left,
                (mask & 2) == 0 ? 211 : control.Top,
                (mask & 4) == 0 ? 223 : control.Width,
                (mask & 8) == 0 ? 227 : control.Height, (BoundsSpecified)mask), form);
        }

        foreach (int mask in new[] { 0, 1, 3, 4, 8, 12, 15, 16, 17, -1 })
        {
            using TraceControl control = Create(form);
            string name = prefix + "-direct-core-" + mask;
            Observe(name, control, () => control.CallCore(23, 29, 83, 47, (BoundsSpecified)mask), form);
            Observe(name + "-repeat", control, () => control.CallCore(23, 29, 83, 47, (BoundsSpecified)mask), form);
        }

        foreach ((int width, int height) in new[] { (0, 0), (-3, -5), (-3, 37), (73, -5), (int.MinValue, -1) })
        {
            using TraceControl control = Create(form);
            string name = prefix + "-dimensions-" + width + "-" + height;
            Observe(name, control, () => control.Size = new Size(width, height), form);
            Observe(name + "-repeat", control, () => control.Size = new Size(width, height), form);
            Observe(name + "-restore", control, () => control.Size = Initial.Size, form);
        }

        foreach (string notification in Notifications)
        {
            using TraceControl control = Create(form);
            control.Skip = notification;
            Observe(prefix + "-skip-" + notification, control, () => control.Bounds = new Rectangle(23, 29, 83, 47), form);
        }

        foreach (string point in new[] { "enter:LocationChanged", "event:Move", "event:LocationChanged", "enter:SizeChanged", "event:Resize", "event:SizeChanged" })
        {
            using TraceControl control = Create(form);
            control.ActionPoint = point;
            control.AtPoint = () => control.Bounds = new Rectangle(31, 41, 97, 61);
            Observe(prefix + "-reenter-" + point, control, () => control.Bounds = new Rectangle(23, 29, 83, 47), form);
        }

        foreach (string point in new[] { "enter:LocationChanged", "enter:Move", "event:Move", "event:LocationChanged", "enter:SizeChanged", "enter:Resize", "event:Resize", "event:SizeChanged" })
        {
            using TraceControl control = Create(form);
            control.ActionPoint = point;
            control.AtPoint = () => throw control.Failure;
            Observe(prefix + "-throw-" + point, control, () => control.Bounds = new Rectangle(23, 29, 83, 47), form);
            Observe(prefix + "-throw-" + point + "-repeat", control, () => control.Bounds = new Rectangle(23, 29, 83, 47), form);
            Observe(prefix + "-throw-" + point + "-restore", control, () => control.Bounds = Initial, form);
        }

        foreach (string notification in Notifications)
        {
            foreach (string args in new[] { "empty", "custom", "null" })
            {
                using TraceControl control = Create(form);
                EventArgs? e = args == "empty" ? EventArgs.Empty : args == "custom" ? control.CustomArgs : null;
                Observe(prefix + "-direct-" + notification + "-" + args, control, () => control.CallNotification(notification, e!), form);
            }
        }
    }

    private static TraceControl Create(Form? form)
    {
        var control = new TraceControl { Bounds = Initial };
        if (form is not null) { form.Controls.Add(control); }
        control.Calls.Clear();
        return control;
    }

    private static void Observe(string name, TraceControl control, Action action, Form? form)
    {
        control.Calls.Clear();
        NativeException = null;
        nint previousHandle = control.IsHandleCreated ? control.Handle : 0;
        Exception? thrown = null;
        try { action(); }
        catch (InvalidOperationException error) when (ReferenceEquals(error, control.Failure)) { thrown = error; }
        Record(name + "-calls", string.Join(";", control.Calls));
        Record(name + "-bounds", Format(control.Bounds));
        Record(name + "-exception", thrown is null ? "none" : ReferenceEquals(thrown, control.Failure) ? "original" : thrown.GetType().Name);
        Record(name + "-thread-exception", NativeException is null ? "none" : ReferenceEquals(NativeException, control.Failure) ? "original" : NativeException.GetType().Name);
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
        internal readonly EventArgs CustomArgs = new();
        internal readonly InvalidOperationException Failure = new("probe notification");
        internal string? Skip;
        internal string? ActionPoint;
        internal Action? AtPoint;

        internal TraceControl()
        {
            LocationChanged += (sender, e) => Event("LocationChanged", sender, e);
            Move += (sender, e) => Event("Move", sender, e);
            SizeChanged += (sender, e) => Event("SizeChanged", sender, e);
            Resize += (sender, e) => Event("Resize", sender, e);
        }

        internal void CallCore(int x, int y, int width, int height, BoundsSpecified specified)
            => SetBoundsCore(x, y, width, height, specified);

        internal void CallNotification(string notification, EventArgs e)
        {
            switch (notification)
            {
                case "LocationChanged": OnLocationChanged(e); break;
                case "Move": OnMove(e); break;
                case "SizeChanged": OnSizeChanged(e); break;
                case "Resize": OnResize(e); break;
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            Point("enter:LocationChanged", e);
            if (Skip != "LocationChanged") { base.OnLocationChanged(e); }
            Point("exit:LocationChanged", e);
        }

        protected override void OnMove(EventArgs e)
        {
            Point("enter:Move", e);
            if (Skip != "Move") { base.OnMove(e); }
            Point("exit:Move", e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Point("enter:SizeChanged", e);
            if (Skip != "SizeChanged") { base.OnSizeChanged(e); }
            Point("exit:SizeChanged", e);
        }

        protected override void OnResize(EventArgs e)
        {
            Point("enter:Resize", e);
            if (Skip != "Resize") { base.OnResize(e); }
            Point("exit:Resize", e);
        }

#if !REFERENCE
        // Existing Lumina controls can override this historical hook without calling base.
        // Standard notifications must still flow for this exact same derived control.
        protected override void OnBoundsChanged() { }
#endif

        private void Event(string name, object? sender, EventArgs? e)
        {
            Calls.Add($"sender:{name}:{ReferenceEquals(sender, this)}");
            Point("event:" + name, e);
        }

        private void Point(string point, EventArgs? e)
        {
            Calls?.Add(point + ":" + Format(Bounds) + ":" + (e is null ? "null" : ReferenceEquals(e, EventArgs.Empty) ? "empty" : ReferenceEquals(e, CustomArgs) ? "custom" : "other"));
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
