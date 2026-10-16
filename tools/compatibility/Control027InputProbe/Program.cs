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

// Identical source is compiled for fixed WinForms 10.0.11 and Lumina.
// Each scenario runs in its own STA process. Injected window messages are not
// evidence of physical mouse input. stdout is compared without substitutions;
// runtime identity and complete exception details are retained on stderr.
internal static class Program
{
    private static readonly List<string> Trace = [];
    private static readonly Exception Failure = new InvalidOperationException("control027 mouse callback failure");
    private static bool _recording;
    private static object? _expectedArgs;
    private static readonly string[] MouseNames = ["Down", "Up", "Click", "DoubleClick", "Wheel", "Move", "Enter", "Leave", "Hover", "CaptureChanged"];

    [STAThread]
    private static void Main(string[] args)
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        Console.Error.WriteLine("runtime=" + RuntimeInformation.FrameworkDescription);
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        Console.Error.WriteLine("reference-assembly=" + version);
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
            throw new InvalidOperationException("Expected WinForms 10.0.11, found " + version);
#endif
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        if (args.Length != 1) throw new ArgumentException("Specify one Control027 scenario.");
        Record("scenario", args[0]);
        Record("input-evidence", args[0].StartsWith("message-", StringComparison.Ordinal) || args[0].StartsWith("consumer-", StringComparison.Ordinal) ? "programmatic-window-messages;physical-device-not-verified" : "managed-api-and-native-state;physical-device-not-verified");
        switch (args[0])
        {
            case "arguments": Arguments(); break;
            case "direct-events": Direct("normal"); break;
            case "direct-null": Direct("null"); break;
            case "direct-skip": Direct("skip"); break;
            case "direct-exception": Direct("exception"); break;
            case "direct-reentrant": DirectReentrant(); break;
            case "direct-helpers": DirectHelpers(); break;
            case "coordinates-uncreated": CoordinatesUncreated(); break;
            case "coordinates-parked": CoordinatesNested(false); break;
            case "coordinates-nested": CoordinatesNested(true); break;
            case "coordinates-disposed": CoordinatesDisposed(); break;
            case "capture-uncreated": CaptureUncreated(); break;
            case "capture-transfer": CaptureTransfer(); break;
            case "capture-dispose": CaptureDispose(); break;
            case "message-buttons": Messages("buttons"); break;
            case "message-double": Messages("double"); break;
            case "message-styles": Messages("styles"); break;
            case "message-wheel": Messages("wheel"); break;
            case "message-track": Messages("track"); break;
            case "message-capture": Messages("capture"); break;
            case "tracking-lifecycle": TrackingLifecycle(); break;
            case "consumer-button": Consumer("button"); break;
            case "consumer-picturebox": Consumer("picturebox"); break;
            case "consumer-checkbox": Consumer("checkbox"); break;
            case "consumer-radiobutton": Consumer("radiobutton"); break;
#if REFERENCE
            case "consumer-button-system": Consumer("button", true); break;
            case "consumer-checkbox-system": Consumer("checkbox", true); break;
            case "consumer-radiobutton-system": Consumer("radiobutton", true); break;
#endif
            default: throw new ArgumentException("Unknown scenario " + args[0]);
        }
        Record("complete", true);
    }

    private static void Arguments()
    {
        Record("mouse-args-sealed", typeof(MouseEventArgs).IsSealed);
        foreach (MouseButtons button in Enum.GetValues<MouseButtons>()) Record("button-" + button, (int)button);
        foreach (MouseEventArgs e in new MouseEventArgs[]
        {
            new(MouseButtons.None, 0, 0, 0, 0),
            new(MouseButtons.Left | MouseButtons.Right, -2, -32768, 32767, -120),
            new((MouseButtons)(-1), int.MinValue, int.MaxValue, int.MinValue, int.MaxValue),
            new HandledMouseEventArgs(MouseButtons.XButton2, 2, 17, 19, 120),
            new HandledMouseEventArgs(MouseButtons.Middle, 3, -7, -9, -240, true)
        })
        {
            Record("args", Args(e));
            Record("location", PointText(e.Location));
            if (e is HandledMouseEventArgs handled)
            {
                handled.Handled = !handled.Handled;
                Record("toggled", Args(handled));
            }
        }
    }

    private static void Direct(string mode)
    {
        foreach (string name in MouseNames)
        {
            using ProbeControl c = New("target");
            c.SkipBase = mode == "skip";
            if (mode == "exception") c.FailingEvent = name;
            object? e = mode == "null" ? null : name is "Enter" or "Leave" or "Hover" or "CaptureChanged"
                ? new EventArgs() : new HandledMouseEventArgs(MouseButtons.Left | MouseButtons.XButton1, 2, -4, 9, -120, true);
            _expectedArgs = e;
            Step("raise-" + name, () => c.Raise(name, e), c);
        }
        _expectedArgs = null;
    }

    private static void DirectReentrant()
    {
        using ProbeControl c = New("target");
        bool nested = false;
        c.MouseDown += (_, e) =>
        {
            Emit("handler-reentry-enter:" + Args(e));
            if (!nested) { nested = true; c.Raise("Up", new MouseEventArgs(MouseButtons.Right, 3, 11, 13, 0)); }
            Emit("handler-reentry-exit");
        };
        Step("raise-nested", () => c.Raise("Down", new MouseEventArgs(MouseButtons.Left, 1, 2, 3, 0)), c);
        c.MouseLeave += (_, _) => c.Dispose();
        Step("dispose-in-event", () => c.Raise("Leave", EventArgs.Empty), c);
        Step("raise-after-dispose", () => c.Raise("Move", new MouseEventArgs(MouseButtons.None, 0, 8, 9, 0)), c);
    }

    private static void DirectHelpers()
    {
        using ProbeControl c = New("caller");
        using ProbeControl target = New("target");
        Step("invoke-click-null-target", () => c.CallClick(null, null!), c, target);
        Step("invoke-click-target", () => c.CallClick(target, new EventArgs()), c, target);
        Step("invoke-click-null-args", () => c.CallClick(target, null!), c, target);
        object key = new();
        MouseEventHandler handler = (sender, e) => Emit("custom-mouse:" + ReferenceEquals(sender, c) + ":" + Args(e));
        c.AddCustomEvent(key, handler);
        Step("custom-key", () => c.CallMouseEvent(key, new MouseEventArgs(MouseButtons.Middle, 2, 5, 7, 120)), c);
        Step("custom-null-args", () => c.CallMouseEvent(key, null!), c);
        Step("unknown-key", () => c.CallMouseEvent(new object(), null!), c);
        Step("null-key", () => c.CallMouseEvent(null!, null!), c);
        c.AddCustomEvent("wrong-kind", new EventHandler((_, _) => Emit("wrong-handler")));
        Step("wrong-delegate-kind", () => c.CallMouseEvent("wrong-kind", null!), c);
        Step("reset-no-handle", c.ResetMouse, c);
    }

    private static void CoordinatesUncreated()
    {
        foreach (string name in new[] { "point-screen", "point-client", "rectangle-screen", "rectangle-client" })
        {
            using ProbeControl c = New("target");
            c.Location = new Point(107, 83);
            Record(name + "-before", State(c));
            Step(name, () =>
            {
                switch (name)
                {
                    case "point-screen": Record("value", PointText(c.PointToScreen(new Point(-7, 11)))); break;
                    case "point-client": Record("value", PointText(c.PointToClient(new Point(-7, 11)))); break;
                    case "rectangle-screen": Record("value", RectangleText(c.RectangleToScreen(new Rectangle(-7, 11, 13, 17)))); break;
                    case "rectangle-client": Record("value", RectangleText(c.RectangleToClient(new Rectangle(-7, 11, 13, 17)))); break;
                }
            }, c);
            Record("native-window", c.IsHandleCreated && IsWindow(c.Handle));
        }
    }

    private static void CoordinatesNested(bool createParents)
    {
        using var host = new Form { StartPosition = FormStartPosition.Manual, Location = new Point(107, 83), ClientSize = new Size(300, 240), Text = "Control027 coordinates" };
        using ProbeControl parent = New("parent");
        using ProbeControl child = New("child");
        parent.Bounds = new Rectangle(19, 23, 180, 150);
        child.Bounds = new Rectangle(7, 11, 80, 60);
        host.Controls.Add(parent);
        parent.Controls.Add(child);
        if (createParents) { _ = host.Handle; _ = parent.Handle; }
        _ = child.Handle;
        Record("tree-handles", $"{host.IsHandleCreated},{parent.IsHandleCreated},{child.IsHandleCreated}");
        foreach (Point point in new[] { Point.Empty, new Point(-7, 11), new Point(32768, -32769), new Point(int.MaxValue, int.MinValue) })
        {
            Point nativeScreen = point;
            _ = ClientToScreen(child.Handle, ref nativeScreen);
            Point actualScreen = child.PointToScreen(point);
            Record("point-screen", "input:" + PointText(point) + "|actual:" + PointText(actualScreen) + "|native:" + PointText(nativeScreen));
            Point nativeClient = point;
            _ = ScreenToClient(child.Handle, ref nativeClient);
            Point actualClient = child.PointToClient(point);
            Record("point-client", "input:" + PointText(point) + "|actual:" + PointText(actualClient) + "|native:" + PointText(nativeClient));
            Record("point-roundtrip", child.PointToClient(actualScreen) == point);
        }
        foreach (Rectangle rectangle in new[] { Rectangle.Empty, new Rectangle(-7, 11, 13, 17), new Rectangle(5, 6, -7, -8), new Rectangle(int.MaxValue, int.MinValue, 3, -5) })
        {
            Rectangle screen = child.RectangleToScreen(rectangle);
            Rectangle client = child.RectangleToClient(rectangle);
            Record("rectangle-screen", "input:" + RectangleText(rectangle) + "|actual:" + RectangleText(screen) + "|native:" + RectangleText(MapRectangle(child.Handle, 0, rectangle)));
            Record("rectangle-client", "input:" + RectangleText(rectangle) + "|actual:" + RectangleText(client) + "|native:" + RectangleText(MapRectangle(0, child.Handle, rectangle)));
            Record("rectangle-roundtrip", child.RectangleToClient(screen) == rectangle);
        }
        Record("native-alive", IsWindow(child.Handle));
    }

    private static void CoordinatesDisposed()
    {
        foreach (string name in new[] { "point-screen", "point-client", "rectangle-screen", "rectangle-client" })
        {
            using ProbeControl c = New("target");
            c.Dispose();
            Step(name, () =>
            {
                switch (name)
                {
                    case "point-screen": Record("value", PointText(c.PointToScreen(Point.Empty))); break;
                    case "point-client": Record("value", PointText(c.PointToClient(Point.Empty))); break;
                    case "rectangle-screen": Record("value", RectangleText(c.RectangleToScreen(Rectangle.Empty))); break;
                    case "rectangle-client": Record("value", RectangleText(c.RectangleToClient(Rectangle.Empty))); break;
                }
            }, c);
        }
    }

    private static void CaptureUncreated()
    {
        using ProbeControl c = New("target");
        Record("initial", State(c));
        Step("false-before-handle", () => c.Capture = false, c);
        Step("true-before-handle", () => c.Capture = true, c);
        Step("same-true", () => c.Capture = true, c);
        Step("release", () => c.Capture = false, c);
        Step("same-false", () => c.Capture = false, c);
        c.Dispose();
        Step("disposed-true", () => c.Capture = true, c);
    }

    private static void CaptureTransfer()
    {
        using ProbeControl first = New("first");
        using ProbeControl second = New("second");
        _ = first.Handle;
        _ = second.Handle;
        Step("first-capture", () => first.Capture = true, first, second);
        Step("transfer-second", () => second.Capture = true, first, second);
        Step("first-false-preserves-second", () => first.Capture = false, first, second);
        Step("external-release", () => Record("release-return", ReleaseCapture()), first, second);
        Step("native-first", () => SetCapture(first.Handle), first, second);
        Step("native-second", () => SetCapture(second.Handle), first, second);
        Step("second-release", () => second.Capture = false, first, second);
    }

    private static void CaptureDispose()
    {
        using ProbeControl c = New("target");
        c.Capture = true;
        nint oldHandle = c.Handle;
        Step("dispose-captured", c.Dispose, c);
        Record("old-native-alive", IsWindow(oldHandle));
        Record("native-capture-zero", GetCapture() == 0);
    }

    private static void Messages(string mode)
    {
        using var host = new Form { StartPosition = FormStartPosition.Manual, Location = new Point(107, 83), ClientSize = new Size(300, 240), Text = "Control027 injected mouse messages" };
        using ProbeControl c = New("target");
        c.Location = new Point(25, 35);
        host.Controls.Add(c);
        host.Show();
        Pump();
        Record("native-topmost", SetWindowPos(host.Handle, -1, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0010));
        Record("native-ready", IsWindow(host.Handle) && IsWindow(c.Handle));
        Point hitPoint = c.PointToScreen(new Point(8, 9));
        NativeDiagnostics(host, c, hitPoint);
        Record("native-hit-point", PointText(hitPoint));
        Record("native-hit-target", WindowFromPoint(hitPoint) == c.Handle);
        Record("style", "click:" + c.ReadStyle(ControlStyles.StandardClick) + "|double:" + c.ReadStyle(ControlStyles.StandardDoubleClick));
        Record("global-mouse-buttons", (int)Control.MouseButtons);
        if (mode == "buttons")
        {
            foreach ((string name, int down, int up, int flags) in new[]
            {
                ("left", 0x0201, 0x0202, 1), ("right", 0x0204, 0x0205, 2),
                ("middle", 0x0207, 0x0208, 16), ("x1", 0x020B, 0x020C, 1 << 16), ("x2", 0x020B, 0x020C, 2 << 16)
            })
            {
                Deliver(c, name + "-down", down, flags, 8, 9);
                Deliver(c, name + "-up", up, flags & unchecked((int)0xFFFF0000), 8, 9);
            }
            Deliver(c, "signed-down", 0x0201, 1, -7, -9);
            Deliver(c, "signed-up-outside", 0x0202, 0, -7, -9);
        }
        else if (mode == "double")
        {
            foreach ((string name, int down, int up, int flags) in new[]
            {
                ("left", 0x0203, 0x0202, 1), ("right", 0x0206, 0x0205, 2),
                ("middle", 0x0209, 0x0208, 16), ("x1", 0x020D, 0x020C, 1 << 16), ("x2", 0x020D, 0x020C, 2 << 16)
            })
            {
                Deliver(c, name + "-double", down, flags, 8, 9);
                Deliver(c, name + "-up", up, flags & unchecked((int)0xFFFF0000), 8, 9);
            }
        }
        else if (mode == "styles")
        {
            foreach ((bool click, bool twice) in new[] { (false, false), (true, false), (false, true), (true, true) })
            {
                c.WriteStyle(ControlStyles.StandardClick, click);
                c.WriteStyle(ControlStyles.StandardDoubleClick, twice);
                string label = "click-" + click + "-double-" + twice;
                Deliver(c, label + "-down", 0x0201, 1, 8, 9);
                Deliver(c, label + "-up", 0x0202, 0, 8, 9);
                Deliver(c, label + "-twice", 0x0203, 1, 8, 9);
                Deliver(c, label + "-twice-up", 0x0202, 0, 8, 9);
            }
        }
        else if (mode == "wheel")
        {
            Point screen = c.PointToScreen(new Point(8, 9));
            foreach (int delta in new[] { 120, -240, 0 }) Deliver(c, "wheel-" + delta, 0x020A, unchecked(delta << 16) | 1, screen.X, screen.Y);
            c.MouseWheel += (_, e) => { if (e is HandledMouseEventArgs handled) handled.Handled = true; };
            Deliver(c, "wheel-handled", 0x020A, 120 << 16, screen.X, screen.Y);
        }
        else if (mode == "track")
        {
            Deliver(c, "move-first", 0x0200, 0, 8, 9);
            Deliver(c, "move-flags-only", 0x0200, 1 | 2 | 16 | 32 | 64, -7, -9);
            Deliver(c, "hover", 0x02A1, 0, 8, 9);
            Deliver(c, "leave", 0x02A3, 0, 0, 0);
            Deliver(c, "leave-repeated", 0x02A3, 0, 0, 0);
            Deliver(c, "move-after-leave", 0x0200, 0, 12, 13);
            Step("sendmessage-move", () => Record("native-result", SendMessageW(c.Handle, 0x0200, 0, Pack(14, 15))), c);
            Pump();
        }
        else
        {
            c.Capture = true;
            Deliver(c, "injected-capturechanged", 0x0215, 0, 0, 0);
            Record("injected-keeps-native-capture", GetCapture() == c.Handle);
            Step("native-release", () => Record("release-return", ReleaseCapture()), c);
        }
        host.Close();
        Record("closed", !host.IsHandleCreated && !c.IsHandleCreated);
    }

    private static void Consumer(string kind, bool system = false)
    {
        Record("consumer-evidence", "WM_COMMAND/BM_CLICK/SendMessage;scripted-cursor-position;not-physical-device-input");
        using var cursor = new CursorPositionScope();
        using var host = new Form { StartPosition = FormStartPosition.Manual, Location = new Point(107, 83), ClientSize = new Size(300, 240), Text = "Control027 consumer " + kind };
        using Control c = kind switch { "button" => new ProbeButton(), "picturebox" => new ProbePictureBox(), "checkbox" => new ProbeCheckBox(), _ => new ProbeRadioButton() };
#if REFERENCE
        if (system)
        {
            ((ButtonBase)c).FlatStyle = FlatStyle.System;
            Record("reference-only-mode", "FlatStyle.System;Lumina does not expose this API;excluded from shared-source parity counts");
        }
#endif
        c.Name = kind;
        c.Bounds = new Rectangle(25, 35, 100, 80);
        c.Text = kind;
        c.Click += (_, e) => Emit("base-Click:" + Args(e));
        c.DoubleClick += (_, e) => Emit("base-DoubleClick:" + Args(e));
        c.MouseDown += (_, e) => Emit("MouseDown:" + Args(e) + ":capture=" + c.Capture);
        c.MouseUp += (_, e) => Emit("MouseUp:" + Args(e) + ":capture=" + c.Capture);
        c.MouseClick += (_, e) => Emit("MouseClick:" + Args(e));
        c.MouseDoubleClick += (_, e) => Emit("MouseDoubleClick:" + Args(e));
        c.MouseCaptureChanged += (_, _) => Emit("CaptureChanged:" + c.Capture);
        switch (c)
        {
            case ProbeButton button: button.Click += (_, e) => Emit("typed-Click:" + Args(e)); break;
            case ProbePictureBox picture: picture.Click += (_, e) => Emit("typed-Click:" + Args(e)); break;
            case ProbeCheckBox check:
                check.Click += (_, e) => Emit("typed-Click:" + Args(e));
                check.CheckedChanged += (_, _) => Emit("CheckedChanged:" + check.Checked);
                break;
            case ProbeRadioButton radio:
                radio.Click += (_, e) => Emit("typed-Click:" + Args(e));
                radio.CheckedChanged += (_, _) => Emit("CheckedChanged:" + radio.Checked);
                break;
        }
        host.Controls.Add(c);
        host.Show();
        Pump();
        Record("native-topmost", SetWindowPos(host.Handle, -1, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0010));
        Point screen = c.PointToScreen(new Point(8, 9));
        NativeDiagnostics(host, c, screen);
        Record("cursor-positioned", SetCursorPos(screen.X, screen.Y));
        Pump();
        Record("native-point", PointText(screen));
        Record("native-hit-target", WindowFromPoint(screen) == c.Handle);
        Record("managed-mouse-position", PointText(Control.MousePosition));
        var styles = (IProbeStyles)c;
        Record("style", "click:" + styles.Read(ControlStyles.StandardClick) + "|double:" + styles.Read(ControlStyles.StandardDoubleClick) + "|userMouse:" + styles.Read(ControlStyles.UserMouse) + "|userPaint:" + styles.Read(ControlStyles.UserPaint));
        void Operation(string name, Action action)
        {
            Step(name, action);
            Record("consumer-state", "capture:" + c.Capture + "|checked:" + (c is CheckBox check ? check.Checked.ToString() : c is RadioButton radio ? radio.Checked.ToString() : "n/a") + "|nativeCheck:" + SendMessageW(c.Handle, 0x00F0, 0, 0));
        }
        using ProbeControl invoker = New("invoker");
        Operation("invoke-control-click", () => invoker.CallClick(c, new EventArgs()));
        Operation("parent-command-click", () => Record("native-result", SendMessageW(host.Handle, 0x0111, GetDlgCtrlID(c.Handle) & 0xFFFF, c.Handle)));
        Operation("native-bm-click", () => Record("native-result", SendMessageW(c.Handle, 0x00F5, 0, 0)));
        Operation("native-down", () => Record("native-result", SendMessageW(c.Handle, 0x0201, 1, Pack(8, 9))));
        Operation("native-up", () => Record("native-result", SendMessageW(c.Handle, 0x0202, 0, Pack(8, 9))));
        Operation("native-double", () => Record("native-result", SendMessageW(c.Handle, 0x0203, 1, Pack(8, 9))));
        Operation("native-double-up", () => Record("native-result", SendMessageW(c.Handle, 0x0202, 0, Pack(8, 9))));
        if (c is Button perform) Operation("perform-click", perform.PerformClick);
        nint oldHost = host.Handle;
        nint oldChild = c.Handle;
        host.Close();
        Record("closed-managed", !host.IsHandleCreated && !c.IsHandleCreated);
        Record("closed-native", !IsWindow(oldHost) && !IsWindow(oldChild));
    }

    private static void TrackingLifecycle()
    {
        Record("tracking-evidence", "SendMessage on real HWND;no physical motion or unobstructed hit-test required");
        foreach (string mode in new[] { "recreate", "destroy", "external", "dispose", "visibility" })
        {
            using ProbeControl c = New(mode);
            c.RecordLifecycle = true;
            nint original = c.Handle;
            Step(mode + "-first-move", () => Record("native-result", SendMessageW(c.Handle, 0x0200, 0, Pack(8, 9))), c);
            Step(mode + "-reset-tracking", c.ResetMouse, c);
            Step(mode + "-transition", () =>
            {
                switch (mode)
                {
                    case "recreate": c.Rebuild(); break;
                    case "destroy": c.DirectDestroy(); break;
                    case "external": Record("destroy-native-result", DestroyWindow(original)); break;
                    case "dispose": c.Dispose(); break;
                    case "visibility": c.Visible = false; c.Visible = true; break;
                }
            }, c);
            Record(mode + "-original-alive", IsWindow(original));
            if (!c.IsDisposed)
                Step(mode + "-next-move", () => Record("native-result", SendMessageW(c.Handle, 0x0200, 0, Pack(10, 11))), c);
            Step(mode + "-dispose", c.Dispose, c);
            Record(mode + "-final-original-alive", IsWindow(original));
        }
    }

    private static void Deliver(ProbeControl c, string name, int message, int flags, int x, int y)
    {
        Step(name, () =>
        {
            Message m = Message.Create(c.Handle, message, unchecked((nint)flags), Pack(x, y));
            c.CallWindow(ref m);
            Record("message-result", m.Result);
        }, c);
    }

    private static ProbeControl New(string name) => new() { Name = name, Size = new Size(100, 80) };
    private static void Step(string name, Action action, params ProbeControl[] controls)
    {
        Trace.Clear();
        _recording = true;
        Exception? error = null;
        try { action(); }
        catch (Exception e) { error = e; Console.Error.WriteLine(name + ":" + e); }
        finally { _recording = false; }
        Record(name, "exception:" + Error(error) + "|events:[" + string.Join(";", Trace) + "]|state:[" + string.Join(";", controls.Select(State)) + "]");
    }

    private static string Error(Exception? e) => e is null ? "none" : e.GetType().Name + ":" + e.Message + "|original:" + ReferenceEquals(e, Failure) + (e is ArgumentException argument ? "|parameter:" + argument.ParamName : "");
    private static string Args(object? e) => e is MouseEventArgs mouse ? $"{e.GetType().Name}:{(int)mouse.Button},{mouse.Clicks},{mouse.X},{mouse.Y},{mouse.Delta}" + (e is HandledMouseEventArgs handled ? ":handled=" + handled.Handled : "") : e?.GetType().Name ?? "null";
    private static string State(ProbeControl c) => $"{c.Name}:handle={c.IsHandleCreated},capture={c.Capture},disposed={c.IsDisposed},nativeCapture={(c.IsHandleCreated && GetCapture() == c.Handle)}";
    private static string PointText(Point p) => $"{p.X},{p.Y}";
    private static string RectangleText(Rectangle r) => $"{r.X},{r.Y},{r.Width},{r.Height}";
    private static void Emit(string value) { if (_recording) Trace.Add(value); }
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + value);
    private static nint Pack(int x, int y) => unchecked((nint)((x & 0xFFFF) | ((y & 0xFFFF) << 16)));

    private static void NativeDiagnostics(Control host, Control child, Point point)
    {
        foreach ((string name, nint hwnd) in new[] { ("host", host.Handle), ("child", child.Handle), ("hit", WindowFromPoint(point)) })
        {
            GetWindowRect(hwnd, out NativeRectangle rect);
            var nameBuffer = new StringBuilder(256);
            _ = GetClassNameW(hwnd, nameBuffer, nameBuffer.Capacity);
            Console.Error.WriteLine($"native-{name}:hwnd={hwnd:X};class={nameBuffer};id={GetDlgCtrlID(hwnd)};visible={IsWindowVisible(hwnd)};enabled={IsWindowEnabled(hwnd)};rect={rect.Left},{rect.Top},{rect.Right},{rect.Bottom}");
        }
    }

    private sealed class ProbeControl : Control
    {
        internal bool SkipBase;
        internal bool RecordLifecycle;
        internal string? FailingEvent;
        internal ProbeControl()
        {
            MouseDown += (s, e) => Observed("Down", s, e);
            MouseUp += (s, e) => Observed("Up", s, e);
            MouseClick += (s, e) => Observed("Click", s, e);
            MouseDoubleClick += (s, e) => Observed("DoubleClick", s, e);
            MouseWheel += (s, e) => Observed("Wheel", s, e);
            MouseMove += (s, e) => Observed("Move", s, e);
            MouseEnter += (s, e) => Observed("Enter", s, e);
            MouseLeave += (s, e) => Observed("Leave", s, e);
            MouseHover += (s, e) => Observed("Hover", s, e);
            MouseCaptureChanged += (s, e) => Observed("CaptureChanged", s, e);
            Click += (_, e) => Emit("event-ControlClick:" + Args(e));
            DoubleClick += (_, e) => Emit("event-ControlDoubleClick:" + Args(e));
        }
        private void Observed(string name, object? sender, object? args)
        {
            Emit($"event:{Name}:{name}:{Args(args)}:sender={ReferenceEquals(sender, this)}:sameArgs={ReferenceEquals(args, _expectedArgs)}:capture={Capture}");
            if (FailingEvent == name) throw Failure;
        }
        internal bool ReadStyle(ControlStyles style) => GetStyle(style);
        internal void WriteStyle(ControlStyles style, bool value) => SetStyle(style, value);
        internal void CallWindow(ref Message message) => WndProc(ref message);
        internal void CallClick(Control? target, EventArgs e) => InvokeOnClick(target, e);
        internal void CallMouseEvent(object key, MouseEventArgs e) => RaiseMouseEvent(key, e);
        internal void AddCustomEvent(object key, Delegate handler) => Events.AddHandler(key, handler);
        internal void ResetMouse() => ResetMouseEventArgs();
        internal void Rebuild() => RecreateHandle();
        internal void DirectDestroy() => DestroyHandle();
        internal void Raise(string name, object? e)
        {
            switch (name)
            {
                case "Down": OnMouseDown((MouseEventArgs)e!); break;
                case "Up": OnMouseUp((MouseEventArgs)e!); break;
                case "Click": OnMouseClick((MouseEventArgs)e!); break;
                case "DoubleClick": OnMouseDoubleClick((MouseEventArgs)e!); break;
                case "Wheel": OnMouseWheel((MouseEventArgs)e!); break;
                case "Move": OnMouseMove((MouseEventArgs)e!); break;
                case "Enter": OnMouseEnter((EventArgs)e!); break;
                case "Leave": OnMouseLeave((EventArgs)e!); break;
                case "Hover": OnMouseHover((EventArgs)e!); break;
                case "CaptureChanged": OnMouseCaptureChanged((EventArgs)e!); break;
            }
        }
        private void Before(string name, object? e) => Emit("on-enter:" + Name + ":" + name + ":" + Args(e));
        private void After(string name) => Emit("on-exit:" + Name + ":" + name);
        protected override void OnMouseDown(MouseEventArgs e) { Before("Down", e); if (!SkipBase) base.OnMouseDown(e); After("Down"); }
        protected override void OnMouseUp(MouseEventArgs e) { Before("Up", e); if (!SkipBase) base.OnMouseUp(e); After("Up"); }
        protected override void OnMouseClick(MouseEventArgs e) { Before("Click", e); if (!SkipBase) base.OnMouseClick(e); After("Click"); }
        protected override void OnMouseDoubleClick(MouseEventArgs e) { Before("DoubleClick", e); if (!SkipBase) base.OnMouseDoubleClick(e); After("DoubleClick"); }
        protected override void OnMouseWheel(MouseEventArgs e) { Before("Wheel", e); if (!SkipBase) base.OnMouseWheel(e); After("Wheel"); }
        protected override void OnMouseMove(MouseEventArgs e) { Before("Move", e); if (!SkipBase) base.OnMouseMove(e); After("Move"); }
        protected override void OnMouseEnter(EventArgs e) { Before("Enter", e); if (!SkipBase) base.OnMouseEnter(e); After("Enter"); }
        protected override void OnMouseLeave(EventArgs e) { Before("Leave", e); if (!SkipBase) base.OnMouseLeave(e); After("Leave"); }
        protected override void OnMouseHover(EventArgs e) { Before("Hover", e); if (!SkipBase) base.OnMouseHover(e); After("Hover"); }
        protected override void OnMouseCaptureChanged(EventArgs e) { Before("CaptureChanged", e); if (!SkipBase) base.OnMouseCaptureChanged(e); After("CaptureChanged"); }
        protected override void OnHandleCreated(EventArgs e) { if (RecordLifecycle) Emit("handle-created-enter"); base.OnHandleCreated(e); if (RecordLifecycle) Emit("handle-created-exit"); }
        protected override void OnHandleDestroyed(EventArgs e) { if (RecordLifecycle) Emit("handle-destroyed-enter"); base.OnHandleDestroyed(e); if (RecordLifecycle) Emit("handle-destroyed-exit"); }
    }

    private interface IProbeStyles { bool Read(ControlStyles style); }
    private sealed class ProbeButton : Button, IProbeStyles { public bool Read(ControlStyles style) => GetStyle(style); }
    private sealed class ProbePictureBox : PictureBox, IProbeStyles { public bool Read(ControlStyles style) => GetStyle(style); }
    private sealed class ProbeCheckBox : CheckBox, IProbeStyles { public bool Read(ControlStyles style) => GetStyle(style); }
    private sealed class ProbeRadioButton : RadioButton, IProbeStyles { public bool Read(ControlStyles style) => GetStyle(style); }
    private sealed class CursorPositionScope : IDisposable
    {
        private readonly Point _saved;
        internal CursorPositionScope() { if (!GetCursorPos(out _saved)) throw new InvalidOperationException("Unable to save the native cursor position."); }
        public void Dispose() { if (!SetCursorPos(_saved.X, _saved.Y)) throw new InvalidOperationException("Unable to restore the native cursor position."); }
    }

    private static Rectangle MapRectangle(nint from, nint to, Rectangle value)
    {
        NativeRectangle native = new() { Left = value.Left, Top = value.Top, Right = value.Right, Bottom = value.Bottom };
        _ = MapWindowPoints(from, to, ref native, 2);
        return Rectangle.FromLTRB(native.Left, native.Top, native.Right, native.Bottom);
    }
    private static void Pump()
    {
        long deadline = Environment.TickCount64 + 2000;
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (message.Message == 0x0012) return;
            if (++count > 4096 || Environment.TickCount64 > deadline) throw new TimeoutException("Control027 message pump did not become idle.");
            _ = TranslateMessage(in message);
            _ = DispatchMessageW(in message);
        }
    }
    [StructLayout(LayoutKind.Sequential)] private struct NativeRectangle { public int Left, Top, Right, Bottom; }
    [StructLayout(LayoutKind.Sequential)] private struct NativeMessage { public nint Window; public uint Message; public nuint WParam; public nint LParam; public uint Time; public Point Point; public uint Private; }
    [DllImport("user32.dll")] private static extern nint GetCapture();
    [DllImport("user32.dll")] private static extern nint WindowFromPoint(Point point);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool GetWindowRect(nint window, out NativeRectangle rectangle);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindowVisible(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindowEnabled(nint window);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetClassNameW(nint window, StringBuilder text, int length);
    [DllImport("user32.dll")] private static extern int GetDlgCtrlID(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool SetWindowPos(nint window, nint after, int x, int y, int width, int height, uint flags);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool GetCursorPos(out Point point);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool SetCursorPos(int x, int y);
    [DllImport("user32.dll")] private static extern nint SetCapture(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool ReleaseCapture();
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool DestroyWindow(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool ClientToScreen(nint window, ref Point point);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool ScreenToClient(nint window, ref Point point);
    [DllImport("user32.dll")] private static extern int MapWindowPoints(nint from, nint to, ref NativeRectangle points, uint count);
    [DllImport("user32.dll")] private static extern nint SendMessageW(nint window, int message, nint wParam, nint lParam);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")] private static extern nint DispatchMessageW(in NativeMessage message);
}
