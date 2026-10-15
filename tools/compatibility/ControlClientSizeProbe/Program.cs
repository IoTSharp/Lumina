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
    private static readonly string[] Notifications = ["LocationChanged", "Move", "SizeChanged", "Resize", "ClientSizeChanged"];
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

    // These five members are the complete reflection surface inspected below.
    // Keep these dependencies local to this executable's metadata inspection.
    [DynamicDependency(nameof(Control.ClientSize), typeof(Control))]
    [DynamicDependency(nameof(Control.ClientRectangle), typeof(Control))]
    [DynamicDependency(nameof(Control.ClientSizeChanged), typeof(Control))]
    [DynamicDependency("OnClientSizeChanged(System.EventArgs)", typeof(Control))]
    [DynamicDependency("SetClientSizeCore(System.Int32,System.Int32)", typeof(Control))]
    private static void RecordMetadata()
    {
        foreach (string name in new[] { "ClientSize", "ClientRectangle" })
        {
            PropertyInfo property = typeof(Control).GetProperty(name)!;
            Record(name + "-type", property.PropertyType.Name);
            RecordMethod(name + "-get", property.GetMethod!);
            if (property.SetMethod is not null) { RecordMethod(name + "-set", property.SetMethod); }
            else { Record(name + "-set", "none"); }
            Record(name + "-browsable", property.GetCustomAttribute<BrowsableAttribute>()?.Browsable ?? true);
            Record(name + "-editor", property.GetCustomAttribute<EditorBrowsableAttribute>()?.State ?? EditorBrowsableState.Always);
            Record(name + "-serialization", property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()?.Visibility ?? DesignerSerializationVisibility.Visible);
            Record(name + "-localizable", property.GetCustomAttribute<LocalizableAttribute>()?.IsLocalizable ?? false);
        }
        EventInfo notification = typeof(Control).GetEvent("ClientSizeChanged")!;
        Record("ClientSizeChanged-event", notification.EventHandlerType!.Name);
        RecordMethod("ClientSizeChanged-add", notification.AddMethod!);
        RecordMethod("ClientSizeChanged-remove", notification.RemoveMethod!);
        Record("ClientSizeChanged-browsable", notification.GetCustomAttribute<BrowsableAttribute>()?.Browsable ?? true);
        Record("ClientSizeChanged-editor", notification.GetCustomAttribute<EditorBrowsableAttribute>()?.State ?? EditorBrowsableState.Always);
        RecordMethod("OnClientSizeChanged", typeof(Control).GetMethod("OnClientSizeChanged", BindingFlags.Instance | BindingFlags.NonPublic, [typeof(EventArgs)])!);
        RecordMethod("SetClientSizeCore", typeof(Control).GetMethod("SetClientSizeCore", BindingFlags.Instance | BindingFlags.NonPublic, [typeof(int), typeof(int)])!);
    }

    private static void RecordMethod(string name, MethodInfo method)
    {
        Record(name + "-signature", method.ReturnType.Name + ":" + string.Join(",", method.GetParameters().Select(parameter => parameter.ParameterType.Name + " " + parameter.Name)));
        Record(name + "-visibility", $"public:{method.IsPublic},family:{method.IsFamily},virtual:{method.IsVirtual},final:{method.IsFinal}");
    }

    private static void RunCases(string border, bool native)
    {
        string prefix = border + (native ? "-native" : "-uncreated");
        using var form = native ? new Form { Text = "Lumina client size probe", Width = 320, Height = 240 } : null;
        if (native) { form!.Show(); }

        (string Name, Action<TraceControl> Set)[] setters =
        [
            ("location", control => control.Location = new Point(23, 29)),
            ("width", control => control.Width = 83),
            ("height", control => control.Height = 47),
            ("size", control => control.Size = new Size(83, 47)),
            ("bounds", control => control.Bounds = new Rectangle(23, 29, 83, 47)),
            ("four", control => control.SetBounds(23, 29, 83, 47)),
            ("specified-width", control => control.SetBounds(23, 29, 83, 47, BoundsSpecified.Width)),
            ("client", control => control.ClientSize = new Size(83, 47)),
            ("client-current", control => control.ClientSize = control.ClientSize),
            ("direct-client-core", control => control.CallClientCore(83, 47)),
            ("direct-bounds-core", control => control.CallBoundsCore(23, 29, 83, 47, BoundsSpecified.None))
        ];
        foreach ((string name, Action<TraceControl> setter) in setters)
        {
            using TraceControl control = Create(form, border);
            Record(prefix + "-setter-" + name + "-initial", State(control));
            Observe(prefix + "-setter-" + name, control, () => setter(control));
            Observe(prefix + "-setter-" + name + "-repeat", control, () => setter(control));
        }

        foreach (string entry in new[] { "location", "specified-x", "direct-location-core" })
        {
            using TraceControl control = Create(form, border);
            control.ClientSize = new Size(-3, -5);
            Observe(prefix + "-negative-cache-" + entry, control, () =>
            {
                if (entry == "location") { control.Location = new Point(23, 29); }
                else if (entry == "specified-x") { control.SetBounds(23, 29, 199, 211, BoundsSpecified.X); }
                else { control.CallBoundsCore(23, 29, control.Width, control.Height, BoundsSpecified.Location); }
            });
        }

        using (TraceControl control = Create(form, border))
        {
            control.ActionPoint = "event:SizeChanged";
            control.AtPoint = () => control.Size = new Size(97, 61);
            control.ClientSize = new Size(83, 47);
            Observe(prefix + "-reentrant-cache-location", control, () => control.Location = new Point(23, 29));
        }

        using (TraceControl control = Create(form, border))
        {
            control.Size = Size.Empty;
            foreach (int dimension in new[] { 1, 2, 3, 4, 5, 6, 7 })
            {
                Observe(prefix + "-small-outer-" + dimension, control, () => control.Size = new Size(dimension, dimension));
            }
        }

        foreach ((int width, int height) in new[] { (0, 0), (-3, -5), (-3, 37), (73, -5), (1, 1), (int.MinValue, -1) })
        {
            foreach (string entry in new[] { "size", "client", "core" })
            {
                using TraceControl control = Create(form, border);
                string name = prefix + "-dimensions-" + entry + "-" + width + "-" + height;
                void Apply()
                {
                    if (entry == "size") { control.Size = new Size(width, height); }
                    else if (entry == "client") { control.ClientSize = new Size(width, height); }
                    else { control.CallClientCore(width, height); }
                }
                Observe(name, control, Apply);
                Observe(name + "-repeat", control, Apply);
                Observe(name + "-restore", control, () => control.Size = Initial.Size);
            }
        }

        foreach (string skip in new[] { "SetClientSizeCore", "SetBoundsCore", "SizeChanged", "Resize", "ClientSizeChanged" })
        {
            using TraceControl control = Create(form, border);
            control.Skip = skip;
            Observe(prefix + "-skip-" + skip + "-client", control, () => control.ClientSize = new Size(83, 47));
            Observe(prefix + "-skip-" + skip + "-size", control, () => control.Size = new Size(97, 61));
        }

        foreach (string point in new[] { "enter:SetClientSizeCore", "enter:SizeChanged", "event:Resize", "event:SizeChanged", "enter:ClientSizeChanged", "event:ClientSizeChanged" })
        {
            foreach (string reentry in new[] { "size", "client" })
            {
                using TraceControl control = Create(form, border);
                control.ActionPoint = point;
                control.AtPoint = () =>
                {
                    if (reentry == "size") { control.Size = new Size(97, 61); }
                    else { control.ClientSize = new Size(97, 61); }
                };
                Observe(prefix + "-reenter-" + reentry + "-" + point, control, () => control.ClientSize = new Size(83, 47));
            }
        }

        foreach (string point in new[] { "enter:SetClientSizeCore", "enter:SetBoundsCore", "enter:SizeChanged", "enter:Resize", "event:Resize", "event:SizeChanged", "enter:ClientSizeChanged", "event:ClientSizeChanged" })
        {
            using TraceControl control = Create(form, border);
            control.ActionPoint = point;
            control.AtPoint = () => throw control.Failure;
            Observe(prefix + "-throw-" + point, control, () => control.ClientSize = new Size(83, 47));
            Observe(prefix + "-throw-" + point + "-repeat", control, () => control.ClientSize = new Size(83, 47));
            Observe(prefix + "-throw-" + point + "-restore", control, () => control.Size = Initial.Size);
        }

        foreach (string args in new[] { "empty", "custom", "null" })
        {
            using TraceControl control = Create(form, border);
            EventArgs? e = args == "empty" ? EventArgs.Empty : args == "custom" ? control.CustomArgs : null;
            Observe(prefix + "-direct-notification-" + args, control, () => control.CallNotification(e!));
        }
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
        Exception? thrown = null;
        try { action(); }
        catch (Exception error) { thrown = error; }
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
        internal readonly EventArgs CustomArgs = new();
        internal readonly InvalidOperationException Failure = new("probe client size");
        private readonly string _border;
        internal string? Skip;
        internal string? ActionPoint;
        internal Action? AtPoint;

        internal TraceControl(string border)
        {
            _border = border;
            LocationChanged += (sender, e) => Event("LocationChanged", sender, e);
            Move += (sender, e) => Event("Move", sender, e);
            SizeChanged += (sender, e) => Event("SizeChanged", sender, e);
            Resize += (sender, e) => Event("Resize", sender, e);
            ClientSizeChanged += (sender, e) => Event("ClientSizeChanged", sender, e);
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

        internal void CallClientCore(int width, int height) => SetClientSizeCore(width, height);
        internal void CallBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) => SetBoundsCore(x, y, width, height, specified);
        internal void CallNotification(EventArgs e) => OnClientSizeChanged(e);

        protected override void SetClientSizeCore(int x, int y)
        {
            Point("enter:SetClientSizeCore", EventArgs.Empty, $"{x},{y}");
            if (Skip != "SetClientSizeCore") { base.SetClientSizeCore(x, y); }
            Point("exit:SetClientSizeCore", EventArgs.Empty, $"{x},{y}");
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Point("enter:SetBoundsCore", EventArgs.Empty, $"{x},{y},{width},{height},{(int)specified}");
            if (Skip != "SetBoundsCore") { base.SetBoundsCore(x, y, width, height, specified); }
            Point("exit:SetBoundsCore", EventArgs.Empty, $"{x},{y},{width},{height},{(int)specified}");
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

        protected override void OnClientSizeChanged(EventArgs e)
        {
            Point("enter:ClientSizeChanged", e);
            if (Skip != "ClientSizeChanged") { base.OnClientSizeChanged(e); }
            Point("exit:ClientSizeChanged", e);
        }

        private void Event(string name, object? sender, EventArgs? e)
        {
            Calls?.Add($"sender:{name}:{ReferenceEquals(sender, this)}");
            Point("event:" + name, e);
        }

        private void Point(string point, EventArgs? e, string? arguments = null)
        {
            Calls?.Add(point + ":" + (arguments is null ? "" : arguments + ":") + State(this) + ":args:"
                + (e is null ? "null" : ReferenceEquals(e, EventArgs.Empty) ? "empty" : ReferenceEquals(e, CustomArgs) ? "custom" : "other"));
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
