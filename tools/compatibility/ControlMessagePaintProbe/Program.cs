using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

// The same source is compiled for both libraries. Each scenario has its own STA
// process. Native observations and exceptions are retained without substitutions.
internal static class Program
{
    private static readonly List<string> Events = [];
    private static readonly Exception Failure = new InvalidOperationException("message paint callback failure");
    private const int ProbeMessage = 0x8000 + 123;
    private static bool _recording;

    [STAThread]
    private static void Main(string[] args)
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        Console.Error.WriteLine("runtime=" + RuntimeInformation.FrameworkDescription);
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
            throw new InvalidOperationException("Expected WinForms 10.0.11, found " + version);
        Console.Error.WriteLine("reference-assembly=" + version);
#endif
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        if (args.Length != 1) throw new ArgumentException("Specify exactly one message/paint scenario.");
        Record("scenario", args[0]);
        switch (args[0])
        {
            case "metadata": Metadata(); break;
            case "message": MessageValues(); break;
            case "message-names": MessageNames(); break;
            case "create-params": CreateParamsValues(); break;
            case "styles": Styles(); break;
            case "style-events": StyleEvents(); break;
            case "wndproc": WindowMessages(); break;
            case "create-native": CreateNative(); break;
            case "create-parameter": CreateParameter(); break;
            case "form-messages": FormMessages(); break;
            case "paint-args": PaintArguments(); break;
            case "paint-direct": PaintDirect(); break;
            case "invalidate": Invalidations(); break;
            case "native-paint": NativePaint(); break;
            case "native-edit": NativeEdit(); break;
            case "transparent": Transparent(); break;
            case "reentrancy": Reentrancy(); break;
            case "exceptions": Exceptions(); break;
            default: throw new ArgumentException("Unknown message/paint scenario: " + args[0]);
        }
        Record("complete", true);
    }

    private static void Metadata()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        (string Name, MethodInfo? Member)[] methods =
        [
            ("WndProc", typeof(Control).GetMethod("WndProc", flags, [typeof(Message).MakeByRefType()])),
            ("DefWndProc", typeof(Control).GetMethod("DefWndProc", flags, [typeof(Message).MakeByRefType()])),
            ("GetStyle", typeof(Control).GetMethod("GetStyle", flags, [typeof(ControlStyles)])),
            ("SetStyle", typeof(Control).GetMethod("SetStyle", flags, [typeof(ControlStyles), typeof(bool)])),
            ("UpdateStyles", typeof(Control).GetMethod("UpdateStyles", flags, Type.EmptyTypes)),
            ("OnStyleChanged", typeof(Control).GetMethod("OnStyleChanged", flags, [typeof(EventArgs)])),
            ("OnPaint", typeof(Control).GetMethod("OnPaint", flags, [typeof(PaintEventArgs)])),
            ("OnPaintBackground", typeof(Control).GetMethod("OnPaintBackground", flags, [typeof(PaintEventArgs)])),
            ("OnInvalidated", typeof(Control).GetMethod("OnInvalidated", flags, [typeof(InvalidateEventArgs)])),
            ("Invalidate", typeof(Control).GetMethod("Invalidate", flags, Type.EmptyTypes)),
            ("Invalidate-bool", typeof(Control).GetMethod("Invalidate", flags, [typeof(bool)])),
            ("Invalidate-rect", typeof(Control).GetMethod("Invalidate", flags, [typeof(Rectangle)])),
            ("Invalidate-rect-bool", typeof(Control).GetMethod("Invalidate", flags, [typeof(Rectangle), typeof(bool)])),
            ("Invalidate-region", typeof(Control).GetMethod("Invalidate", flags, [typeof(Region)])),
            ("Invalidate-region-bool", typeof(Control).GetMethod("Invalidate", flags, [typeof(Region), typeof(bool)])),
            ("Update", typeof(Control).GetMethod("Update", flags, Type.EmptyTypes)),
            ("Refresh", typeof(Control).GetMethod("Refresh", flags, Type.EmptyTypes))
        ];
        foreach ((string name, MethodInfo? method) in methods) Record("method-" + name, Shape(method));
        PropertyInfo? createParams = typeof(Control).GetProperty("CreateParams", flags);
        PropertyInfo? buffered = typeof(Control).GetProperty("DoubleBuffered", flags);
        Record("property-CreateParams", Shape(createParams?.GetMethod));
        Record("property-DoubleBuffered", "get:" + Shape(buffered?.GetMethod) + "|set:" + Shape(buffered?.SetMethod));
        Record("event-Paint", typeof(Control).GetEvent("Paint", flags)?.EventHandlerType?.Name);
        Record("event-Invalidated", typeof(Control).GetEvent("Invalidated", flags)?.EventHandlerType?.Name);
        Record("event-StyleChanged", typeof(Control).GetEvent("StyleChanged", flags)?.EventHandlerType?.Name);
        Record("message-value-type", typeof(Message).IsValueType);
        Record("styles-flags", typeof(ControlStyles).IsDefined(typeof(FlagsAttribute), false));
        foreach (ControlStyles style in Enum.GetValues<ControlStyles>()) Record("style-value-" + style, (int)style);
    }

    private static string Shape(MethodInfo? method) => method is null ? "missing" :
        $"access:{(method.IsPublic ? "public" : method.IsFamily ? "protected" : method.IsFamilyOrAssembly ? "protected-internal" : "other")},static:{method.IsStatic},virtual:{method.IsVirtual},final:{method.IsFinal},return:{method.ReturnType.Name},params:[{string.Join(",", method.GetParameters().Select(p => p.ParameterType.Name))}]";

    private static void MessageValues()
    {
        Message empty = default;
        Record("empty", MessageText(empty));
        Message value = Message.Create((nint)0x1234, 0x000C, (nint)0x3456, (nint)(-7));
        Record("created", MessageText(value));
        Record("created-string", value.ToString());
        Message copy = value;
        Record("equal-copy", $"operator:{value == copy}|equals:{value.Equals(copy)}|object:{value.Equals((object)copy)}|hash:{value.GetHashCode() == copy.GetHashCode()}");
        copy.Result = (nint)91;
        Record("different-result", $"operator:{value != copy}|equals:{value.Equals(copy)}");
        value.HWnd = (nint)(-3); value.Msg = -9; value.WParam = (nint)8; value.LParam = (nint)7; value.Result = (nint)6;
        Record("setters", MessageText(value));
        Record("equals-null", value.Equals(null));
        Record("equals-other", value.Equals(8));
        nint memory = Marshal.AllocHGlobal(Marshal.SizeOf<Payload>());
        try
        {
            // Static StructureToPtr<T> roots this specific struct's marshalling.
            // This does not claim arbitrary runtime GetLParam(Type) AOT support.
            Marshal.StructureToPtr(new Payload { Number = 23, Handle = (nint)456 }, memory, false);
            Message payload = Message.Create(0, 0, 0, memory);
            Step("get-lparam-struct", () =>
            {
                object? result = payload.GetLParam(typeof(Payload));
                Record("payload", result is Payload p ? $"{p.Number},{p.Handle}" : result?.GetType().Name ?? "null");
            });
            Step("get-lparam-null-type", () => _ = payload.GetLParam(null!));
            Message noPointer = Message.Create(0, 0, 0, 0);
            Step("get-lparam-zero-pointer", () => Record("zero-payload", noPointer.GetLParam(typeof(Payload))));
        }
        finally { Marshal.FreeHGlobal(memory); }
    }

    private static string MessageText(Message m) => $"hwnd:{m.HWnd},msg:{m.Msg},wparam:{m.WParam},lparam:{m.LParam},result:{m.Result}";

    private static void MessageNames()
    {
        // Keep complete original ToString text. The selection records all names
        // recognized by the tested library, so a missing name is a real diff.
        for (int id = 0; id <= 0x8000; id++)
        {
            string text = Message.Create(0, id, 0, 0).ToString();
            if (text.Contains(" (") && !text.Contains("WM_REFLECT")) Record("named-" + id, text);
        }
        foreach (int id in new[] { -1, int.MinValue, 0x0400, 0x7FFF, 0x2000, 0x200F, 0x7FFFFFFF })
            Record("boundary-" + id, Message.Create((nint)(-1), id, (nint)(-2), (nint)(-3)).ToString());
        foreach (int low in new[] { 1, 2, 0x0201, 0x7FFF })
            Record("parent-notify-" + low, Message.Create((nint)0x1234, 0x0210, (nint)(0x12340000 | low), (nint)0x5678).ToString());
    }

    private static void CreateParamsValues()
    {
        var cp = new CreateParams();
        Record("defaults", ParamsText(cp));
        Record("default-string", cp.ToString());
        object parameter = new();
        cp.Caption = "caption"; cp.ClassName = "STATIC"; cp.ClassStyle = 0x123; cp.ExStyle = 0x200;
        cp.Style = unchecked((int)0xA1234567); cp.X = -11; cp.Y = 12; cp.Width = -13; cp.Height = 14;
        cp.Parent = (nint)0x4321; cp.Param = parameter;
        Record("setters", ParamsText(cp));
        Record("parameter-identity", ReferenceEquals(parameter, cp.Param));
        Record("value-string", cp.ToString());
        using var c = New("params");
        CreateParams first = c.Parameters;
        Record("control-params", ControlParamsText(first));
        Record("control-params-reused", ReferenceEquals(first, c.Parameters));
        Record("control-handle-after-get", c.IsHandleCreated);
    }

    private static string ParamsText(CreateParams p) => $"caption:{p.Caption ?? "null"}|class:{p.ClassName ?? "null"}|classstyle:{p.ClassStyle}|exstyle:{p.ExStyle}|style:{p.Style}|bounds:{p.X},{p.Y},{p.Width},{p.Height}|parent:{p.Parent}|param:{(p.Param is null ? "null" : "object")}";
    private static string ControlParamsText(CreateParams p) => $"caption:{p.Caption ?? "null"}|class-null:{p.ClassName is null}|classstyle:{p.ClassStyle}|exstyle:{p.ExStyle}|style:{p.Style}|bounds:{p.X},{p.Y},{p.Width},{p.Height}|parent-zero:{p.Parent == 0}|param-null:{p.Param is null}";

    private static void Styles()
    {
        using var c = New("styles");
        Record("initial", StyleText(c));
        foreach (ControlStyles style in Enum.GetValues<ControlStyles>()) Record("default-" + style, c.ReadStyle(style));
        Record("zero", c.ReadStyle(0));
        Record("unknown", c.ReadStyle(unchecked((ControlStyles)0x80000000)));
        Step("set-unknown", () => c.WriteStyle(unchecked((ControlStyles)0x80000000), true), c);
        Step("clear-unknown", () => c.WriteStyle(unchecked((ControlStyles)0x80000000), false), c);
        Step("clear-all", () => c.WriteStyle((ControlStyles)(-1), false), c);
        Step("set-combined", () => c.WriteStyle(ControlStyles.UserPaint | ControlStyles.Opaque, true), c);
        Record("combined-query", $"both:{c.ReadStyle(ControlStyles.UserPaint | ControlStyles.Opaque)}|one-missing:{c.ReadStyle(ControlStyles.UserPaint | ControlStyles.Selectable)}");
        Step("double-buffer-true", () => c.Buffered = true, c);
        Step("double-buffer-repeat", () => c.Buffered = true, c);
        Step("double-buffer-false", () => c.Buffered = false, c);
        Step("style-update-no-handle", c.ApplyStyles, c);
        Step("dispose", c.Dispose, c);
        Step("after-dispose", () => { c.WriteStyle(ControlStyles.UserPaint, true); c.ApplyStyles(); }, c);
    }

    private static void StyleEvents()
    {
        using var c = New("style-events");
        c.StyleChanged += (_, e) => Trace("event-style:empty=" + ReferenceEquals(e, EventArgs.Empty));
        Step("set-style", () => c.WriteStyle(ControlStyles.Opaque, true), c);
        Step("update-no-handle", c.ApplyStyles, c);
        EventArgs argument = new();
        c.StyleChanged += (_, e) => Trace("event-identity:" + ReferenceEquals(e, argument));
        Step("direct-notify", () => c.RaiseStyle(argument), c);
        _ = c.Handle;
        Step("update-with-handle", c.ApplyStyles, c);
        c.ExtraStyle = 0x00800000;
        Step("update-add-border", c.ApplyStyles, c);
        Record("native-border", (GetWindowLongPtrW(c.Handle, -16).ToInt64() & 0x00800000) != 0);
        Record("native-client", NativeClient(c.Handle));
    }

    private static void WindowMessages()
    {
        using var c = New("messages");
        c.Text = "native caption";
        nint handle = c.Handle;
        Step("native-override", () => Record("native-result", SendMessageW(handle, ProbeMessage, (nint)12, (nint)34)), c);
        Step("native-base", () => Record("native-text-length", SendMessageW(handle, 0x000E, 0, 0)), c);
        Message direct = Message.Create(handle, 0x000E, 0, 0);
        Step("def-direct", () => c.CallDefault(ref direct), c);
        Record("def-result", direct.Result);
        direct = Message.Create(handle, ProbeMessage, (nint)4, (nint)5);
        Step("wndproc-direct", () => c.CallWindow(ref direct), c);
        Record("direct-result", direct.Result);
        c.RewriteToTextLength = true;
        Step("native-rewrite", () => Record("rewritten-result", SendMessageW(handle, ProbeMessage, 0, 0)), c);
    }

    private static void CreateNative()
    {
        using var c = New("creation");
        c.NativeCaption = "override caption"; c.NativeBounds = new Rectangle(17, 19, 91, 73);
        c.ExtraStyle = 0x00800000; c.ExtraExStyle = 0x00000200;
        c.HandleCreated += (_, _) => Trace("created");
        c.HandleDestroyed += (_, _) => Trace("destroyed");
        Step("create", () => _ = c.Handle, c);
        RecordNative("first", c);
        nint old = c.Handle;
        c.NativeCaption = "replacement"; c.NativeBounds = new Rectangle(23, 29, 97, 79);
        c.ExtraStyle = 0; c.ExtraExStyle = 0;
        Step("recreate", c.Rebuild, c);
        Record("old-window-valid", IsWindow(old));
        RecordNative("second", c);
        nint last = c.Handle;
        Step("dispose", c.Dispose, c);
        Record("disposed-window-valid", IsWindow(last));
    }

    private static void RecordNative(string name, ProbeControl c)
    {
        nint hwnd = c.Handle;
        _ = GetWindowRect(hwnd, out NativeRect r);
        var caption = new StringBuilder(128);
        _ = GetWindowTextW(hwnd, caption, caption.Capacity);
        Record(name, $"caption:{caption}|size:{r.Right - r.Left},{r.Bottom - r.Top}|client:{NativeClient(hwnd)}|border:{(GetWindowLongPtrW(hwnd, -16).ToInt64() & 0x00800000) != 0}|clientedge:{(GetWindowLongPtrW(hwnd, -20).ToInt64() & 0x00000200) != 0}|mapped:{ReferenceEquals(Control.FromHandle(hwnd), c)}");
    }

    private static void CreateParameter()
    {
        foreach (string kind in new[] { "null", "string", "pointer", "payload", "object" })
        {
            using var c = New("parameter-" + kind);
            c.ParameterKind = kind;
            c.NativeParameter = ParameterValue(kind);
            Step("create-" + kind, () => _ = c.Handle, c);
            Record("creation-observed-" + kind, c.CreationMessages);
            Record("native-created-" + kind, c.IsHandleCreated && IsWindow(c.Handle));
        }
    }

    private static object? ParameterValue(string kind) => kind switch
    {
        "string" => "parameter \u4e2d\u6587",
        "pointer" => (nint)0x12345678,
        "payload" => new Payload { Number = 135, Handle = (nint)246 },
        "object" => new object(),
        _ => null
    };

    private static string CreationParameter(Message m, string kind)
    {
        NativeCreateStruct create = Marshal.PtrToStructure<NativeCreateStruct>(m.LParam);
        nint pointer = create.Parameter;
        if (pointer == 0) return "null";
        return kind switch
        {
            "string" => Marshal.PtrToStringUni(pointer) ?? "null",
            "pointer" => "boxed:" + Marshal.ReadIntPtr(pointer),
            "payload" => PayloadText(Marshal.PtrToStructure<Payload>(pointer)),
            _ => "nonzero"
        };
    }

    private static string PayloadText(Payload payload) => $"payload:{payload.Number},{payload.Handle}";

    private static void FormMessages()
    {
        using var form = new ProbeForm { Text = "form native caption", Bounds = new Rectangle(40, 40, 260, 180) };
        form.NativeParameter = "form parameter";
        Step("form-create", () => _ = form.Handle);
        Record("form-creation-messages", form.CreationMessages);
        Step("form-custom-message", () => Record("form-result", SendMessageW(form.Handle, ProbeMessage, (nint)11, (nint)22)));
        Message message = Message.Create(form.Handle, 0x000E, 0, 0);
        Step("form-default", () => form.CallDefault(ref message));
        Record("form-default-result", message.Result);
        form.NativeParameter = "form replacement";
        Step("form-recreate", form.Rebuild);
        Record("form-creation-after-recreate", form.CreationMessages);
        form.Show(); Pump();
        Step("form-close", form.Close);
        Record("form-disposed", form.IsDisposed);
    }

    private static void PaintArguments()
    {
        using var bitmap = new Bitmap(32, 32);
        using Graphics graphics = Graphics.FromImage(bitmap);
        Rectangle clip = new(3, 4, -5, 6);
        var args = new PaintEventArgs(graphics, clip);
        Record("paint-clip", Rect(args.ClipRectangle));
        Record("graphics-identity", ReferenceEquals(graphics, args.Graphics));
        Step("device-context", () =>
        {
            var context = (IDeviceContext)args;
            nint hdc = context.GetHdc();
            try { Record("hdc-nonzero", hdc != 0); }
            finally { context.ReleaseHdc(); }
        });
        Step("dispose-args", args.Dispose);
        Step("borrowed-graphics-alive", () => { graphics.Clear(Color.Red); Record("pixel", Pixel(bitmap, 1, 1)); });
        Step("dispose-args-repeat", args.Dispose);
        Step("graphics-after-dispose", () => Record("same-graphics", ReferenceEquals(graphics, args.Graphics)));
        Step("null-graphics", () => { using var a = new PaintEventArgs(null!, Rectangle.Empty); Record("graphics-null", a.Graphics is null); });
        var invalidated = new InvalidateEventArgs(clip);
        Record("invalidated-rect", Rect(invalidated.InvalidRect));
    }

    private static void PaintDirect()
    {
        using var c = New("direct");
        c.BackColor = Color.FromArgb(255, 12, 34, 56);
        using var bitmap = new Bitmap(100, 80);
        using Graphics graphics = Graphics.FromImage(bitmap);
        using var args = new PaintEventArgs(graphics, new Rectangle(2, 3, 20, 15));
        c.Paint += (_, e) => Trace("event-paint:identity=" + ReferenceEquals(e, args));
        graphics.Clear(Color.Fuchsia);
        Step("background", () => c.RaiseBackground(args), c);
        Record("background-pixels", Pixel(bitmap, 5, 6) + ";" + Pixel(bitmap, 70, 60));
        c.PaintColor = Color.Blue;
        Step("paint", () => c.RaisePaint(args), c);
        Record("paint-pixels", Pixel(bitmap, 5, 6) + ";" + Pixel(bitmap, 70, 60));
        c.SkipPaintBase = true;
        Step("paint-skip-base", () => c.RaisePaint(args), c);
        Step("null-paint", () => c.RaisePaint(null!), c);
        Step("null-background", () => c.RaiseBackground(null!), c);
        Record("never-created", c.IsHandleCreated);
        c.SkipPaintBase = false;
        Step("invoke-paint", () => c.CallPaint(c, args), c);
        Step("invoke-background", () => c.CallBackground(c, args), c);
        Step("invoke-paint-null-control", () => c.CallPaint(null!, args), c);
        Step("on-print", () => c.RaisePrint(args), c);
        Step("on-print-null", () => c.RaisePrint(null!), c);
        Graphics? created = null;
        Step("create-graphics", () => { created = c.CreateGraphics(); Record("created-graphics", GraphicsStatus(created)); }, c);
        created?.Dispose();
        Record("created-graphics-after-dispose", GraphicsStatus(created));
        c.ShiftBackground = true;
        Step("on-print-graphics-state", () => c.RaisePrint(args), c);
        Record("on-print-transform", $"{graphics.Transform.OffsetX},{graphics.Transform.OffsetY}");
    }

    private static void Invalidations()
    {
        using var c = New("invalidations");
        using var child = New("child");
        child.Bounds = new Rectangle(5, 6, 30, 20);
        c.Controls.Add(child);
        AttachInvalidation(c); AttachInvalidation(child);
        Step("uncreated-invalidate", c.Invalidate, c, child);
        Step("uncreated-update", c.Update, c, child);
        Step("uncreated-refresh", c.Refresh, c, child);
        _ = c.Handle; _ = child.Handle;
        ValidateRect(c.Handle, 0); ValidateRect(child.Handle, 0);
        void Check(string name, Action action)
        {
            ValidateRect(c.Handle, 0); ValidateRect(child.Handle, 0);
            Step(name, action, c, child);
            Record(name + "-regions", "parent:" + UpdateRect(c.Handle) + "|child:" + UpdateRect(child.Handle));
        }
        Check("all", c.Invalidate);
        Check("all-children-false", () => c.Invalidate(false));
        Check("all-children-true", () => c.Invalidate(true));
        Check("rectangle", () => c.Invalidate(new Rectangle(2, 3, 13, 17)));
        Check("rectangle-children", () => c.Invalidate(new Rectangle(7, 8, 11, 13), true));
        Check("empty-rectangle", () => c.Invalidate(Rectangle.Empty));
        Check("negative-rectangle", () => c.Invalidate(new Rectangle(2, 3, -4, -5)));
        using var region = new Region(new Rectangle(2, 3, 13, 17));
        region.Union(new Rectangle(40, 30, 5, 6));
        Check("region", () => c.Invalidate(region));
        Check("region-children", () => c.Invalidate(region, true));
        Check("null-region", () => c.Invalidate((Region?)null));
        Check("null-region-children", () => c.Invalidate((Region?)null, true));
        region.Dispose();
        Check("disposed-region", () => c.Invalidate(region));
        Check("update-no-region", c.Update);
        Check("refresh-hidden", c.Refresh);
    }

    private static void NativePaint()
    {
        using var host = new Form { Text = "message paint probe", Bounds = new Rectangle(40, 40, 260, 180) };
        using var c = New("paint");
        c.BackColor = Color.FromArgb(255, 12, 34, 56);
        c.PaintColor = Color.FromArgb(255, 90, 110, 130);
        host.Controls.Add(c);
        AttachInvalidation(c);
        c.Paint += (_, e) => Trace("event-paint:" + Rect(e.ClipRectangle));
        host.Show(); Pump();
        ValidateRect(c.Handle, 0);
        Step("native-invalidate", () => c.Invalidate(new Rectangle(2, 3, 20, 15)), c);
        Record("pending-region", UpdateRect(c.Handle));
        Step("native-update", c.Update, c);
        Record("after-update-region", UpdateRect(c.Handle));
        Step("native-refresh", c.Refresh, c);
        Step("print-client", () => PrintPixels(c), c);
        c.Buffered = true;
        Step("buffered-refresh", c.Refresh, c);
        Step("buffered-print", () => PrintPixels(c), c);
        c.WriteStyle(ControlStyles.Opaque, true);
        Step("opaque-refresh", c.Refresh, c);
        Step("opaque-print", () => PrintPixels(c), c);
        Record("last-graphics-after-native-paint", GraphicsStatus(c.LastGraphics));
        host.Close();
        Record("closed", !host.IsHandleCreated && !c.IsHandleCreated);
    }

    private static void PrintPixels(Control c)
    {
        using var bitmap = new Bitmap(Math.Max(1, c.ClientSize.Width), Math.Max(1, c.ClientSize.Height));
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.Fuchsia);
        nint dc = graphics.GetHdc();
        try { Record("print-result", SendMessageW(c.Handle, 0x0318, dc, (nint)0x000C)); }
        finally { graphics.ReleaseHdc(dc); }
        Record("printed-pixels", Pixel(bitmap, 5, 6) + ";" + Pixel(bitmap, bitmap.Width - 2, bitmap.Height - 2));
    }

    private static void NativeEdit()
    {
        using var host = new Form { Bounds = new Rectangle(40, 40, 260, 180) };
        using var edit = new ProbeTextBox { Bounds = new Rectangle(5, 5, 100, 30), Text = "editable" };
        host.Controls.Add(edit);
        host.Show(); Pump();
        Record("edit-user-paint", edit.ReadStyle(ControlStyles.UserPaint));
        Record("edit-native-font", SendMessageW(edit.Handle, 0x0031, 0, 0) != 0);
        Record("edit-text-length", SendMessageW(edit.Handle, 0x000E, 0, 0));
        int paints = 0;
        edit.Paint += (_, _) => paints++;
        Step("edit-native-message", () => Record("edit-custom-result", SendMessageW(edit.Handle, ProbeMessage, 0, 0)));
        Step("edit-refresh", edit.Refresh);
        Record("edit-paint-events", paints);
        Record("edit-after-update-region", UpdateRect(edit.Handle));
        host.Close();
        Record("edit-closed", !edit.IsHandleCreated);
    }

    private static void Transparent()
    {
        using var parent = New("parent");
        parent.BackColor = Color.FromArgb(255, 20, 40, 60);
        using var c = New("transparent");
        c.Location = new Point(4, 5);
        parent.Controls.Add(c);
        Step("unsupported-transparent", () => c.BackColor = Color.Transparent, c);
        c.WriteStyle(ControlStyles.SupportsTransparentBackColor, true);
        Step("supported-transparent", () => c.BackColor = Color.Transparent, c);
        using var bitmap = new Bitmap(100, 80);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.Fuchsia);
        using var args = new PaintEventArgs(graphics, new Rectangle(0, 0, 100, 80));
        Step("transparent-background", () => c.RaiseBackground(args), c);
        Record("transparent-pixel", Pixel(bitmap, 5, 6));
        c.BackColor = Color.FromArgb(128, 200, 100, 50);
        Step("alpha-background", () => c.RaiseBackground(args), c);
        Record("alpha-pixel", Pixel(bitmap, 5, 6));
    }

    private static void Reentrancy()
    {
        using var c = New("reentry");
        bool nested = false;
        c.StyleChanged += (_, _) =>
        {
            Trace("style-handler");
            if (nested) return;
            nested = true; c.WriteStyle(ControlStyles.Opaque, true); c.ApplyStyles();
        };
        Step("style-reentry", c.ApplyStyles, c);
        _ = c.Handle;
        c.Invalidated += (_, e) =>
        {
            Trace("invalidated-handler:" + Rect(e.InvalidRect));
            if (nested) return;
            nested = true; c.Invalidate(new Rectangle(9, 8, 7, 6));
        };
        nested = false;
        Step("invalidate-reentry", () => c.Invalidate(new Rectangle(1, 2, 3, 4)), c);
        using var bitmap = new Bitmap(100, 80);
        using Graphics graphics = Graphics.FromImage(bitmap);
        using var args = new PaintEventArgs(graphics, new Rectangle(0, 0, 10, 10));
        nested = false;
        c.Paint += (_, _) =>
        {
            Trace("paint-handler");
            if (nested) return;
            nested = true; c.RaisePaint(args);
        };
        Step("paint-reentry", () => c.RaisePaint(args), c);
    }

    private static void Exceptions()
    {
        using var style = New("style-failure");
        style.StyleChanged += (_, _) => throw Failure;
        Step("style-original", style.ApplyStyles, style);
        using var invalid = New("invalid-failure");
        _ = invalid.Handle;
        invalid.Invalidated += (_, _) => throw Failure;
        Step("invalid-original", invalid.Invalidate, invalid);
        Record("region-after-failure", UpdateRect(invalid.Handle));
        using var paint = New("paint-failure");
        paint.Paint += (_, _) => throw Failure;
        using var bitmap = new Bitmap(100, 80);
        using Graphics graphics = Graphics.FromImage(bitmap);
        using var args = new PaintEventArgs(graphics, Rectangle.Empty);
        Step("paint-original", () => paint.RaisePaint(args), paint);
        Record("graphics-after-callback-failure", GraphicsStatus(graphics));
        using var native = New("native-failure");
        _ = native.Handle;
        native.ThrowMessage = true;
        Message message = Message.Create(native.Handle, ProbeMessage, 0, 0);
        Step("direct-wndproc-original", () => native.CallWindow(ref message), native);
        native.ThrowMessage = false;
        native.ThrowParameters = true;
        Step("createparams-original", () => _ = native.Parameters, native);
        native.ThrowParameters = false;
    }

    private static ProbeControl New(string name) => new() { Name = name, Bounds = new Rectangle(0, 0, 100, 80) };
    private static void AttachInvalidation(Control c) => c.Invalidated += (_, e) => Trace("event-invalid:" + c.Name + ":" + Rect(e.InvalidRect));
    private static string StyleText(ProbeControl c) => $"mask:{c.StyleMask()}|buffered:{c.Buffered}|handle:{c.IsHandleCreated}|disposed:{c.IsDisposed}";
    private static void Step(string name, Action action, params ProbeControl[] controls)
    {
        Events.Clear(); _recording = true;
        Exception? error = null;
        try { action(); }
        catch (Exception e) { error = e; }
        finally { _recording = false; }
        Record(name, "exception:" + ExceptionText(error) + "|events:[" + string.Join(";", Events) + "]|state:[" + string.Join(";", controls.Select(StyleText)) + "]");
    }

    private static string ExceptionText(Exception? e) => e is null ? "none" : ReferenceEquals(e, Failure) ? "original" : e.GetType().Name + (e is ArgumentException a ? ":" + a.ParamName : "");
    private static string GraphicsStatus(Graphics? graphics)
    {
        if (graphics is null) return "null";
        try { _ = graphics.ClipBounds; return "alive"; }
        catch (Exception e) { return ExceptionText(e); }
    }
    private static string Pixel(Bitmap bitmap, int x, int y) => bitmap.GetPixel(x, y).ToArgb().ToString("X8", CultureInfo.InvariantCulture);
    private static string Rect(Rectangle r) => $"{r.X},{r.Y},{r.Width},{r.Height}";
    private static void Trace(string value) { if (_recording) Events.Add(value); }
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + (value?.ToString() ?? "null"));
    private static string NativeClient(nint hwnd) { _ = GetClientRect(hwnd, out NativeRect r); return $"{r.Right - r.Left},{r.Bottom - r.Top}"; }
    private static string UpdateRect(nint hwnd) => GetUpdateRect(hwnd, out NativeRect r, false) ? $"{r.Left},{r.Top},{r.Right - r.Left},{r.Bottom - r.Top}" : "empty";

    private sealed class ProbeControl : Control
    {
        internal int ExtraStyle, ExtraExStyle;
        internal string? NativeCaption;
        internal object? NativeParameter;
        internal string? ParameterKind;
        internal int CreationMessages;
        internal Rectangle? NativeBounds;
        internal bool RewriteToTextLength, ThrowMessage, ThrowParameters, SkipPaintBase, ShiftBackground;
        internal Color PaintColor = Color.Empty;
        internal Graphics? LastGraphics;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        internal bool Buffered { get => DoubleBuffered; set => DoubleBuffered = value; }
        internal CreateParams Parameters => CreateParams;
        internal bool ReadStyle(ControlStyles style) => GetStyle(style);
        internal void WriteStyle(ControlStyles style, bool value) => SetStyle(style, value);
        internal int StyleMask()
        {
            int value = 0;
            for (int bit = 0; bit < 32; bit++) if (GetStyle(unchecked((ControlStyles)(1 << bit)))) value |= 1 << bit;
            return value;
        }
        internal void ApplyStyles() => UpdateStyles();
        internal void RaiseStyle(EventArgs e) => OnStyleChanged(e);
        internal void RaisePaint(PaintEventArgs e) => OnPaint(e);
        internal void RaiseBackground(PaintEventArgs e) => OnPaintBackground(e);
        internal void RaisePrint(PaintEventArgs e) => OnPrint(e);
        internal void CallPaint(Control c, PaintEventArgs e) => InvokePaint(c, e);
        internal void CallBackground(Control c, PaintEventArgs e) => InvokePaintBackground(c, e);
        internal void CallWindow(ref Message message) => WndProc(ref message);
        internal void CallDefault(ref Message message) => DefWndProc(ref message);
        internal void Rebuild() => RecreateHandle();
        protected override CreateParams CreateParams
        {
            get
            {
                if (ThrowParameters) throw Failure;
                CreateParams p = base.CreateParams;
                p.Style |= ExtraStyle; p.ExStyle |= ExtraExStyle;
                p.Param = NativeParameter;
                if (NativeCaption is not null) p.Caption = NativeCaption;
                if (NativeBounds is Rectangle r) { p.X = r.X; p.Y = r.Y; p.Width = r.Width; p.Height = r.Height; }
                return p;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (ParameterKind is not null && m.Msg is 0x0081 or 0x0001)
            {
                CreationMessages++;
                Trace("creation-" + m.Msg + ":" + CreationParameter(m, ParameterKind));
            }
            if (m.Msg == ProbeMessage)
            {
                Trace("wnd-enter:" + m.WParam + "," + m.LParam);
                if (ThrowMessage) throw Failure;
                if (!RewriteToTextLength) { m.Result = (nint)2468; Trace("wnd-handled"); return; }
                m.Msg = 0x000E;
                base.WndProc(ref m);
                Trace("wnd-rewritten-result:" + m.Result);
                return;
            }
            base.WndProc(ref m);
        }
        protected override void OnStyleChanged(EventArgs e) { Trace("on-style-enter"); base.OnStyleChanged(e); Trace("on-style-exit"); }
        protected override void NotifyInvalidate(Rectangle area) { Trace("notify-invalid-enter:" + Name + ":" + Rect(area)); base.NotifyInvalidate(area); Trace("notify-invalid-exit:" + Name); }
        protected override void OnInvalidated(InvalidateEventArgs e) { Trace("on-invalid-enter:" + Name + ":" + (e is null ? "null" : Rect(e.InvalidRect))); base.OnInvalidated(e!); Trace("on-invalid-exit:" + Name); }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Trace("background-enter:" + (e is null ? "null" : Rect(e.ClipRectangle)));
            base.OnPaintBackground(e!);
            if (ShiftBackground && e is not null) e.Graphics.TranslateTransform(5, 6);
            Trace("background-exit");
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Trace("paint-enter:" + (e is null ? "null" : Rect(e.ClipRectangle)));
            if (e is not null)
            {
                LastGraphics = e.Graphics;
                if (ShiftBackground) Trace("paint-transform:" + e.Graphics.Transform.OffsetX + "," + e.Graphics.Transform.OffsetY);
                if (!PaintColor.IsEmpty)
                {
                    using var brush = new SolidBrush(PaintColor);
                    e.Graphics.FillRectangle(brush, new Rectangle(0, 0, 20, 20));
                }
            }
            if (!SkipPaintBase) base.OnPaint(e!);
            Trace("paint-exit");
        }
    }

    private sealed class ProbeTextBox : TextBox
    {
        internal bool ReadStyle(ControlStyles style) => GetStyle(style);
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == ProbeMessage) { Trace("edit-wndproc"); m.Result = (nint)1357; return; }
            base.WndProc(ref m);
        }
    }

    private sealed class ProbeForm : Form
    {
        internal string? NativeParameter;
        internal int CreationMessages;
        internal void Rebuild() => RecreateHandle();
        internal void CallDefault(ref Message message) => DefWndProc(ref message);
        protected override CreateParams CreateParams
        {
            get { CreateParams p = base.CreateParams; p.Param = NativeParameter; return p; }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg is 0x0081 or 0x0001)
            {
                CreationMessages++;
                Trace("form-creation-" + m.Msg + ":" + CreationParameter(m, "string"));
            }
            if (m.Msg == ProbeMessage)
            {
                Trace("form-wnd-enter:" + m.WParam + "," + m.LParam);
                m.Result = (nint)9753;
                return;
            }
            base.WndProc(ref m);
        }
    }

    private static void Pump()
    {
        long deadline = Environment.TickCount64 + 2000;
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (message.Message == 0x0012) { PostQuitMessage(unchecked((int)message.WParam)); return; }
            if (++count > 4096 || Environment.TickCount64 > deadline) throw new TimeoutException("Paint message pump did not become idle.");
            _ = TranslateMessage(in message); _ = DispatchMessageW(in message);
        }
    }

    [StructLayout(LayoutKind.Sequential)] private struct Payload { public int Number; public nint Handle; }
    [StructLayout(LayoutKind.Sequential)] private struct NativeCreateStruct
    {
        public nint Parameter, Instance, Menu, Parent;
        public int Height, Width, Y, X, Style;
        public nint Name, Class;
        public uint ExStyle;
    }
    [StructLayout(LayoutKind.Sequential)] private struct NativeRect { public int Left, Top, Right, Bottom; }
    [StructLayout(LayoutKind.Sequential)] private struct NativeMessage { public nint HWnd; public uint Message; public nuint WParam; public nint LParam; public uint Time; public Point Point; public uint Private; }
    [DllImport("user32.dll")] private static extern nint SendMessageW(nint window, int message, nint wParam, nint lParam);
    [DllImport("user32.dll")] private static extern nint GetWindowLongPtrW(nint window, int index);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetWindowTextW(nint window, StringBuilder text, int count);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool GetWindowRect(nint window, out NativeRect rect);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool GetClientRect(nint window, out NativeRect rect);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool GetUpdateRect(nint window, out NativeRect rect, [MarshalAs(UnmanagedType.Bool)] bool erase);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool ValidateRect(nint window, nint rect);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")] private static extern nint DispatchMessageW(in NativeMessage message);
    [DllImport("user32.dll")] private static extern void PostQuitMessage(int exitCode);
}
