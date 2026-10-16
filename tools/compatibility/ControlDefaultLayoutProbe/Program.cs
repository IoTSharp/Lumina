using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
#if REFERENCE
using System.Windows.Forms;
using System.Windows.Forms.Layout;
#else
using Lumina.Forms;
using Lumina.Forms.Layout;
#endif

internal static class Program
{
    private static readonly List<string> Events = [];
    private static readonly Exception OriginalFailure = new InvalidOperationException("default layout callback failure");
    private static bool _recording;

    [STAThread]
    private static void Main(string[] args)
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        if (args.Length != 1) throw new ArgumentException("Specify exactly one default layout scenario.");
        if (args[0] == "anchor-v2")
            AppContext.SetSwitch("System.Windows.Forms.AnchorLayoutV2", true);
        bool switchConfigured = AppContext.TryGetSwitch("System.Windows.Forms.AnchorLayoutV2", out bool anchorLayoutV2);
        Console.Error.WriteLine($"AppContext.System.Windows.Forms.AnchorLayoutV2:configured={switchConfigured};value={anchorLayoutV2}");
        Console.Error.WriteLine("runtime=" + RuntimeInformation.FrameworkDescription);
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
            throw new InvalidOperationException("Expected WinForms 10.0.11, found " + version);
        Console.Error.WriteLine("reference-assembly=" + version);
#endif
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        Record("scenario", args[0]);
        Console.Error.WriteLine("BEGIN " + args[0]);
        switch (args[0])
        {
            case "metadata": Metadata(); break;
            case "engine-custom": EngineCustom(); break;
            case "engine-contracts": EngineContracts(); break;
            case "preferred-basic": PreferredBasic(); break;
            case "preferred-derived": PreferredDerived(); break;
            case "preferred-cache": PreferredCache(); break;
            case "dock-edges": DockEdges(); break;
            case "anchor-all": AnchorAll(); break;
            case "anchor-explicit": AnchorExplicit(); break;
            case "anchor-v2": AnchorAll(); AnchorExplicit(); ResumeInit(); AutoSizeLayout(); break;
            case "dock-anchor-events": DockAnchorEvents(); break;
            case "autosize": AutoSizeLayout(); break;
            case "resume-init": ResumeInit(); break;
            case "container-panel": ContainerPanel(); break;
            case "container-measurement": ContainerMeasurement(); break;
            case "form-strips": FormStrips(); break;
            case "split-ratio": SplitRatio(); break;
            case "native-form": NativeForm(); break;
            default: throw new ArgumentException("Unknown scenario: " + args[0]);
        }
        Console.Error.WriteLine("END " + args[0]);
        Record("complete", true);
    }

    private static void Metadata()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        Record("engine-type", $"base:{typeof(LayoutEngine).BaseType?.Name}|abstract:{typeof(LayoutEngine).IsAbstract}|sealed:{typeof(LayoutEngine).IsSealed}");
        Record("engine-layout", Shape(typeof(LayoutEngine).GetMethod("Layout", flags, [typeof(object), typeof(LayoutEventArgs)])));
        Record("engine-init", Shape(typeof(LayoutEngine).GetMethod("InitLayout", flags, [typeof(object), typeof(BoundsSpecified)])));
        Record("control-engine", Shape(typeof(Control).GetProperty("LayoutEngine")?.GetMethod));
        Record("control-preferred", Shape(typeof(Control).GetProperty("PreferredSize")?.GetMethod));
        Record("control-get-preferred", Shape(typeof(Control).GetMethod("GetPreferredSize", [typeof(Size)])));
        Record("engine-browsable", typeof(Control).GetProperty("LayoutEngine")?.GetCustomAttribute<BrowsableAttribute>()?.Browsable);
        Record("preferred-browsable", typeof(Control).GetProperty("PreferredSize")?.GetCustomAttribute<BrowsableAttribute>()?.Browsable);
        using var first = New("first", new Rectangle(0, 0, 100, 80));
        using var second = New("second", new Rectangle(0, 0, 50, 40));
        using var panel = new Panel();
        Record("engine-instances", $"type:{first.LayoutEngine.GetType().Name}|same-self:{ReferenceEquals(first.LayoutEngine, first.LayoutEngine)}|same-control:{ReferenceEquals(first.LayoutEngine, second.LayoutEngine)}|same-panel:{ReferenceEquals(first.LayoutEngine, panel.LayoutEngine)}");
        Step("engine-direct-layout", () => Record("engine-result", first.LayoutEngine.Layout(first, new LayoutEventArgs(first, "Direct"))), first);
        Step("engine-null-layout", () => first.LayoutEngine.Layout(null!, new LayoutEventArgs(first, "Null")), first);
        Step("engine-null-init", () => first.LayoutEngine.InitLayout(null!, BoundsSpecified.All), first);
        Step("engine-object-layout", () => first.LayoutEngine.Layout(new object(), new LayoutEventArgs(first, "Object")), first);
    }

    private static string Shape(MethodInfo? method) => method is null ? "missing" :
        $"access:{(method.IsPublic ? "public" : method.IsFamily ? "protected" : method.IsFamilyOrAssembly ? "protected-internal" : "other")}|virtual:{method.IsVirtual}|final:{method.IsFinal}|return:{method.ReturnType.Name}|parameters:{string.Join(",", method.GetParameters().Select(p => p.ParameterType.Name + ":" + p.Name))}";

    private static void EngineCustom()
    {
        using var grand = new EngineControl { Name = "grand", Size = new Size(300, 200) };
        using var parent = new EngineControl { Name = "parent", Bounds = new Rectangle(10, 12, 140, 100) };
        using var child = new EngineControl { Name = "child", Bounds = new Rectangle(5, 6, 20, 15) };
        Watch(grand); Watch(parent); Watch(child);
        grand.Controls.Add(parent);
        Step("engine-add", () => parent.Controls.Add(child), grand, parent, child);
        Step("engine-false", () => parent.PerformLayout(child, "False"), grand, parent, child);
        parent.Engine.Result = true;
        Step("engine-true", () => parent.PerformLayout(child, "True"), grand, parent, child);
        parent.Engine.Result = false;
        Step("engine-init-direct", child.DirectInit, grand, parent, child);
        Step("engine-child-bounds", () => child.Bounds = new Rectangle(9, 11, 30, 21), grand, parent, child);
        Step("engine-resume-false", () => parent.ResumeLayout(false), grand, parent, child);
        parent.Engine.ThrowLayout = true;
        Step("engine-original-exception", () => parent.PerformLayout(child, "Throw"), grand, parent, child);
        parent.Engine.ThrowLayout = false;
        Step("engine-after-exception", parent.PerformLayout, grand, parent, child);
        child.Engine.ThrowInit = true;
        Step("engine-init-original-exception", child.DirectInit, grand, parent, child);
        child.Engine.ThrowInit = false;
        Step("engine-init-after-exception", child.DirectInit, grand, parent, child);
    }

    private static void PreferredBasic()
    {
        using var parent = New("parent", new Rectangle(0, 0, 160, 100));
        using var child = New("child", new Rectangle(7, 8, 33, 22));
        parent.Padding = new Padding(3, 5, 7, 11);
        Step("preferred-unparented", () => Measure(child), parent, child);
        Step("preferred-add", () => parent.Controls.Add(child), parent, child);
        Step("preferred-child-dock", () => child.Dock = DockStyle.Fill, parent, child);
        Step("preferred-docked", () => { Measure(child); Measure(parent); }, parent, child);
        Step("preferred-raw-none", () => child.DirectBounds(4, 6, 71, 52, BoundsSpecified.None), parent, child);
        Step("preferred-raw-none-measure", () => Measure(child), parent, child);
        Step("preferred-undock", () => child.Dock = DockStyle.None, parent, child);
        Step("preferred-undock-measure", () => Measure(child), parent, child);
        Step("preferred-constraints", () => { child.MinimumSize = new Size(40, 10); child.MaximumSize = new Size(60, 19); Measure(child); }, parent, child);
        Step("preferred-padding", () => { child.Padding = new Padding(8); Measure(child); }, parent, child);
        Step("preferred-dispose", () => { child.Dispose(); Measure(child); }, parent, child);
    }

    private static void EngineContracts()
    {
        using var control = New("control", new Rectangle(0, 0, 120, 80));
        var plain = new PlainEngine();
        Step("base-layout-control", () => Trace("result:" + plain.Layout(control, new LayoutEventArgs(control, "Control"))), control);
        Step("base-layout-null", () => Trace("result:" + plain.Layout(null!, new LayoutEventArgs(control, "Null"))), control);
        Step("base-layout-object", () => Trace("result:" + plain.Layout(new object(), new LayoutEventArgs(control, "Object"))), control);
        Step("base-layout-null-args", () => Trace("result:" + plain.Layout(control, null!)), control);
        foreach (BoundsSpecified specified in new[] { BoundsSpecified.None, BoundsSpecified.All, (BoundsSpecified)(-1), (BoundsSpecified)64 })
            Step("base-init-control-" + (int)specified, () => plain.InitLayout(control, specified), control);
        Step("base-init-null", () => plain.InitLayout(null!, BoundsSpecified.All), control);
        Step("base-init-object", () => plain.InitLayout(new object(), BoundsSpecified.All), control);
        Step("default-layout-null-args", () => Trace("result:" + control.LayoutEngine.Layout(control, null!)), control);
        Step("default-init-object", () => control.LayoutEngine.InitLayout(new object(), BoundsSpecified.All), control);
        using var parent = new EngineControl { Name = "parent", Size = new Size(200, 140) };
        parent.Controls.Add(control);
        Watch(parent);
        LayoutEventHandler throwing = (_, _) => { Trace("layout-handler-original-exception"); throw OriginalFailure; };
        parent.Layout += throwing;
        Step("event-before-engine-throw", parent.PerformLayout, parent, control);
        parent.Layout -= throwing;
        Step("event-after-engine-throw", parent.PerformLayout, parent, control);
        parent.Engine.Result = true;
        Step("root-engine-return-true", parent.PerformLayout, parent, control);
        Step("default-child-direct-init", control.DirectInit, parent, control);
        Step("default-child-resume-false", () => parent.ResumeLayout(false), parent, control);
        Step("base-layout-disposed", () => { control.Dispose(); Trace("result:" + plain.Layout(control, new LayoutEventArgs(control, "Disposed"))); }, parent, control);
    }

    private static void Measure(Control control)
    {
        foreach (Size proposal in new[] { Size.Empty, new Size(1, 2), new Size(10, 0), new Size(0, 9), new Size(-1, -2), new Size(200, 300), new Size(int.MaxValue, int.MaxValue) })
            Trace($"measure:{Id(control)}:proposed:{SizeText(proposal)}:result:{SizeText(control.GetPreferredSize(proposal))}");
        Trace($"preferred:{Id(control)}:{SizeText(control.PreferredSize)}");
        Trace($"preferred-repeat:{Id(control)}:{SizeText(control.PreferredSize)}");
    }

    private static void PreferredDerived()
    {
        using var child = new PreferredControl { Name = "child", Bounds = new Rectangle(5, 6, 33, 22) };
        Watch(child);
        Step("derived-return", () => Measure(child), child);
        child.UseBasePreferred = true;
        Step("derived-base", () => Measure(child), child);
        Step("derived-minmax", () => { child.MinimumSize = new Size(40, 10); child.MaximumSize = new Size(60, 19); Measure(child); }, child);
        child.UseBasePreferred = false;
        Step("derived-return-minmax", () => Measure(child), child);
        child.ThrowPreferred = true;
        Step("derived-original-exception", () => _ = child.PreferredSize, child);
        child.ThrowPreferred = false;
        Step("derived-after-exception", () => _ = child.PreferredSize, child);
    }

    private static void PreferredCache()
    {
        using var panel = new Panel { Name = "panel", Bounds = new Rectangle(0, 0, 160, 100), Padding = new Padding(3, 5, 7, 11) };
        using var child = new PreferredControl { Name = "child", Bounds = new Rectangle(13, 17, 31, 23), AutoSize = true, Desired = new Size(41, 29) };
        Watch(panel); Watch(child);
        panel.Controls.Add(child);
        Step("cache-initial", () => Measure(panel), panel, child);
        Step("cache-repeat", () => Measure(panel), panel, child);
        Step("cache-desired-without-layout", () => { child.Desired = new Size(57, 43); Measure(panel); }, panel, child);
        Step("cache-perform", () => { panel.PerformLayout(); Measure(panel); }, panel, child);
        Step("cache-child-padding", () => { child.Padding = new Padding(9); Measure(panel); }, panel, child);
        Step("cache-child-margin", () => { child.Margin = new Padding(2, 4, 6, 8); Measure(panel); }, panel, child);
        Step("cache-child-location", () => { child.Location = new Point(21, 25); Measure(panel); }, panel, child);
        Step("cache-hidden", () => { child.Visible = false; Measure(panel); }, panel, child);
        Step("cache-visible", () => { child.Visible = true; Measure(panel); }, panel, child);
        Step("cache-suspended", () => { panel.SuspendLayout(); child.Desired = new Size(62, 47); panel.Padding = new Padding(4); Measure(panel); }, panel, child);
        Step("cache-resume-false", () => { panel.ResumeLayout(false); Measure(panel); }, panel, child);
        Step("cache-parent-constraints", () => { panel.MinimumSize = new Size(80, 70); panel.MaximumSize = new Size(130, 90); Measure(panel); }, panel, child);
        Step("cache-clear", () => { panel.Controls.Remove(child); Measure(panel); }, panel, child);
        Step("cache-disposed", () => { panel.Dispose(); Measure(panel); }, panel, child);
        using var form = new Form { Name = "form", AutoScaleMode = AutoScaleMode.None, ClientSize = new Size(160, 100), Padding = new Padding(3, 5, 7, 11) };
        using var ordinary = New("ordinary", new Rectangle(13, 17, 31, 23));
        form.Controls.Add(ordinary);
        Step("form-preferred", () => Measure(form), form, ordinary);
        Step("form-preferred-child-move", () => { ordinary.Location = new Point(37, 29); Measure(form); }, form, ordinary);
    }

    private static void ContainerPanel()
    {
        using var host = New("host", new Rectangle(0, 0, 240, 160));
        using var panel = new Panel { Name = "panel", Bounds = new Rectangle(7, 9, 180, 110), Padding = new Padding(3, 5, 7, 11) };
        using var top = New("top", new Rectangle(13, 17, 31, 23));
        using var anchored = New("anchored", new Rectangle(29, 31, 37, 27));
        using var fill = New("fill", new Rectangle(5, 6, 20, 15));
        top.Dock = DockStyle.Top;
        anchored.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        fill.Dock = DockStyle.Fill;
        Watch(panel);
        host.Controls.Add(panel);
        panel.Controls.AddRange([top, anchored, fill]);
        Control[] controls = [host, panel, top, anchored, fill];
        Step("panel-initial", panel.PerformLayout, controls);
        Step("panel-resize", () => panel.Size = new Size(211, 139), controls);
        Step("panel-margin", () => top.Margin = new Padding(13, 17, 19, 23), controls);
        Step("panel-padding", () => panel.Padding = new Padding(7, 11, 13, 17), controls);
        Step("panel-hide-docked", () => top.Visible = false, controls);
        Step("panel-resize-hidden", () => panel.Size = new Size(225, 151), controls);
        Step("panel-show-docked", () => top.Visible = true, controls);
        Step("panel-hide-host", () => host.Visible = false, controls);
        Step("panel-resize-hidden-host", () => panel.Size = new Size(231, 157), controls);
        Step("panel-show-host", () => host.Visible = true, controls);
        Step("panel-suspend-edit", () => { panel.SuspendLayout(); panel.Size = new Size(200, 130); top.Height = 29; }, controls);
        Step("panel-resume-false", () => panel.ResumeLayout(false), controls);
        Step("panel-explicit", panel.PerformLayout, controls);
        Step("panel-undock", () => top.Dock = DockStyle.None, controls);
    }

    private static void ContainerMeasurement()
    {
        // These four fixtures correspond to the finite Flow/Table integration tests;
        // they do not stand in for either container's complete layout algorithm.
        foreach (bool autoSize in new[] { false, true })
        {
            using var parent = new Panel { Name = "parent", Size = new Size(300, 180) };
            using var flow = new FlowLayoutPanel
            {
                Name = "flow", Size = new Size(160, 90), Padding = new Padding(2), WrapContents = false,
                AutoSize = autoSize, AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            using var first = new Control { Name = "first", Size = new Size(20, 10), Margin = new Padding(1), Dock = DockStyle.Top };
            using var second = new Control { Name = "second", Size = new Size(30, 15), Margin = new Padding(1) };
            flow.Controls.AddRange([first, second]);
            parent.Controls.Add(flow);
            parent.PerformLayout();
            flow.PerformLayout();
            Record("flow-measure-" + autoSize, $"preferred:{SizeText(flow.PreferredSize)}|same-engine:{ReferenceEquals(parent.LayoutEngine, flow.LayoutEngine)}|state:[{string.Join(";", new Control[] { parent, flow, first, second }.Select(State))}]");
        }
        foreach (bool autoSize in new[] { false, true })
        {
            using var parent = new Panel { Name = "parent", Size = new Size(300, 180) };
            using var table = new TableLayoutPanel
            {
                Name = "table", Size = new Size(104, 24), Padding = new Padding(2), ColumnCount = 2, RowCount = 1,
                AutoSize = autoSize, AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            using var first = new Control { Name = "first", Size = new Size(20, 10), Margin = new Padding(1) };
            using var second = new Control { Name = "second", Size = new Size(30, 12), Margin = new Padding(1) };
            table.Controls.Add(first, 0, 0);
            table.Controls.Add(second, 1, 0);
            parent.Controls.Add(table);
            parent.PerformLayout();
            table.PerformLayout();
            Record("table-measure-" + autoSize, $"preferred:{SizeText(table.PreferredSize)}|same-engine:{ReferenceEquals(parent.LayoutEngine, table.LayoutEngine)}|state:[{string.Join(";", new Control[] { parent, table, first, second }.Select(State))}]");
        }
    }

    private static void DockEdges()
    {
        using var parent = New("parent", new Rectangle(0, 0, 180, 120));
        using var top = New("top", new Rectangle(7, 8, 31, 17));
        using var bottom = New("bottom", new Rectangle(9, 10, 32, 19));
        using var left = New("left", new Rectangle(11, 12, 23, 33));
        using var right = New("right", new Rectangle(13, 14, 29, 35));
        using var fill = New("fill", new Rectangle(15, 16, 37, 39));
        parent.SuspendLayout();
        top.Dock = DockStyle.Top; bottom.Dock = DockStyle.Bottom; left.Dock = DockStyle.Left; right.Dock = DockStyle.Right; fill.Dock = DockStyle.Fill;
        parent.Controls.AddRange([top, bottom, left, right, fill]);
        Control[] controls = [parent, top, bottom, left, right, fill];
        Step("dock-resume", parent.ResumeLayout, controls);
        Step("dock-padding", () => parent.Padding = new Padding(3, 5, 7, 11), controls);
        Step("dock-resize", () => parent.Size = new Size(211, 149), controls);
        Step("dock-hide", () => right.Visible = false, controls);
        Step("dock-margin", () => left.Margin = new Padding(20, 30, 40, 50), controls);
        Step("dock-show", () => right.Visible = true, controls);
        Step("dock-order", () => parent.Controls.SetChildIndex(fill, 0), controls);
        Step("dock-repeat", parent.PerformLayout, controls);
        Step("dock-undock", () => top.Dock = DockStyle.None, controls);
    }

    private static void AnchorAll()
    {
        for (int value = 0; value <= 15; value++)
        {
            using var parent = New("parent", new Rectangle(0, 0, 120, 90));
            using var child = New("child", new Rectangle(11, 13, 31, 23));
            child.Anchor = (AnchorStyles)value;
            parent.Controls.Add(child);
            Step("anchor-" + value + "-grow-odd", () => parent.Size = new Size(127, 95), parent, child);
            Step("anchor-" + value + "-grow-one", () => parent.Size = new Size(128, 96), parent, child);
            Step("anchor-" + value + "-restore", () => parent.Size = new Size(120, 90), parent, child);
            Step("anchor-" + value + "-shrink", () => parent.Size = new Size(30, 20), parent, child);
            Step("anchor-" + value + "-restore-two", () => parent.Size = new Size(120, 90), parent, child);
        }
    }

    private static void DockAnchorEvents()
    {
        using var parent = New("parent", new Rectangle(0, 0, 160, 100));
        using var child = New("child", new Rectangle(7, 8, 33, 22));
        parent.Controls.Add(child);
        Step("switch-anchor-right", () => child.Anchor = AnchorStyles.Bottom | AnchorStyles.Right, parent, child);
        Step("switch-dock-top", () => child.Dock = DockStyle.Top, parent, child);
        Step("switch-anchor-default", () => child.Anchor = AnchorStyles.Top | AnchorStyles.Left, parent, child);
        Step("switch-dock-fill", () => child.Dock = DockStyle.Fill, parent, child);
        Step("switch-anchor-right-again", () => child.Anchor = AnchorStyles.Bottom | AnchorStyles.Right, parent, child);
        Step("switch-dock-left", () => child.Dock = DockStyle.Left, parent, child);
        Step("switch-dock-none", () => child.Dock = DockStyle.None, parent, child);
        foreach (int value in new[] { 17, -1, 16, 0, 3 })
            Step("switch-anchor-raw-" + value, () => child.Anchor = (AnchorStyles)value, parent, child);
    }

    private static void AnchorExplicit()
    {
        foreach (AnchorStyles anchor in new[] { AnchorStyles.None, AnchorStyles.Right | AnchorStyles.Bottom, AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom })
        {
            using var parent = New("parent", new Rectangle(0, 0, 120, 90));
            using var child = New("child", new Rectangle(11, 13, 31, 23));
            child.Anchor = anchor;
            parent.Controls.Add(child);
            string prefix = "anchor-explicit-" + (int)anchor;
            Step(prefix + "-grow", () => parent.Size = new Size(131, 99), parent, child);
            Step(prefix + "-move", () => child.Location = new Point(17, 19), parent, child);
            Step(prefix + "-restore-after-move", () => parent.Size = new Size(120, 90), parent, child);
            Step(prefix + "-size", () => child.Size = new Size(37, 29), parent, child);
            Step(prefix + "-grow-after-size", () => parent.Size = new Size(131, 99), parent, child);
            Step(prefix + "-raw-none", () => child.DirectBounds(23, 27, 41, 33, BoundsSpecified.None), parent, child);
            Step(prefix + "-restore-after-raw", () => parent.Size = new Size(120, 90), parent, child);
            Step(prefix + "-padding", () => parent.Padding = new Padding(3, 5, 7, 11), parent, child);
            Step(prefix + "-suspend", () => { parent.SuspendLayout(); parent.Size = new Size(139, 107); child.Bounds = new Rectangle(29, 31, 43, 35); }, parent, child);
            Step(prefix + "-resume-false", () => parent.ResumeLayout(false), parent, child);
            Step(prefix + "-perform", parent.PerformLayout, parent, child);
            Step(prefix + "-resize", () => parent.Size = new Size(151, 119), parent, child);
        }
    }

    private static void AutoSizeLayout()
    {
        using var parent = New("parent", new Rectangle(0, 0, 160, 100));
        using var child = new PreferredControl { Name = "child", Bounds = new Rectangle(7, 8, 33, 22), Desired = new Size(51, 37) };
        Watch(child); parent.Controls.Add(child);
        Step("auto-enable", () => child.AutoSize = true, parent, child);
        Step("auto-grow-shrink", () => { child.SetMode(AutoSizeMode.GrowAndShrink); parent.PerformLayout(); }, parent, child);
        Step("auto-desired-small", () => { child.Desired = new Size(12, 9); parent.PerformLayout(); }, parent, child);
        Step("auto-grow-only", () => { child.SetMode(AutoSizeMode.GrowOnly); parent.PerformLayout(); }, parent, child);
        Step("auto-dock-top", () => child.Dock = DockStyle.Top, parent, child);
        Step("auto-dock-left", () => child.Dock = DockStyle.Left, parent, child);
        Step("auto-dock-fill", () => child.Dock = DockStyle.Fill, parent, child);
        Step("auto-disable", () => child.AutoSize = false, parent, child);
        Step("auto-undock", () => child.Dock = DockStyle.None, parent, child);
    }

    private static void ResumeInit()
    {
        using var parent = New("parent", new Rectangle(0, 0, 160, 100));
        using var child = New("child", new Rectangle(7, 8, 33, 22));
        child.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        parent.Controls.Add(child);
        Step("resume-false-unsuspended", () => parent.ResumeLayout(false), parent, child);
        Step("resume-suspend-edit", () => { parent.SuspendLayout(); parent.Size = new Size(180, 120); child.Location = new Point(20, 21); }, parent, child);
        Step("resume-false", () => parent.ResumeLayout(false), parent, child);
        Step("resume-explicit", parent.PerformLayout, parent, child);
        Step("resume-resize", () => parent.Size = new Size(200, 140), parent, child);
        Step("resume-direct-init", child.DirectInit, parent, child);
        Step("resume-resize-two", () => parent.Size = new Size(210, 150), parent, child);
        using var other = New("other", new Rectangle(0, 0, 300, 200));
        Step("reparent", () => child.Parent = other, parent, other, child);
        Step("reparent-resize", () => other.Size = new Size(320, 220), parent, other, child);
    }

    private static void FormStrips()
    {
        using var form = new Form { Name = "form", AutoScaleMode = AutoScaleMode.None, ClientSize = new Size(320, 180) };
        using var menu = new MenuStrip { Name = "menu", AutoSize = false, Height = 20, Dock = DockStyle.Top };
        using var tool = new ToolStrip { Name = "tool", AutoSize = false, Height = 25, Dock = DockStyle.Top };
        using var tool2 = new ToolStrip { Name = "tool2", AutoSize = false, Height = 15, Dock = DockStyle.Top };
        using var status = new StatusStrip { Name = "status", AutoSize = false, Height = 18, Dock = DockStyle.Bottom };
        using var fill = New("fill", new Rectangle(4, 6, 30, 20));
        fill.Dock = DockStyle.Fill;
        Watch(form); Watch(menu); Watch(tool); Watch(tool2); Watch(status);
        Control[] controls = [form, menu, tool, tool2, status, fill];
        Step("strips-add", () => { form.SuspendLayout(); form.Controls.AddRange([menu, tool, tool2, status, fill]); form.ResumeLayout(); }, controls);
        Step("strips-padding", () => form.Padding = new Padding(3, 5, 7, 11), controls);
        Step("strips-resize", () => form.ClientSize = new Size(350, 210), controls);
        Step("strips-undock", () => { tool.Dock = DockStyle.None; tool.Bounds = new Rectangle(23, 31, 99, 27); }, controls);
        Step("strips-repeat", form.PerformLayout, controls);
        Step("strips-hide", () => menu.Visible = false, controls);
        Step("strips-show", () => menu.Visible = true, controls);
        using var panel = new Panel { Name = "panel", Bounds = new Rectangle(9, 11, 180, 100), Padding = new Padding(2, 3, 4, 5) };
        Watch(panel);
        Step("strips-nest", () => { form.Controls.Add(panel); tool.Parent = panel; tool.Dock = DockStyle.Top; }, form, panel, tool);
        Step("strips-nest-resize", () => panel.Size = new Size(211, 133), form, panel, tool);
    }

    private static void SplitRatio()
    {
        foreach (Orientation orientation in new[] { Orientation.Vertical, Orientation.Horizontal })
        foreach (FixedPanel fixedPanel in new[] { FixedPanel.None, FixedPanel.Panel1, FixedPanel.Panel2 })
        {
            using var split = new SplitContainer { Name = "split", Orientation = orientation, Size = new Size(240, 240), SplitterDistance = 90, FixedPanel = fixedPanel };
            split.Panel1.Name = "panel1"; split.Panel2.Name = "panel2";
            Watch(split); Watch(split.Panel1); Watch(split.Panel2);
            string prefix = orientation + "-" + fixedPanel;
            Step(prefix + "-initial", () => { }, split, split.Panel1, split.Panel2);
            foreach (int size in new[] { 260, 240, 241, 242, 240, 259, 240 })
                Step(prefix + "-size-" + size, () => split.Size = new Size(size, size), split, split.Panel1, split.Panel2);
            Step(prefix + "-suspend-edit", () => { split.SuspendLayout(); split.SplitterDistance = 100; split.Size = new Size(260, 260); }, split, split.Panel1, split.Panel2);
            Step(prefix + "-resume", split.ResumeLayout, split, split.Panel1, split.Panel2);
        }
    }

    private static void NativeForm()
    {
        using var form = new Form { Name = "form", AutoScaleMode = AutoScaleMode.None, ClientSize = new Size(320, 180) };
        using var menu = new MenuStrip { Name = "menu", AutoSize = false, Height = 20, Dock = DockStyle.Top };
        using var tool = new ToolStrip { Name = "tool", AutoSize = false, Height = 25, Dock = DockStyle.Top };
        using var fill = new Panel { Name = "fill", Dock = DockStyle.Fill };
        using var ordinary = New("ordinary", new Rectangle(23, 31, 47, 29));
        using var nested = New("nested", new Rectangle(9, 11, 31, 19));
        fill.Controls.Add(nested);
        form.SuspendLayout();
        form.Controls.AddRange([menu, tool, fill, ordinary]);
        form.ResumeLayout();
        // The driver bounds this real window scenario and preserves native failures.
        form.Show();
        PumpNativeMessages();
        NativeSnapshot("native-shown", form, menu, tool, fill, ordinary, nested);
        form.ClientSize = new Size(360, 210);
        PumpNativeMessages();
        NativeSnapshot("native-resized", form, menu, tool, fill, ordinary, nested);
        ordinary.Bounds = new Rectangle(37, 43, 53, 31);
        nested.Bounds = new Rectangle(13, 17, 37, 23);
        PumpNativeMessages();
        NativeSnapshot("native-manual-move", form, menu, tool, fill, ordinary, nested);
        tool.Dock = DockStyle.None;
        tool.Bounds = new Rectangle(19, 67, 107, 27);
        form.PerformLayout();
        PumpNativeMessages();
        NativeSnapshot("native-undocked-tool", form, menu, tool, fill, ordinary, nested);
        form.Close();
        PumpNativeMessages();
        Record("native-closed", string.Join(";", new Control[] { form, menu, tool, fill, ordinary, nested }.Select(c => $"{Id(c)}:handle:{c.IsHandleCreated}:disposed:{c.IsDisposed}")));
    }

    private static void NativeSnapshot(string name, Form form, params Control[] controls)
    {
        Record(name + "-form", $"client:{SizeText(form.ClientSize)}:handle:{form.IsHandleCreated}");
        foreach (Control control in controls)
        {
            if (!control.IsHandleCreated || control.Parent is null || !control.Parent.IsHandleCreated)
            {
                Record(name + "-" + Id(control), $"managed:{RectangleText(control.Bounds)}:native:missing-handle");
                continue;
            }
            if (!GetWindowRect(control.Handle, out NativeRectangle rectangle))
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GetWindowRect failed for " + Id(control));
            var topLeft = new NativePoint { X = rectangle.Left, Y = rectangle.Top };
            var bottomRight = new NativePoint { X = rectangle.Right, Y = rectangle.Bottom };
            if (!ScreenToClient(control.Parent.Handle, ref topLeft) || !ScreenToClient(control.Parent.Handle, ref bottomRight))
                throw new Win32Exception(Marshal.GetLastWin32Error(), "ScreenToClient failed for " + Id(control));
            Record(name + "-" + Id(control), $"managed:{RectangleText(control.Bounds)}:native:{topLeft.X},{topLeft.Y},{bottomRight.X - topLeft.X},{bottomRight.Y - topLeft.Y}:parent:{Id(control.Parent)}");
        }
    }

    private static void PumpNativeMessages()
    {
        long deadline = Environment.TickCount64 + 2000;
        int messageCount = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            // WM_QUIT has no window procedure and belongs to the enclosing loop.
            if (message.Message == 0x0012)
            {
                PostQuitMessage(unchecked((int)message.WParam));
                return;
            }
            if (++messageCount > 4096 || Environment.TickCount64 > deadline)
                throw new TimeoutException("The native layout message pump did not become idle.");
            _ = TranslateMessage(in message);
            _ = DispatchMessageW(in message);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRectangle { public int Left, Top, Right, Bottom; }
    [StructLayout(LayoutKind.Sequential)]
    private struct NativePoint { public int X, Y; }
    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage { public nint HWnd; public uint Message; public nuint WParam; public nint LParam; public uint Time; public NativePoint Point; public uint Private; }
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint window, out NativeRectangle rectangle);
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ScreenToClient(nint window, ref NativePoint point);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")]
    private static extern nint DispatchMessageW(in NativeMessage message);
    [DllImport("user32.dll")]
    private static extern void PostQuitMessage(int exitCode);

    private static TrackedControl New(string name, Rectangle bounds)
    {
        var control = new TrackedControl { Name = name, Bounds = bounds };
        Watch(control);
        return control;
    }

    private static void Watch(Control control)
    {
        control.Layout += (_, e) => Trace($"layout:{Id(control)}:{Args(e)}:children:[{string.Join(";", control.Controls.Cast<Control>().Select(Geometry))}]");
        control.LocationChanged += (_, _) => Trace("location:" + Geometry(control));
        control.Move += (_, _) => Trace("move:" + Geometry(control));
        control.Resize += (_, _) => Trace("resize:" + Geometry(control));
        control.SizeChanged += (_, _) => Trace("size:" + Geometry(control));
        control.ClientSizeChanged += (_, _) => Trace("client:" + Geometry(control));
        control.DockChanged += (_, _) => Trace($"dock:{Id(control)}:{control.Dock}:{control.Anchor}");
        control.AutoSizeChanged += (_, _) => Trace("autosize:" + Id(control) + ":" + control.AutoSize);
        control.ParentChanged += (_, _) => Trace("parent:" + Id(control) + ":" + Id(control.Parent));
    }

    private static void Step(string name, Action action, params Control[] controls)
    {
        Events.Clear(); _recording = true;
        Exception? failure = null;
        try { action(); }
        catch (Exception exception) { failure = exception; }
        finally { _recording = false; }
        Record(name, $"exception:{ExceptionText(failure)}|events:[{string.Join(";", Events)}]|state:[{string.Join(";", controls.Select(State))}]");
        if (failure is ProbeRecursionException) throw failure;
    }

    private static void Trace(string value)
    {
        if (!_recording) return;
        if (Events.Count >= 512) throw new ProbeRecursionException();
        Events.Add(value);
    }

    private static string Id(Control? control) => control is null ? "null" : control.Name.Length == 0 ? "unnamed" : control.Name;
    private static string Args(LayoutEventArgs e) => $"affected:{Id(e.AffectedControl)}:property:{e.AffectedProperty ?? "null"}";
    private static string SizeText(Size size) => $"{size.Width},{size.Height}";
    private static string RectangleText(Rectangle rectangle) => $"{rectangle.X},{rectangle.Y},{rectangle.Width},{rectangle.Height}";
    private static string Geometry(Control control) => Id(control) + "(" + RectangleText(control.Bounds) + ")";
    private static string State(Control control) => $"{Geometry(control)}:client:{SizeText(control.ClientSize)}:display:{RectangleText(control.DisplayRectangle)}:dock:{control.Dock}:anchor:{(int)control.Anchor}:auto:{control.AutoSize}:visible:{control.Visible}:parent:{Id(control.Parent)}:handle:{control.IsHandleCreated}:disposed:{control.IsDisposed}";
    private static string ExceptionText(Exception? exception) => exception is null ? "none" : ReferenceEquals(exception, OriginalFailure) ? "original" : exception.GetType().Name + (exception is ArgumentException argument ? ":" + argument.ParamName : "");
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + (value?.ToString() ?? "null"));

    private class TrackedControl : Control
    {
        internal void DirectInit() => InitLayout();
        internal void DirectBounds(int x, int y, int width, int height, BoundsSpecified specified) => SetBoundsCore(x, y, width, height, specified);
        internal void SetMode(AutoSizeMode mode) => SetAutoSizeMode(mode);
        protected override void InitLayout()
        {
            Trace("init-enter:" + Id(this) + ":parent:" + Id(Parent));
            base.InitLayout();
            Trace("init-exit:" + Id(this) + ":parent:" + Id(Parent));
        }
    }

    private sealed class PreferredControl : TrackedControl
    {
        internal Size Desired = new(51, 37);
        internal bool UseBasePreferred;
        internal bool ThrowPreferred;
        public override Size GetPreferredSize(Size proposedSize)
        {
            Trace("preferred-enter:" + Id(this) + ":" + SizeText(proposedSize));
            if (ThrowPreferred) throw OriginalFailure;
            Size result = UseBasePreferred ? base.GetPreferredSize(proposedSize) : Desired;
            Trace("preferred-exit:" + Id(this) + ":" + SizeText(result));
            return result;
        }
    }

    private sealed class EngineControl : TrackedControl
    {
        private ProbeEngine? _engine;
        internal ProbeEngine Engine => _engine ??= new ProbeEngine(this);
        public override LayoutEngine LayoutEngine
        {
            get { Trace("engine-get:" + Id(this)); return Engine; }
        }
    }

    private sealed class ProbeEngine(Control owner) : LayoutEngine
    {
        internal bool Result;
        internal bool ThrowLayout;
        internal bool ThrowInit;
        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            Trace($"engine-layout:{Id(owner)}:container:{(container is Control control ? Id(control) : container?.GetType().Name ?? "null")}:{Args(layoutEventArgs)}:result:{Result}");
            if (ThrowLayout) throw OriginalFailure;
            return Result;
        }
        public override void InitLayout(object child, BoundsSpecified specified)
        {
            Trace($"engine-init:{Id(owner)}:child:{(child is Control control ? Id(control) : child?.GetType().Name ?? "null")}:specified:{(int)specified}");
            if (ThrowInit) throw OriginalFailure;
            base.InitLayout(child, specified);
        }
    }

    private sealed class PlainEngine : LayoutEngine { }
    private sealed class ProbeRecursionException : Exception { }
}
