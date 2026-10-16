using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

// One source, three executions: fixed WinForms 10.0.11, Lumina JIT and real AOT.
// Every scenario runs in an isolated STA process. Screen and print pixels are
// intentionally separate observations; raw exceptions and failures are retained.
internal static class Program
{
    private static readonly List<string> Events = [];
    private static readonly Exception Failure = new InvalidOperationException("background probe callback failure");
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
        if (args.Length != 1) throw new ArgumentException("Specify exactly one background/close scenario.");
        Record("scenario", args[0]);
        switch (args[0])
        {
            case "metadata": Metadata(); break;
            case "properties": Properties(); break;
            case "image-events": ImageEvents(); break;
            case "layout-events": LayoutEvents(); break;
            case "borrowed-images": BorrowedImages(); break;
            case "reentrancy": Reentrancy(); break;
            case "exceptions": Exceptions(); break;
            case "direct-layouts": DirectLayouts(); break;
            case "direct-state": DirectState(); break;
            case "screen-layouts": ScreenLayouts(); break;
            case "screen-aspect": ScreenAspect(); break;
            case "screen-transparent": ScreenTransparent(false); break;
            case "screen-alpha": ScreenTransparent(true); break;
            case "parent-graphics": ParentGraphics(); break;
            case "screen-invalidate": ScreenInvalidate(); break;
            case "close-dispose": CloseQueue("dispose"); break;
            case "close-destroy": CloseQueue("destroy"); break;
            case "close-native": CloseQueue("native"); break;
            case "drain-disposed-wait": DrainQueue(false); break;
            case "callback-failure-disposed-wait": DrainQueue(true); break;
            case "completed-disposed-wait": CompletedWait(); break;
            default: throw new ArgumentException("Unknown background/close scenario: " + args[0]);
        }
        Record("complete", true);
    }

    private static void Metadata()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        foreach (string name in new[] { "BackgroundImage", "BackgroundImageLayout" })
        {
            PropertyInfo? property = typeof(Control).GetProperty(name, flags);
            Record("property-" + name, "type:" + property?.PropertyType.Name + "|get:" + Shape(property?.GetMethod) + "|set:" + Shape(property?.SetMethod));
            Record("event-" + name + "Changed", typeof(Control).GetEvent(name + "Changed", flags)?.EventHandlerType?.Name);
            Record("method-On" + name + "Changed", Shape(typeof(Control).GetMethod("On" + name + "Changed", flags, [typeof(EventArgs)])));
        }
        Record("method-OnParentBackgroundImageChanged", Shape(typeof(Control).GetMethod("OnParentBackgroundImageChanged", flags, [typeof(EventArgs)])));
        foreach (ImageLayout layout in Enum.GetValues<ImageLayout>()) Record("layout-value-" + layout, (int)layout);
    }

    private static string Shape(MethodInfo? method) => method is null ? "missing" :
        $"access:{(method.IsPublic ? "public" : method.IsFamily ? "protected" : method.IsFamilyOrAssembly ? "protected-internal" : "other")},virtual:{method.IsVirtual},return:{method.ReturnType.Name}";

    private static void Properties()
    {
        using var c = New("control");
        using var image = Pattern();
        Record("defaults", State(c));
        Step("assign-image", () => c.BackgroundImage = image, c);
        Record("same-instance", ReferenceEquals(c.BackgroundImage, image));
        Step("same-image", () => c.BackgroundImage = image, c);
        Step("clear-image", () => c.BackgroundImage = null, c);
        foreach (ImageLayout layout in Enum.GetValues<ImageLayout>()) Step("layout-" + layout, () => c.BackgroundImageLayout = layout, c);
        foreach (int value in new[] { -1, 5, int.MinValue, int.MaxValue })
            Step("invalid-layout-" + value, () => c.BackgroundImageLayout = (ImageLayout)value, c);
        using var alphaSource = Pattern(true);
        using var pngStream = new MemoryStream();
        alphaSource.Save(pngStream, ImageFormat.Png);
        pngStream.Position = 0;
        using var alpha = new Bitmap(pngStream);
        Record("alpha-png-has-alpha", (alpha.Flags & (int)ImageFlags.HasAlpha) != 0);
        c.BackgroundImage = alpha;
        foreach (ImageLayout layout in Enum.GetValues<ImageLayout>()) Step("alpha-layout-" + layout, () => c.BackgroundImageLayout = layout, c);
        Record("image-after-properties", ImageStatus(image));
        Record("alpha-after-properties", ImageStatus(alpha));
    }

    private static void ImageEvents()
    {
        using var parent = New("parent");
        using var inherited = New("inherited");
        using var local = New("local");
        using var grandchild = New("grandchild");
        using var first = Pattern();
        using var second = Pattern(true);
        local.BackgroundImage = second;
        inherited.Controls.Add(grandchild);
        parent.Controls.Add(inherited); parent.Controls.Add(local);
        Attach(parent); Attach(inherited); Attach(local); Attach(grandchild);
        Step("parent-image", () => parent.BackgroundImage = first, parent, inherited, local, grandchild);
        Step("parent-same-image", () => parent.BackgroundImage = first, parent, inherited, local, grandchild);
        Step("parent-clear-image", () => parent.BackgroundImage = null, parent, inherited, local, grandchild);
        Step("direct-parent-notification", () => local.RaiseParentImage(EventArgs.Empty), local);
        Step("null-image-event", () => parent.RaiseImage(null!), parent);
    }

    private static void LayoutEvents()
    {
        using var c = New("control");
        using var child = New("child");
        c.Controls.Add(child); Attach(c); Attach(child);
        foreach (ImageLayout layout in Enum.GetValues<ImageLayout>())
        {
            Step("layout-" + layout, () => c.BackgroundImageLayout = layout, c, child);
            Step("same-layout-" + layout, () => c.BackgroundImageLayout = layout, c, child);
        }
        Step("null-layout-event", () => c.RaiseLayout(null!), c);
    }

    private static void BorrowedImages()
    {
        using var first = Pattern();
        using var second = Pattern(true);
        var c = New("control");
        c.BackgroundImage = first;
        c.BackgroundImage = second;
        Record("first-after-replace", ImageStatus(first));
        c.Dispose();
        Record("second-after-control-dispose", ImageStatus(second));
        Record("image-identity-after-dispose", ReferenceEquals(c.BackgroundImage, second));
        using var disposed = Pattern();
        disposed.Dispose();
        using var invalid = New("invalid");
        Step("assign-disposed-image", () => invalid.BackgroundImage = disposed, invalid);
        foreach (ImageLayout layout in new[] { ImageLayout.Tile, ImageLayout.Center, ImageLayout.Stretch })
        {
            Step("disposed-layout-" + layout, () => invalid.BackgroundImageLayout = layout, invalid);
            using var bitmap = new Bitmap(64, 48);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using var e = new PaintEventArgs(graphics, invalid.ClientRectangle);
            Step("paint-disposed-" + layout, () => invalid.RaiseBackground(e), invalid);
            Record("graphics-after-disposed-image-" + layout, GraphicsStatus(graphics));
        }
    }

    private static void Reentrancy()
    {
        using var c = New("control");
        using var first = Pattern();
        using var second = Pattern(true);
        Attach(c);
        bool nested = false;
        c.BackgroundImageChanged += (_, _) =>
        {
            Trace("image-handler:" + (ReferenceEquals(c.BackgroundImage, first) ? "first" : "other"));
            if (nested) return;
            nested = true; c.BackgroundImage = second;
        };
        Step("image-reentry", () => c.BackgroundImage = first, c);
        Record("reentry-final-image", ReferenceEquals(c.BackgroundImage, second));
        nested = false;
        c.BackgroundImageLayoutChanged += (_, _) =>
        {
            Trace("layout-handler:" + c.BackgroundImageLayout);
            if (nested) return;
            nested = true; c.BackgroundImageLayout = ImageLayout.Zoom;
        };
        Step("layout-reentry", () => c.BackgroundImageLayout = ImageLayout.Center, c);
    }

    private static void Exceptions()
    {
        using var image = Pattern();
        using var c = New("image-failure");
        c.BackgroundImageChanged += (_, _) => throw Failure;
        Step("image-original", () => c.BackgroundImage = image, c);
        Record("image-assigned-before-failure", ReferenceEquals(c.BackgroundImage, image));
        using var layout = New("layout-failure");
        layout.BackgroundImageLayoutChanged += (_, _) => throw Failure;
        Step("layout-original", () => layout.BackgroundImageLayout = ImageLayout.Center, layout);
        using var parent = New("parent");
        using var child = New("child");
        parent.Controls.Add(child);
        child.BackgroundImageChanged += (_, _) => throw Failure;
        Step("child-original", () => parent.BackgroundImage = image, parent, child);
        Record("image-after-callback-failure", ImageStatus(image));
    }

    private static void DirectLayouts()
    {
        using var c = New("control");
        using var image = Pattern();
        c.BackColor = Color.FromArgb(255, 12, 34, 56);
        c.BackgroundImage = image;
        foreach (ImageLayout layout in Enum.GetValues<ImageLayout>())
        {
            c.BackgroundImageLayout = layout;
            using var bitmap = new Bitmap(64, 48);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Fuchsia);
            graphics.SetClip(new Rectangle(3, 4, 57, 40));
            using var e = new PaintEventArgs(graphics, new Rectangle(3, 4, 57, 40));
            Step("direct-" + layout, () => c.RaiseBackground(e), c);
            Record("bitmap-" + layout, Pixels(bitmap));
            Record("borrowed-graphics-" + layout, GraphicsStatus(graphics));
        }
        Record("borrowed-image", ImageStatus(image));
    }

    private static void DirectState()
    {
        using var c = New("control");
        using var image = Pattern(true);
        c.BackColor = Color.FromArgb(255, 12, 34, 56);
        c.BackgroundImage = image;
        foreach (ImageLayout layout in Enum.GetValues<ImageLayout>())
        {
            c.BackgroundImageLayout = layout;
            using var bitmap = new Bitmap(80, 64);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Fuchsia);
            graphics.TranslateTransform(2, 3);
            graphics.SetClip(new Rectangle(3, 4, 50, 30));
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            string before = GraphicsState(graphics);
            using var e = new PaintEventArgs(graphics, new Rectangle(3, 4, 50, 30));
            Step("alpha-direct-" + layout, () => c.RaiseBackground(e), c);
            Record("alpha-bitmap-" + layout, Pixels(bitmap));
            Record("state-before-" + layout, before);
            Record("state-after-" + layout, GraphicsState(graphics));
        }
    }

    private static void ScreenLayouts()
    {
        using var host = Host();
        using var c = New("control");
        using var image = Pattern();
        c.Location = new Point(8, 8); c.BackColor = Color.FromArgb(255, 12, 34, 56); c.BackgroundImage = image;
        host.Controls.Add(c);
        Show(host, c);
        foreach (ImageLayout layout in Enum.GetValues<ImageLayout>())
        {
            c.BackgroundImageLayout = layout; Settle(host);
            Record("screen-" + layout, ScreenPixels(c));
            Record("printclient-" + layout, PrintPixels(c));
        }
        CloseHost(host, c);
        Record("image-after-window-close", ImageStatus(image));
    }

    private static void ScreenTransparent(bool alpha)
    {
        using var host = Host();
        using var parent = New("parent");
        using var child = New("child");
        using var image = Pattern();
        parent.Location = new Point(8, 8); parent.BackColor = Color.FromArgb(255, 20, 40, 60); parent.BackgroundImage = image;
        child.Bounds = new Rectangle(9, 7, 24, 20); child.TransparentStyle();
        child.BackColor = alpha ? Color.FromArgb(128, 200, 100, 50) : Color.Transparent;
        parent.Controls.Add(child); host.Controls.Add(parent);
        Show(host, parent, child);
        foreach (ImageLayout layout in Enum.GetValues<ImageLayout>())
        {
            parent.BackgroundImageLayout = layout; Settle(host);
            Record("parent-layout-" + layout + "-screen", ScreenPixels(child));
            Record("parent-layout-" + layout + "-printclient", PrintPixels(child));
        }
        parent.BackgroundImageLayout = ImageLayout.Tile;
        Attach(child);
        child.Move += (_, _) => Trace("event-move:" + child.Name);
        Step("move-transparent-child", () => child.Location = new Point(17, 11), child);
        Settle(host);
        Record("moved-screen", ScreenPixels(child));
        Record("moved-printclient", PrintPixels(child));
        child.Bounds = new Rectangle(53, 37, 24, 20); Settle(host);
        Record("clipped-screen-visible-points", ScreenPixels(child, [new Point(1, 1), new Point(5, 4), new Point(9, 8)]));
        Record("clipped-printclient", PrintPixels(child));
        CloseHost(host, parent, child);
        Record("parent-image-after-window-close", ImageStatus(image));
    }

    private static void ScreenAspect()
    {
        using var host = Host();
        using var c = New("control");
        using var image = Pattern();
        c.Location = new Point(8, 8); c.BackColor = Color.FromArgb(255, 12, 34, 56); c.BackgroundImage = image;
        host.Controls.Add(c); Show(host, c);
        foreach ((string name, Size size) in new[] { ("wide", new Size(64, 32)), ("tall", new Size(32, 64)) })
        {
            c.Size = size;
            Record("aspect-" + name + "-client", Rect(c.ClientRectangle));
            foreach (ImageLayout layout in new[] { ImageLayout.Stretch, ImageLayout.Zoom })
            {
                c.BackgroundImageLayout = layout; Settle(host);
                Record("aspect-" + name + "-" + layout + "-screen", ScreenPixels(c));
                Record("aspect-" + name + "-" + layout + "-printclient", PrintPixels(c));
            }
        }
        CloseHost(host, c);
        Record("image-after-aspect-close", ImageStatus(image));
    }

    private static void ParentGraphics()
    {
        using var host = Host();
        using var parent = New("parent");
        using var child = New("child");
        using var image = Pattern();
        parent.Location = new Point(8, 8); parent.BackgroundImage = image; parent.BackColor = Color.FromArgb(255, 20, 40, 60);
        child.Bounds = new Rectangle(9, 7, 24, 20); child.TransparentStyle(); child.BackColor = Color.Transparent;
        parent.Controls.Add(child); host.Controls.Add(parent); Show(host, parent, child);
        parent.ObserveGraphics = true;
        using var bitmap = new Bitmap(40, 36);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.Fuchsia); graphics.TranslateTransform(2, 3); graphics.SetClip(new Rectangle(1, 2, 18, 15));
        using var e = new PaintEventArgs(graphics, new Rectangle(1, 2, 18, 15));
        Record("caller-state-before", GraphicsState(graphics));
        Step("visible-child-direct-background", () => child.RaiseBackground(e), child);
        Record("caller-state-after", GraphicsState(graphics));
        Record("caller-graphics-after", GraphicsStatus(graphics));
        parent.MutateGraphics = true;
        parent.ThrowBackground = true;
        Step("visible-parent-original-failure", () => child.RaiseBackground(e), child);
        Record("caller-state-after-failure", GraphicsState(graphics));
        Record("caller-graphics-after-failure", GraphicsStatus(graphics));
        parent.ThrowBackground = false;
        parent.MutateGraphics = false;
        CloseHost(host, parent, child);
    }

    private static void ScreenInvalidate()
    {
        using var host = Host();
        using var parent = New("parent");
        using var child = New("child");
        using var image = Pattern();
        parent.Location = new Point(8, 8); parent.BackgroundImage = image; parent.BackColor = Color.FromArgb(255, 20, 40, 60);
        child.Bounds = new Rectangle(9, 7, 24, 20); child.TransparentStyle(); child.BackColor = Color.Transparent;
        parent.Controls.Add(child); host.Controls.Add(parent); Attach(parent); Attach(child); Show(host, parent, child);
        ValidateRect(parent.Handle, 0); ValidateRect(child.Handle, 0);
        Step("parent-invalidate", () => parent.Invalidate(new Rectangle(12, 10, 8, 6)), parent, child);
        Record("parent-invalid-region", UpdateRect(parent.Handle));
        Record("child-invalid-region", UpdateRect(child.Handle));
        parent.Update(); child.Update(); Pump(); DwmFlush();
        Record("after-invalid-screen", ScreenPixels(child));
        image.SetPixel(2, 2, Color.White);
        Step("image-mutated-invalidate", parent.Invalidate, parent, child);
        parent.Update(); child.Update(); Pump(); DwmFlush();
        Record("mutated-screen", ScreenPixels(child));
        child.BackgroundImage = Pattern(true);
        Image? childImage = child.BackgroundImage;
        try
        {
            Step("parent-clear-with-child-image", () => parent.BackgroundImage = null, parent, child);
            Settle(host); Record("child-image-screen", ScreenPixels(child));
        }
        finally { child.BackgroundImage = null; childImage?.Dispose(); }
        CloseHost(host, parent, child);
    }

    private static void CloseQueue(string kind)
    {
        var c = New("queue");
        nint hwnd = c.Handle;
        int called = 0;
        Application.ThreadException += (_, e) => Trace("thread-exception:" + ExceptionText(e.Exception));
        IAsyncResult first = c.BeginInvoke((Func<int>)(() => { called++; Trace("callback:first"); return 11; }));
        IAsyncResult second = c.BeginInvoke((Func<int>)(() => { called++; Trace("callback:second"); return 22; }));
        IAsyncResult third = c.BeginInvoke((Func<int>)(() => { called++; Trace("callback:third"); return 33; }));
        WaitHandle borrowed = first.AsyncWaitHandle;
        borrowed.Dispose();
        Record("before-close", QueueState(c, hwnd, called, first, second, third));
        Step("close-" + kind, () =>
        {
            if (kind == "dispose") c.Dispose();
            else if (kind == "destroy") c.Destroy();
            else Record("destroywindow-result", DestroyWindow(hwnd));
        }, c);
        Record("after-close", QueueState(c, hwnd, called, first, second, third));
        EndCompleted(c, "first", first); EndCompleted(c, "second", second); EndCompleted(c, "third", third);
        Record("after-endinvoke", QueueState(c, hwnd, called, first, second, third));
        Step("first-wait-handle-after-close", () => Record("first-wait-signaled", first.AsyncWaitHandle.WaitOne(0)));
        Cleanup(c, hwnd);
    }

    private static void DrainQueue(bool throwCallback)
    {
        var c = New("queue");
        nint hwnd = c.Handle;
        int called = 0;
        Application.ThreadException += (_, e) => Trace("thread-exception:" + ExceptionText(e.Exception));
        IAsyncResult first = c.BeginInvoke((Func<int>)(() => { called++; Trace("callback:first"); if (throwCallback) throw Failure; return 11; }));
        IAsyncResult second = c.BeginInvoke((Func<int>)(() => { called++; Trace("callback:second"); return 22; }));
        first.AsyncWaitHandle.Dispose();
        Record("before-drain", QueueState(c, hwnd, called, first, second));
        Step("first-endinvoke-drain", () => Record("first-result", c.EndInvoke(first)));
        Record("after-first-endinvoke", QueueState(c, hwnd, called, first, second));
        EndCompleted(c, "first-repeat", first);
        Step("second-endinvoke-drain", () => Record("second-result", c.EndInvoke(second)));
        Record("after-second-endinvoke", QueueState(c, hwnd, called, first, second));
        Cleanup(c, hwnd);
    }

    private static void CompletedWait()
    {
        var c = New("queue");
        nint hwnd = c.Handle;
        IAsyncResult result = c.BeginInvoke((Func<int>)(() => 41));
        WaitHandle borrowed = result.AsyncWaitHandle;
        Step("first-endinvoke", () => Record("first-result", c.EndInvoke(result)));
        Record("completed-before-dispose", result.IsCompleted);
        Record("signaled-before-dispose", borrowed.WaitOne(0));
        borrowed.Dispose();
        Step("repeat-endinvoke", () => Record("repeat-result", c.EndInvoke(result)));
        Record("completed-after-dispose", result.IsCompleted);
        Cleanup(c, hwnd);
    }

    private static void EndCompleted(Control c, string name, IAsyncResult result)
    {
        // Never turn a reference queue deadlock into an unbounded probe. Pending
        // items are recorded as pending, not promoted to completed or successful.
        if (!result.IsCompleted) { Record("end-" + name, "not-invoked-pending"); return; }
        Step("end-" + name, () => Record("result-" + name, c.EndInvoke(result)));
    }

    private static string QueueState(Control c, nint hwnd, int called, params IAsyncResult[] results) =>
        $"called:{called}|completed:[{string.Join(",", results.Select(r => r.IsCompleted))}]|managedHandle:{c.IsHandleCreated}|nativeWindow:{IsWindow(hwnd)}|disposed:{c.IsDisposed}";

    private static void Cleanup(Control c, nint hwnd)
    {
        Step("cleanup-dispose", c.Dispose);
        if (IsWindow(hwnd)) Step("cleanup-native", () => Record("cleanup-destroywindow", DestroyWindow(hwnd)));
        Record("final-native-window", IsWindow(hwnd));
        Record("final-managed-handle", c.IsHandleCreated);
    }

    private static Bitmap Pattern(bool alpha = false)
    {
        var image = new Bitmap(8, 6, PixelFormat.Format32bppArgb);
        for (int y = 0; y < image.Height; y++)
            for (int x = 0; x < image.Width; x++)
            {
                Color color = y < 3 ? (x < 4 ? Color.Red : Color.Lime) : (x < 4 ? Color.Blue : Color.Yellow);
                image.SetPixel(x, y, Color.FromArgb(alpha ? 128 : 255, color));
            }
        return image;
    }

    private static ProbeControl New(string name) => new() { Name = name, Bounds = new Rectangle(0, 0, 64, 48) };
    private static Form Host() => new() { Text = "Control background compatibility probe", Bounds = new Rectangle(96, 88, 220, 180) };
    private static void Show(Form host, params Control[] controls)
    {
        host.Show();
        _ = SetWindowPos(host.Handle, (nint)(-1), 0, 0, 0, 0, 0x0013);
        Settle(host);
        Record("host-visible", IsWindowVisible(host.Handle));
        Record("native-controls", string.Join(";", controls.Select(c => c.Name + ":hwnd:" + IsWindow(c.Handle) + ",visible:" + IsWindowVisible(c.Handle) + ",client:" + Rect(c.ClientRectangle))));
    }
    private static void Settle(Form host)
    {
        _ = RedrawWindow(host.Handle, 0, 0, 0x0185); // invalidate + erase + all children + update now
        Pump(); DwmFlush();
    }
    private static void CloseHost(Form host, params Control[] controls)
    {
        nint[] handles = [host.Handle, .. controls.Select(c => c.Handle)];
        host.Close();
        Record("closed-managed", !host.IsHandleCreated && controls.All(c => !c.IsHandleCreated));
        Record("closed-native", handles.All(h => !IsWindow(h)));
    }

    private static Point[] SamplePoints(int width, int height) =>
        [new(1, 1), new(4, 5), new(width / 2, height / 2), new(Math.Max(1, width - 5), Math.Max(1, height - 6)), new(width - 2, height - 2)];
    private static string Pixels(Bitmap bitmap) => string.Join(";", SamplePoints(bitmap.Width, bitmap.Height).Select(p => $"{p.X},{p.Y}:" + bitmap.GetPixel(p.X, p.Y).ToArgb().ToString("X8", CultureInfo.InvariantCulture)));
    private static string ScreenPixels(Control c, Point[]? points = null)
    {
        nint dc = GetDC(0);
        if (dc == 0) throw new InvalidOperationException("GetDC(screen) failed.");
        try
        {
            return string.Join(";", (points ?? SamplePoints(c.ClientSize.Width, c.ClientSize.Height)).Select(p =>
            {
                Point screen = p;
                if (!ClientToScreen(c.Handle, ref screen)) throw new InvalidOperationException("ClientToScreen failed.");
                uint pixel = GetPixel(dc, screen.X, screen.Y);
                if (pixel == uint.MaxValue) throw new InvalidOperationException("GetPixel(screen) failed.");
                uint argb = 0xFF000000u | ((pixel & 0xFF) << 16) | (pixel & 0xFF00) | ((pixel >> 16) & 0xFF);
                return $"{p.X},{p.Y}:" + argb.ToString("X8", CultureInfo.InvariantCulture);
            }));
        }
        finally { _ = ReleaseDC(0, dc); }
    }
    private static string PrintPixels(Control c)
    {
        using var bitmap = new Bitmap(c.ClientSize.Width, c.ClientSize.Height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.Fuchsia);
        nint dc = graphics.GetHdc();
        nint result;
        try { result = SendMessageW(c.Handle, 0x0318, dc, (nint)0x000C); }
        finally { graphics.ReleaseHdc(dc); }
        return "result:" + result + "|pixels:" + Pixels(bitmap);
    }

    private static void Attach(ProbeControl c)
    {
        c.BackgroundImageChanged += (_, e) => Trace("event-image:" + c.Name + ":null:" + (e is null));
        c.BackgroundImageLayoutChanged += (_, e) => Trace("event-layout:" + c.Name + ":null:" + (e is null));
        c.Invalidated += (_, e) => Trace("event-invalid:" + c.Name + ":" + Rect(e.InvalidRect));
    }
    private static string State(ProbeControl c) =>
        $"{c.Name}:image:{(c.BackgroundImage is null ? "null" : "set")},layout:{c.BackgroundImageLayout},resize:{c.ReadStyle(ControlStyles.ResizeRedraw)},buffered:{c.Buffered},handle:{c.IsHandleCreated},disposed:{c.IsDisposed}";
    private static void Step(string name, Action action, params ProbeControl[] controls)
    {
        Events.Clear(); _recording = true;
        Exception? error = null;
        try { action(); }
        catch (Exception e) { error = e; }
        finally { _recording = false; }
        Record(name, "exception:" + ExceptionText(error) + "|events:[" + string.Join(";", Events) + "]|state:[" + string.Join(";", controls.Select(State)) + "]");
    }
    private static string ExceptionText(Exception? e) => e is null ? "none" : ReferenceEquals(e, Failure) ? "original" :
        e.GetType().Name + (e is ArgumentException a ? ":" + a.ParamName : "") + (e.InnerException is null ? "" : "(inner:" + ExceptionText(e.InnerException) + ")");
    private static string ImageStatus(Image image)
    {
        try { return "alive:" + image.Width + "," + image.Height; }
        catch (Exception e) { return ExceptionText(e); }
    }
    private static string GraphicsStatus(Graphics graphics)
    {
        try { _ = graphics.ClipBounds; return "alive"; }
        catch (Exception e) { return ExceptionText(e); }
    }
    private static string GraphicsState(Graphics graphics)
    {
        using Matrix transform = graphics.Transform;
        RectangleF clip = graphics.ClipBounds;
        return $"transform:[{string.Join(",", transform.Elements)}]|clip:{clip.X},{clip.Y},{clip.Width},{clip.Height}|interpolation:{graphics.InterpolationMode}|offset:{graphics.PixelOffsetMode}";
    }
    private static string Rect(Rectangle r) => $"{r.X},{r.Y},{r.Width},{r.Height}";
    private static string UpdateRect(nint hwnd) => GetUpdateRect(hwnd, out NativeRect r, false) ? $"{r.Left},{r.Top},{r.Right - r.Left},{r.Bottom - r.Top}" : "empty";
    private static void Trace(string value) { if (_recording) Events.Add(value); }
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + (value?.ToString() ?? "null"));

    private sealed class ProbeControl : Control
    {
        internal bool ObserveGraphics, ThrowBackground, MutateGraphics;
        internal bool Buffered => DoubleBuffered;
        internal bool ReadStyle(ControlStyles style) => GetStyle(style);
        internal void TransparentStyle() => SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        internal void RaiseImage(EventArgs e) => OnBackgroundImageChanged(e);
        internal void RaiseLayout(EventArgs e) => OnBackgroundImageLayoutChanged(e);
        internal void RaiseParentImage(EventArgs e) => OnParentBackgroundImageChanged(e);
        internal void RaiseBackground(PaintEventArgs e) => OnPaintBackground(e);
        internal void Destroy() => DestroyHandle();
        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            Trace("on-image-enter:" + Name); base.OnBackgroundImageChanged(e); Trace("on-image-exit:" + Name);
        }
        protected override void OnBackgroundImageLayoutChanged(EventArgs e)
        {
            Trace("on-layout-enter:" + Name); base.OnBackgroundImageLayoutChanged(e); Trace("on-layout-exit:" + Name);
        }
        protected override void OnParentBackgroundImageChanged(EventArgs e)
        {
            Trace("on-parent-image-enter:" + Name); base.OnParentBackgroundImageChanged(e); Trace("on-parent-image-exit:" + Name);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (ObserveGraphics) Trace("parent-background:" + Rect(e.ClipRectangle) + ":" + GraphicsState(e.Graphics));
            if (MutateGraphics) { e.Graphics.TranslateTransform(11, 13); e.Graphics.SetClip(new Rectangle(2, 3, 4, 5)); }
            if (ThrowBackground) throw Failure;
            base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (ObserveGraphics) Trace("parent-paint:" + Rect(e.ClipRectangle) + ":" + GraphicsState(e.Graphics));
            base.OnPaint(e);
        }
    }

    private static void Pump()
    {
        long deadline = Environment.TickCount64 + 2000;
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (message.Message == 0x0012) { PostQuitMessage(unchecked((int)message.WParam)); return; }
            if (++count > 4096 || Environment.TickCount64 > deadline) throw new TimeoutException("Background message pump did not become idle.");
            _ = TranslateMessage(in message); _ = DispatchMessageW(in message);
        }
    }

    [StructLayout(LayoutKind.Sequential)] private struct NativeRect { public int Left, Top, Right, Bottom; }
    [StructLayout(LayoutKind.Sequential)] private struct NativeMessage { public nint HWnd; public uint Message; public nuint WParam; public nint LParam; public uint Time; public Point Point; public uint Private; }
    [DllImport("user32.dll")] private static extern nint SendMessageW(nint window, int message, nint wParam, nint lParam);
    [DllImport("user32.dll")] private static extern nint GetDC(nint window);
    [DllImport("user32.dll")] private static extern int ReleaseDC(nint window, nint dc);
    [DllImport("gdi32.dll")] private static extern uint GetPixel(nint dc, int x, int y);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool ClientToScreen(nint window, ref Point point);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindowVisible(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool DestroyWindow(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool SetWindowPos(nint window, nint after, int x, int y, int width, int height, uint flags);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool RedrawWindow(nint window, nint updateRect, nint updateRegion, uint flags);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool GetUpdateRect(nint window, out NativeRect rect, [MarshalAs(UnmanagedType.Bool)] bool erase);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool ValidateRect(nint window, nint rect);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")] private static extern nint DispatchMessageW(in NativeMessage message);
    [DllImport("user32.dll")] private static extern void PostQuitMessage(int exitCode);
    [DllImport("dwmapi.dll")] private static extern int DwmFlush();
}
