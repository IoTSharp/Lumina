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
        using (var uncreated = new Control())
        {
            RunCases("uncreated", uncreated);
        }

        using var form = new Form { Text = "Lumina bounds probe", Width = 320, Height = 220 };
        using var native = new Control();
        using var handleless = new HandlelessControl { Visible = false };
        form.Controls.Add(native);
        form.Controls.Add(handleless);
        RunCases("attached-before-show", native);
        form.Show();
        RunCases("native", native, form);
        RunCases("owner-shown-without-own-handle", handleless);
        RunCreationCases();
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    private static void RunCreationCases()
    {
        Size[] requestedSizes = [Size.Empty, new Size(0, 37), new Size(73, 0), new Size(-3, 37),
            new Size(73, -5), new Size(-3, -5), new Size(int.MinValue, 37), new Size(73, int.MinValue)];
        using var form = new Form { Text = "Lumina initial bounds probe", Width = 320, Height = 220 };
        var controls = new Control[requestedSizes.Length];
        var boundsAtHandleCreation = new Rectangle?[requestedSizes.Length];
        for (int index = 0; index < controls.Length; index++)
        {
            int capturedIndex = index;
            var control = new Control { Bounds = new Rectangle(new Point(7, 9), requestedSizes[index]) };
            control.HandleCreated += (_, _) => boundsAtHandleCreation[capturedIndex] = control.Bounds;
            controls[index] = control;
            form.Controls.Add(control);
            Record("creation-" + index + "-before-bounds", Format(control.Bounds));
            Record("creation-" + index + "-before-no-own-handle", !control.IsHandleCreated);
        }

        form.Show();
        for (int index = 0; index < controls.Length; index++)
        {
            Control control = controls[index];
            string key = "creation-" + index;
            Record(key + "-own-handle", control.IsHandleCreated);
            Record(key + "-bounds-at-handle-created", boundsAtHandleCreation[index] is Rectangle value ? Format(value) : "missing");
            Record(key + "-after-bounds", Format(control.Bounds));
            RecordNativeBounds(key + "-after", control, form);
            nint handle = control.Handle;
            control.Size = new Size(73, 37);
            Record(key + "-restored-bounds", Format(control.Bounds));
            RecordNativeBounds(key + "-restored", control, form);
            Record(key + "-same-handle", handle == control.Handle);
        }
    }

    private static void RunCases(string prefix, Control control, Form? nativeParent = null)
    {
        // Constructor default sizes and derived-control size constraints have separate scopes.
        control.SetBounds(11, 13, 73, 37);
        bool startedWithHandle = control.IsHandleCreated;
        nint originalHandle = startedWithHandle ? control.Handle : 0;
        int createdCount = 0;
        control.HandleCreated += (_, _) => createdCount++;
        Snapshot("initial");

        Apply("setbounds-zero-width", c => c.SetBounds(11, 13, 0, 37));
        Apply("setbounds-zero-width-repeat", c => c.SetBounds(11, 13, 0, 37));
        Apply("setbounds-negative-width", c => c.SetBounds(11, 13, -7, 37));
        Apply("setbounds-minvalue-width", c => c.SetBounds(11, 13, int.MinValue, 37));
        Apply("setbounds-width-restored", c => c.SetBounds(11, 13, 73, 37));
        Apply("setbounds-zero-height", c => c.SetBounds(17, 19, 73, 0));
        Apply("setbounds-zero-height-repeat", c => c.SetBounds(17, 19, 73, 0));
        Apply("setbounds-negative-height", c => c.SetBounds(17, 19, 73, -9));
        Apply("setbounds-minvalue-height", c => c.SetBounds(17, 19, 73, int.MinValue));
        Apply("setbounds-height-restored", c => c.SetBounds(17, 19, 73, 37));
        Apply("setbounds-zero-both", c => c.SetBounds(23, 29, 0, 0));
        Apply("setbounds-negative-both", c => c.SetBounds(23, 29, -3, -5));
        Apply("setbounds-both-restored", c => c.SetBounds(11, 13, 73, 37));

        Apply("width-zero", c => c.Width = 0);
        Apply("width-negative", c => c.Width = -4);
        Apply("width-negative-repeat", c => c.Width = -4);
        Apply("width-restored", c => c.Width = 73);
        Apply("height-zero", c => c.Height = 0);
        Apply("height-negative", c => c.Height = -5);
        Apply("height-negative-repeat", c => c.Height = -5);
        Apply("height-restored", c => c.Height = 37);

        Apply("size-zero", c => c.Size = Size.Empty);
        Apply("size-negative", c => c.Size = new Size(-3, -5));
        Apply("size-negative-repeat", c => c.Size = new Size(-3, -5));
        Apply("size-negative-width", c => c.Size = new Size(-3, 17));
        Apply("size-negative-height", c => c.Size = new Size(31, -5));
        Apply("size-restored", c => c.Size = new Size(73, 37));

        Apply("bounds-zero", c => c.Bounds = new Rectangle(-7, -11, 0, 0));
        Apply("bounds-negative", c => c.Bounds = new Rectangle(-7, -11, -3, -5));
        Apply("bounds-negative-repeat", c => c.Bounds = new Rectangle(-7, -11, -3, -5));
        Apply("bounds-negative-width", c => c.Bounds = new Rectangle(5, 7, -3, 17));
        Apply("bounds-negative-height", c => c.Bounds = new Rectangle(7, 9, 31, -5));
        Apply("bounds-restored", c => c.Bounds = new Rectangle(11, 13, 73, 37));

        Apply("zero-before-location", c => c.Size = Size.Empty);
        Apply("zero-location", c => c.Location = new Point(-17, -23));
        Apply("zero-left", c => c.Left = 31);
        Apply("zero-top", c => c.Top = 41);
        Apply("zero-size-properties-preserve-location", c => { c.Width = -13; c.Height = -19; });
        Apply("final-restored", c => c.Size = new Size(73, 37));
        Record(prefix + "-handle-created-events", createdCount);

        void Apply(string name, Action<Control> operation)
        {
            operation(control);
            Snapshot(name);
        }

        void Snapshot(string name)
        {
            string key = prefix + "-" + name;
            bool beforeQuery = control.IsHandleCreated;
            Rectangle bounds = control.Bounds;
            Record(key + "-bounds", $"{bounds.X},{bounds.Y},{bounds.Width},{bounds.Height}");
            Record(key + "-properties-agree", control.Left == bounds.X && control.Top == bounds.Y
                && control.Width == bounds.Width && control.Height == bounds.Height
                && control.Location == bounds.Location && control.Size == bounds.Size
                && control.Right == bounds.Right && control.Bottom == bounds.Bottom);
            Record(key + "-queries-preserve-handle", control.IsHandleCreated == beforeQuery);
            Record(key + "-handle-state-preserved", control.IsHandleCreated == startedWithHandle
                && (!startedWithHandle || control.Handle == originalHandle));
            if (nativeParent is not null)
            {
                RecordNativeBounds(key, control, nativeParent);
            }
        }
    }

    private static void RecordNativeBounds(string name, Control control, Form parent)
    {
        if (!GetWindowRect(control.Handle, out NativeRect rectangle))
        {
            throw new InvalidOperationException("GetWindowRect failed for the bounds probe.");
        }

        var point = new NativePoint { X = rectangle.Left, Y = rectangle.Top };
        if (!ScreenToClient(parent.Handle, ref point))
        {
            throw new InvalidOperationException("ScreenToClient failed for the bounds probe.");
        }

        Record(name + "-native-bounds", $"{point.X},{point.Y},{rectangle.Right - rectangle.Left},{rectangle.Bottom - rectangle.Top}");
    }

    private static string Format(Rectangle bounds) => $"{bounds.X},{bounds.Y},{bounds.Width},{bounds.Height}";

    private static void Record(string name, object? value)
    {
        string result = name + "=" + (value?.ToString() ?? "null");
        Results.Add(result);
        Console.Error.WriteLine(result);
    }

    private sealed class HandlelessControl : Control
    {
#if !REFERENCE
        protected override bool ShouldCreateNativeHandle => false;
#endif
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativePoint
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint handle, out NativeRect rectangle);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ScreenToClient(nint handle, ref NativePoint point);
}
