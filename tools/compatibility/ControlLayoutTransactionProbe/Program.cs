using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
#if REFERENCE
using System.Windows.Forms;
#else
using Lumina.Forms;
#endif

// The driver starts each scenario in a separate STA process. All observations
// come from public/protected APIs; no private runtime layout state is inspected.
internal static class Program
{
    private static readonly List<string> Events = [];
    private static readonly InvalidOperationException CallbackFailure = new("layout callback failure");
    private static bool _recording;
    private static int _eventCount;

    [STAThread]
    private static void Main(string[] args)
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
#endif
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        if (args.Length != 1) throw new ArgumentException("Specify exactly one layout transaction scenario.");
        Record("scenario", args[0]);
        Console.Error.WriteLine("BEGIN " + args[0]);
        switch (args[0])
        {
            case "metadata": Metadata(); break;
            case "event-args": EventArgsContracts(); break;
            case "immediate": Immediate(); break;
            case "resume": Resume(); break;
            case "deferred-cache": DeferredCache(); break;
            case "direct-hooks": DirectHooks(); break;
            case "exceptions": Exceptions(); break;
            case "reentrant-perform": ReentrantPerform(); break;
            case "reentrant-suspend": ReentrantSuspend(); break;
            case "parent-collection": ParentCollection(); break;
            case "parent-properties": ParentProperties(); break;
            case "property-events": PropertyEvents(); break;
            case "container-control": ContainerTransactions(new Control()); break;
            case "container-scrollable": ContainerTransactions(new ScrollableControl()); break;
            case "container-container": ContainerTransactions(new ContainerControl()); break;
            case "container-panel": ContainerTransactions(new Panel()); break;
            case "container-form": ContainerTransactions(new Form()); break;
            case "container-flow": ContainerTransactions(new FlowLayoutPanel()); break;
            case "container-table": ContainerTransactions(new TableLayoutPanel()); break;
            case "container-split": ContainerTransactions(new SplitContainer()); break;
            case "form-event-views": FormEventViews(); break;
            case "flow-boundaries": FlowBoundaries(); break;
            case "table-boundaries": TableBoundaries(); break;
            case "split-boundaries": SplitBoundaries(); break;
            default: throw new ArgumentException("Unknown layout transaction scenario: " + args[0]);
        }
        Console.Error.WriteLine("END " + args[0]);
        Record("complete", true);
    }

    private static void Metadata()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        // Literal types/names let trimming retain only the queried members.
        // Passing Type values through an array would lose member annotations.
        Record("method-SuspendLayout-0", MethodShape(typeof(Control).GetMethod("SuspendLayout", flags, Type.EmptyTypes)));
        Record("method-ResumeLayout-0", MethodShape(typeof(Control).GetMethod("ResumeLayout", flags, Type.EmptyTypes)));
        Record("method-ResumeLayout-1", MethodShape(typeof(Control).GetMethod("ResumeLayout", flags, [typeof(bool)])));
        Record("method-PerformLayout-0", MethodShape(typeof(Control).GetMethod("PerformLayout", flags, Type.EmptyTypes)));
        Record("method-PerformLayout-2", MethodShape(typeof(Control).GetMethod("PerformLayout", flags, [typeof(Control), typeof(string)])));
        Record("method-OnLayout-1", MethodShape(typeof(Control).GetMethod("OnLayout", flags, [typeof(LayoutEventArgs)])));
        Record("method-InitLayout-0", MethodShape(typeof(Control).GetMethod("InitLayout", flags, Type.EmptyTypes)));
        Record("event-Control", EventShape(typeof(Control).GetEvent("Layout")));
        Record("event-ScrollableControl", EventShape(typeof(ScrollableControl).GetEvent("Layout")));
        Record("event-Form", EventShape(typeof(Form).GetEvent("Layout")));
        Record("args-type", $"base:{typeof(LayoutEventArgs).BaseType?.Name}|sealed:{typeof(LayoutEventArgs).IsSealed}");
        Record("args-constructor-Control", ConstructorShape(typeof(LayoutEventArgs).GetConstructor([typeof(Control), typeof(string)])));
        Record("args-constructor-IComponent", ConstructorShape(typeof(LayoutEventArgs).GetConstructor([typeof(IComponent), typeof(string)])));
        Record("args-property-AffectedControl", PropertyShape(typeof(LayoutEventArgs).GetProperty("AffectedControl")));
        Record("args-property-AffectedComponent", PropertyShape(typeof(LayoutEventArgs).GetProperty("AffectedComponent")));
        Record("args-property-AffectedProperty", PropertyShape(typeof(LayoutEventArgs).GetProperty("AffectedProperty")));
        Record("handler", MethodShape(typeof(LayoutEventHandler).GetMethod("Invoke")));
    }

    private static string EventShape(EventInfo? item) => item is null ? "missing" : $"declaring:{item.DeclaringType?.Name}|type:{item.EventHandlerType?.Name}|add:{MethodShape(item.AddMethod)}|remove:{MethodShape(item.RemoveMethod)}";
    private static string ConstructorShape(ConstructorInfo? constructor) => constructor is null ? "missing" : string.Join(",", constructor.GetParameters().Select(p => p.ParameterType.Name + ":" + p.Name));
    private static string PropertyShape(PropertyInfo? property) => property is null ? "missing" : $"type:{property.PropertyType.Name}|get:{MethodShape(property.GetMethod)}|set:{MethodShape(property.SetMethod)}";

    private static string MethodShape(MethodInfo? method) => method is null ? "missing" :
        $"access:{(method.IsPublic ? "public" : method.IsFamily ? "protected" : method.IsFamilyOrAssembly ? "protected-internal" : method.IsAssembly ? "internal" : "private")},static:{method.IsStatic},virtual:{method.IsVirtual},final:{method.IsFinal},return:{method.ReturnType.Name},params:[{string.Join(",", method.GetParameters().Select(p => p.ParameterType.Name))}]";

    private static void EventArgsContracts()
    {
        using var control = NewControl("affected");
        using var component = new Component();
        foreach (string? property in new string?[] { null, "", "Bounds", "\u5e03\u5c40\0property" })
        {
            Record("control-args", Describe(new LayoutEventArgs(control, property)));
            Record("component-control-args", Describe(new LayoutEventArgs((IComponent)control, property)));
            Record("component-args", Describe(new LayoutEventArgs(component, property)));
            Record("null-control-args", Describe(new LayoutEventArgs((Control?)null, property)));
            Record("null-component-args", Describe(new LayoutEventArgs((IComponent?)null, property)));
        }
        string unique = new(['B', 'o', 'u', 'n', 'd', 's']);
        var identity = new LayoutEventArgs(control, unique);
        Record("args-reference-identity", $"control:{ReferenceEquals(control, identity.AffectedControl)}|component:{ReferenceEquals(control, identity.AffectedComponent)}|property:{ReferenceEquals(unique, identity.AffectedProperty)}");
        control.Dispose();
        Record("args-after-component-dispose", Describe(identity));
        LayoutEventArgs weak = MakeUnrootedArgs();
        GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();
        Record("args-after-component-collection", Describe(weak));
        GC.KeepAlive(control);
        GC.KeepAlive(component);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static LayoutEventArgs MakeUnrootedArgs() => new(new Component(), "Weak");

    private static void Immediate()
    {
        using var root = NewControl("root");
        using var affected = NewControl("affected");
        Step("perform-empty", root.PerformLayout, root);
        Step("perform-self-bounds", () => root.PerformLayout(root, "Bounds"), root);
        Step("perform-other-custom", () => root.PerformLayout(affected, "Custom"), root, affected);
        Step("perform-null-bounds", () => root.PerformLayout(null, "Bounds"), root);
        Step("perform-self-null", () => root.PerformLayout(root, null), root);
        Step("perform-null-null", () => root.PerformLayout(null, null), root);
        Step("perform-empty-property", () => root.PerformLayout(affected, ""), root);
        Step("perform-repeat", () => { root.PerformLayout(affected, "Same"); root.PerformLayout(affected, "Same"); }, root);
        Step("dispose", root.Dispose, root);
        Step("perform-after-dispose", () => root.PerformLayout(affected, "Disposed"), root);
    }

    private static void Resume()
    {
        using var root = NewControl("root");
        Step("resume-without-suspend", root.ResumeLayout, root);
        Step("resume-true-without-suspend", () => root.ResumeLayout(true), root);
        Step("resume-false-without-suspend", () => root.ResumeLayout(false), root);
        Step("empty-suspend", root.SuspendLayout, root);
        Step("empty-resume-true", () => root.ResumeLayout(true), root);
        Step("empty-resume-true-again", () => root.ResumeLayout(true), root);
        Step("empty-suspend-false", root.SuspendLayout, root);
        Step("empty-resume-false", () => root.ResumeLayout(false), root);
        Step("empty-perform-after-false", root.PerformLayout, root);
        Step("nested-suspend", () => { root.SuspendLayout(); root.SuspendLayout(); root.PerformLayout(root, "First"); }, root);
        Step("nested-inner-true", () => root.ResumeLayout(true), root);
        Step("nested-outer-true", () => root.ResumeLayout(true), root);
        Step("nested-true-extra", () => root.ResumeLayout(true), root);
        Step("nested-second-suspend", () => { root.SuspendLayout(); root.SuspendLayout(); root.PerformLayout(root, "Second"); }, root);
        Step("nested-inner-false", () => root.ResumeLayout(false), root);
        Step("nested-outer-default", root.ResumeLayout, root);
        Step("nested-third-suspend", () => { root.SuspendLayout(); root.SuspendLayout(); root.PerformLayout(root, "Third"); }, root);
        Step("nested-third-inner-true", () => root.ResumeLayout(true), root);
        Step("nested-third-outer-false", () => root.ResumeLayout(false), root);
        Step("nested-third-perform", root.PerformLayout, root);
    }

    private static void DeferredCache()
    {
        using var first = NewControl("first");
        using var second = NewControl("second");
        foreach (string sequence in new[] { "first-second", "first-empty", "empty-first", "null-first", "first-null", "first-same", "first-other-null" })
        {
            using var root = NewControl("root");
            Step(sequence + "-suspend", root.SuspendLayout, root);
            Step(sequence + "-requests", () =>
            {
                if (sequence == "empty-first") root.PerformLayout();
                else if (sequence == "null-first") root.PerformLayout(null, null);
                else root.PerformLayout(first, "First");
                switch (sequence)
                {
                    case "first-second": root.PerformLayout(second, "Second"); break;
                    case "first-empty": root.PerformLayout(); break;
                    case "empty-first": case "null-first": root.PerformLayout(first, "First"); break;
                    case "first-null": root.PerformLayout(null, null); break;
                    case "first-same": root.PerformLayout(first, "Second"); break;
                    case "first-other-null": root.PerformLayout(second, null); break;
                }
            }, root);
            Step(sequence + "-resume", () => root.ResumeLayout(true), root);
            Step(sequence + "-following-perform", root.PerformLayout, root);
        }
        foreach (string next in new[] { "empty", "explicit", "resume-true", "suspend-resume" })
        {
            using var root = NewControl("root");
            Step("false-" + next + "-defer", () => { root.SuspendLayout(); root.PerformLayout(first, "Cached"); root.ResumeLayout(false); }, root);
            Step("false-" + next + "-next", () =>
            {
                switch (next)
                {
                    case "empty": root.PerformLayout(); break;
                    case "explicit": root.PerformLayout(second, "Explicit"); break;
                    case "resume-true": root.ResumeLayout(true); break;
                    case "suspend-resume": root.SuspendLayout(); root.ResumeLayout(true); break;
                }
            }, root);
            Step("false-" + next + "-after", root.PerformLayout, root);
        }
    }

    private static void DirectHooks()
    {
        using var root = NewControl("root");
        using var affected = NewControl("affected");
        var arguments = new LayoutEventArgs(affected, "Direct");
        root.ExpectedArguments = arguments;
        Step("direct-layout", () => root.DirectLayout(arguments), root);
        Step("direct-layout-suspended", () => { root.SuspendLayout(); root.DirectLayout(arguments); }, root);
        Step("direct-layout-resume", root.ResumeLayout, root);
        root.ExpectedArguments = null;
        Step("direct-layout-null", () => root.DirectLayout(null!), root);
        root.SkipBase = true;
        Step("override-skips-base", () => root.PerformLayout(affected, "Skip"), root);
        root.SkipBase = false;
        Step("override-base-restored", () => root.PerformLayout(affected, "Restored"), root);
        Step("direct-init-no-parent", root.DirectInit, root);
        affected.Controls.Add(root);
        Step("direct-init-with-parent", root.DirectInit, affected, root);
        Step("direct-init-with-parent-suspended", () => { affected.SuspendLayout(); root.DirectInit(); }, affected, root);
        Step("direct-init-parent-resume", affected.ResumeLayout, affected, root);
    }

    private static void Exceptions()
    {
        using var root = NewControl("root");
        LayoutEventHandler throwing = (_, _) => { Trace("throw-event"); throw CallbackFailure; };
        root.Layout += throwing;
        Step("event-throws", () => root.PerformLayout(root, "Throw"), root);
        root.Layout -= throwing;
        Step("event-retry", () => root.PerformLayout(root, "Retry"), root);
        root.ThrowBeforeBase = true;
        Step("override-throws", () => root.PerformLayout(root, "ThrowBeforeBase"), root);
        root.ThrowBeforeBase = false;
        Step("override-retry", root.PerformLayout, root);
        root.Layout += throwing;
        Step("deferred-event-throws", () => { root.SuspendLayout(); root.PerformLayout(root, "DeferredThrow"); root.ResumeLayout(true); }, root);
        root.Layout -= throwing;
        Step("deferred-event-retry", root.PerformLayout, root);
        bool first = true;
        LayoutEventHandler reentrantThrow = (_, _) =>
        {
            if (!first) return;
            first = false;
            root.PerformLayout(root, "PendingBeforeThrow");
            Trace("throw-after-pending");
            throw CallbackFailure;
        };
        root.Layout += reentrantThrow;
        Step("reentrant-event-throws", () => root.PerformLayout(root, "OuterThrow"), root);
        root.Layout -= reentrantThrow;
        Step("reentrant-event-after-throw", root.PerformLayout, root);
    }

    private static void ReentrantPerform()
    {
        using var root = NewControl("root");
        using var affected = NewControl("affected");
        bool first = true;
        LayoutEventHandler reentrant = (_, _) =>
        {
            if (!first) return;
            first = false;
            Trace("callback-enter");
            root.PerformLayout(affected, "InnerFirst");
            root.PerformLayout(root, "InnerSecond");
            Trace("callback-exit");
        };
        root.Layout += reentrant;
        Step("reentrant-outer", () => root.PerformLayout(root, "Outer"), root);
        Step("reentrant-following-empty", root.PerformLayout, root);
        Step("reentrant-following-resume", root.ResumeLayout, root);
        root.Layout -= reentrant;
        bool directFirst = true;
        LayoutEventHandler direct = (_, _) =>
        {
            if (!directFirst) return;
            directFirst = false;
            Trace("direct-callback-enter");
            root.DirectLayout(new LayoutEventArgs(affected, "DirectInner"));
            Trace("direct-callback-exit");
        };
        root.Layout += direct;
        Step("direct-reentrant-outer", () => root.PerformLayout(root, "DirectOuter"), root);
        root.Layout -= direct;
    }

    private static void ReentrantSuspend()
    {
        foreach (string policy in new[] { "true", "false", "leave-suspended" })
        {
            using var root = NewControl("root");
            bool first = true;
            LayoutEventHandler handler = (_, _) =>
            {
                if (!first) return;
                first = false;
                Trace("callback-suspend");
                root.SuspendLayout();
                root.PerformLayout(root, "Inner");
                if (policy != "leave-suspended") root.ResumeLayout(policy == "true");
                Trace("callback-return");
            };
            root.Layout += handler;
            Step(policy + "-outer", () => root.PerformLayout(root, "Outer"), root);
            Step(policy + "-following-empty", root.PerformLayout, root);
            Step(policy + "-following-resume", root.ResumeLayout, root);
            Step(policy + "-final-empty", root.PerformLayout, root);
            root.Layout -= handler;
        }
    }

    private static void ParentCollection()
    {
        using var parent = NewControl("parent");
        using var other = NewControl("other");
        using var first = NewControl("first");
        using var second = NewControl("second");
        using var third = NewControl("third");
        WatchCollection(parent); WatchCollection(other);
        Step("add-first", () => parent.Controls.Add(first), parent, first);
        Step("add-same-again", () => parent.Controls.Add(first), parent, first);
        Step("add-range", () => parent.Controls.AddRange([second, third]), parent, first, second, third);
        Step("set-child-index", () => parent.Controls.SetChildIndex(first, 2), parent, first);
        Step("set-child-index-same", () => parent.Controls.SetChildIndex(first, 2), parent, first);
        Step("remove-second", () => parent.Controls.Remove(second), parent, second);
        Step("remove-second-again", () => parent.Controls.Remove(second), parent, second);
        Step("reparent-first", () => other.Controls.Add(first), parent, other, first);
        Step("suspend-add-remove", () => { parent.SuspendLayout(); parent.Controls.Add(second); parent.Controls.Remove(third); }, parent, second, third);
        Step("resume-add-remove", parent.ResumeLayout, parent, second, third);
        Step("clear", parent.Controls.Clear, parent, second);
        Step("clear-empty", parent.Controls.Clear, parent);
        Step("add-null", () => parent.Controls.Add(null), parent);
    }

    private static void ParentProperties()
    {
        using var parent = NewControl("parent");
        using var child = NewControl("child");
        parent.Size = new Size(200, 150);
        child.Bounds = new Rectangle(10, 15, 30, 25);
        parent.Controls.Add(child);
        Step("child-location", () => child.Location = new Point(12, 17), parent, child);
        Step("child-size", () => child.Size = new Size(32, 27), parent, child);
        Step("child-bounds", () => child.Bounds = new Rectangle(14, 19, 34, 29), parent, child);
        Step("child-client-size", () => child.ClientSize = new Size(36, 31), parent, child);
        Step("child-padding", () => child.Padding = new Padding(2), parent, child);
        Step("child-margin", () => child.Margin = new Padding(4), parent, child);
        Step("child-visible-false", () => child.Visible = false, parent, child);
        Step("child-visible-true", () => child.Visible = true, parent, child);
        Step("child-anchor", () => child.Anchor = AnchorStyles.Top | AnchorStyles.Right, parent, child);
        Step("child-dock", () => child.Dock = DockStyle.Top, parent, child);
        Step("child-dock-none", () => child.Dock = DockStyle.None, parent, child);
        Step("child-autosize", () => child.AutoSize = true, parent, child);
        Step("child-minimum", () => child.MinimumSize = new Size(12, 13), parent, child);
        Step("child-maximum", () => child.MaximumSize = new Size(120, 130), parent, child);
        Step("parent-size", () => parent.Size = new Size(220, 170), parent, child);
        Step("parent-padding", () => parent.Padding = new Padding(3), parent, child);
    }

    private static void PropertyEvents()
    {
        foreach (string property in new[] { "Padding", "Margin", "Dock", "AutoSize" })
        {
            using var parent = NewControl("parent");
            using var child = new PropertyTraceControl { Name = "child" };
            WatchLayout(child);
            parent.Controls.Add(child);
            EventHandler observer = (sender, e) => Trace($"property-event:{property}:sender:{ReferenceEquals(sender, child)}:empty:{ReferenceEquals(e, EventArgs.Empty)}:expected:{ReferenceEquals(e, child.ExpectedPropertyArguments)}");
            AddPropertyHandler(child, property, observer);
            Step(property + "-changed", () => ChangeProperty(child, property), parent, child);
            Record(property + "-changed-values", PropertyValues(child));
            Step(property + "-same", () => ChangeProperty(child, property), parent, child);
            child.ExpectedPropertyArguments = new EventArgs();
            Step(property + "-direct-hook", () => child.DirectProperty(property, child.ExpectedPropertyArguments), parent, child);
        }
        foreach (string property in new[] { "Padding", "Margin", "Dock", "AutoSize" })
        {
            using var parent = NewControl("parent");
            using var child = new PropertyTraceControl { Name = "child" };
            WatchLayout(child);
            parent.Controls.Add(child);
            EventHandler throwing = (_, _) => { Trace("property-event-throws:" + property); throw CallbackFailure; };
            AddPropertyHandler(child, property, throwing);
            Step(property + "-throw", () => ChangeProperty(child, property), parent, child);
            Record(property + "-throw-values", PropertyValues(child));
            RemovePropertyHandler(child, property, throwing);
            Step(property + "-same-after-throw", () => ChangeProperty(child, property), parent, child);
            Step(property + "-parent-after-throw", parent.PerformLayout, parent, child);
        }
    }

    private static void ChangeProperty(Control control, string property)
    {
        switch (property)
        {
            case "Padding": control.Padding = new Padding(2); break;
            case "Margin": control.Margin = new Padding(4); break;
            case "Dock": control.Dock = DockStyle.Left; break;
            case "AutoSize": control.AutoSize = true; break;
            default: throw new ArgumentException("Unknown property", nameof(property));
        }
    }

    private static void AddPropertyHandler(Control control, string property, EventHandler handler)
    {
        switch (property)
        {
            case "Padding": control.PaddingChanged += handler; break;
            case "Margin": control.MarginChanged += handler; break;
            case "Dock": control.DockChanged += handler; break;
            case "AutoSize": control.AutoSizeChanged += handler; break;
        }
    }

    private static void RemovePropertyHandler(Control control, string property, EventHandler handler)
    {
        switch (property)
        {
            case "Padding": control.PaddingChanged -= handler; break;
            case "Margin": control.MarginChanged -= handler; break;
            case "Dock": control.DockChanged -= handler; break;
            case "AutoSize": control.AutoSizeChanged -= handler; break;
        }
    }

    private static string PropertyValues(Control control) => $"padding:{PaddingValues(control.Padding)}|margin:{PaddingValues(control.Margin)}|dock:{control.Dock}|auto-size:{control.AutoSize}";
    private static string PaddingValues(Padding value) => $"{value.Left},{value.Top},{value.Right},{value.Bottom}";

    private static void ContainerTransactions(Control container)
    {
        using (container)
        using (var first = NewControl("first"))
        using (var second = NewControl("second"))
        {
            container.Name = "container";
            container.Size = new Size(200, 150);
            if (container is Form form) form.AutoScaleMode = AutoScaleMode.None;
            first.Size = new Size(20, 15); second.Size = new Size(25, 15);
            // SplitContainer reserves its own children. Observe its public panels.
            Control host = container is SplitContainer split ? split.Panel1 : container;
            host.Name = host == container ? "container" : "panel1";
            host.Controls.AddRange([first, second]);
            container.Layout += (_, e) => Trace("container-event:" + Describe(e));
            if (host != container) host.Layout += (_, e) => Trace("panel-event:" + Describe(e));
            Step("container-perform", container.PerformLayout, container, first, second);
            Step("container-suspend", () => { container.SuspendLayout(); container.PerformLayout(first, "First"); container.PerformLayout(second, "Second"); }, container, first, second);
            Step("container-resume-false", () => container.ResumeLayout(false), container, first, second);
            Step("container-perform-cached", container.PerformLayout, container, first, second);
            Step("container-nested", () => { container.SuspendLayout(); container.SuspendLayout(); container.PerformLayout(first, "Nested"); container.ResumeLayout(true); }, container, first, second);
            Step("container-outer-resume", container.ResumeLayout, container, first, second);
            Step("container-extra-resume", container.ResumeLayout, container, first, second);
            Step("container-children-suspended", () => { container.SuspendLayout(); first.SuspendLayout(); first.PerformLayout(first, "Child"); container.PerformLayout(second, "Parent"); }, container, first, second);
            Step("container-parent-resume", container.ResumeLayout, container, first, second);
            Step("container-child-resume", first.ResumeLayout, container, first, second);
        }
    }

    private static void FormEventViews()
    {
        using var form = new Form { Name = "form", AutoScaleMode = AutoScaleMode.None };
        Control control = form;
        ScrollableControl scrollable = form;
        LayoutEventArgs? firstArgs = null;
        LayoutEventHandler formHandler = (sender, e) => { firstArgs = e; Trace($"form-view:sender:{ReferenceEquals(sender, form)}:{Describe(e)}"); };
        LayoutEventHandler controlHandler = (sender, e) => Trace($"control-view:sender:{ReferenceEquals(sender, form)}:same:{ReferenceEquals(e, firstArgs)}:{Describe(e)}");
        LayoutEventHandler scrollableHandler = (sender, e) => Trace($"scrollable-view:sender:{ReferenceEquals(sender, form)}:same:{ReferenceEquals(e, firstArgs)}:{Describe(e)}");
        form.Layout += formHandler; control.Layout += controlHandler; scrollable.Layout += scrollableHandler;
        Step("form-view-perform", () => form.PerformLayout(form, "Form"), control);
        Step("control-view-perform", () => control.PerformLayout(form, "Control"), control);
        Step("scrollable-view-suspend", () => { scrollable.SuspendLayout(); form.PerformLayout(form, "Suspended"); }, control);
        Step("control-view-resume", control.ResumeLayout, control);
        control.Layout -= formHandler;
        Step("cross-view-remove-form-handler", () => form.PerformLayout(form, "Removed"), control);
    }

    private static void FlowBoundaries()
    {
        using var flow = new FlowLayoutPanel { Name = "flow", Size = new Size(200, 150), Margin = Padding.Empty, Padding = Padding.Empty };
        using var first = NewControl("first");
        using var second = NewControl("second");
        first.Size = new Size(20, 15); first.Margin = Padding.Empty;
        second.Size = new Size(25, 15); second.Margin = Padding.Empty;
        flow.Controls.AddRange([first, second]);
        flow.Layout += (_, e) => Trace($"flow-event:{Describe(e)}:bounds:{Geometry(first, second)}");
        GeometryStep("flow-initial", () => { }, flow, first, second);
        GeometryStep("flow-suspend", flow.SuspendLayout, flow, first, second);
        GeometryStep("flow-child-move-suspended", () => first.Location = new Point(80, 90), flow, first, second);
        GeometryStep("flow-direction-suspended", () => flow.FlowDirection = FlowDirection.TopDown, flow, first, second);
        GeometryStep("flow-size-suspended", () => flow.Size = new Size(220, 160), flow, first, second);
        GeometryStep("flow-resume", flow.ResumeLayout, flow, first, second);
        GeometryStep("flow-repeat-perform", flow.PerformLayout, flow, first, second);
        GeometryStep("flow-nested-direction", () => { flow.SuspendLayout(); flow.SuspendLayout(); flow.FlowDirection = FlowDirection.LeftToRight; flow.ResumeLayout(true); }, flow, first, second);
        GeometryStep("flow-outer-resume-false", () => flow.ResumeLayout(false), flow, first, second);
        GeometryStep("flow-explicit-after-false", flow.PerformLayout, flow, first, second);
    }

    private static void TableBoundaries()
    {
        using var table = new TableLayoutPanel { Name = "table", Size = new Size(200, 150), ColumnCount = 2, RowCount = 1, Margin = Padding.Empty, Padding = Padding.Empty };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
        using var first = NewControl("first");
        using var second = NewControl("second");
        first.Size = new Size(20, 15); first.Margin = Padding.Empty;
        second.Size = new Size(25, 15); second.Margin = Padding.Empty;
        table.Controls.Add(first, 0, 0); table.Controls.Add(second, 1, 0);
        table.Layout += (_, e) => Trace($"table-event:{Describe(e)}:bounds:{Geometry(first, second)}");
        GeometryStep("table-initial", () => { }, table, first, second);
        GeometryStep("table-suspend", table.SuspendLayout, table, first, second);
        GeometryStep("table-child-move-suspended", () => first.Location = new Point(70, 90), table, first, second);
        GeometryStep("table-size-suspended", () => table.Size = new Size(220, 160), table, first, second);
        GeometryStep("table-resume", table.ResumeLayout, table, first, second);
        GeometryStep("table-repeat-perform", table.PerformLayout, table, first, second);
        GeometryStep("table-nested-child-move", () => { table.SuspendLayout(); table.SuspendLayout(); second.Location = new Point(170, 90); table.ResumeLayout(true); }, table, first, second);
        GeometryStep("table-outer-resume-false", () => table.ResumeLayout(false), table, first, second);
        GeometryStep("table-explicit-after-false", table.PerformLayout, table, first, second);
    }

    private static void SplitBoundaries()
    {
        using var split = new SplitContainer { Name = "split", Size = new Size(240, 160), SplitterDistance = 80, Margin = Padding.Empty, Padding = Padding.Empty };
        split.Panel1.Name = "panel1"; split.Panel2.Name = "panel2";
        split.Layout += (_, e) => Trace($"split-event:{Describe(e)}:bounds:{Geometry(split.Panel1, split.Panel2)}");
        split.Panel1.Layout += (_, e) => Trace($"panel1-event:{Describe(e)}:bounds:{Geometry(split.Panel1, split.Panel2)}");
        split.Panel2.Layout += (_, e) => Trace($"panel2-event:{Describe(e)}:bounds:{Geometry(split.Panel1, split.Panel2)}");
        GeometryStep("split-initial", () => { }, split, split.Panel1, split.Panel2);
        GeometryStep("split-suspend", split.SuspendLayout, split, split.Panel1, split.Panel2);
        GeometryStep("split-distance-suspended", () => split.SplitterDistance = 90, split, split.Panel1, split.Panel2);
        GeometryStep("split-size-suspended", () => split.Size = new Size(260, 180), split, split.Panel1, split.Panel2);
        GeometryStep("split-resume", split.ResumeLayout, split, split.Panel1, split.Panel2);
        GeometryStep("split-repeat-perform", split.PerformLayout, split, split.Panel1, split.Panel2);
        GeometryStep("split-nested-distance", () => { split.SuspendLayout(); split.SuspendLayout(); split.SplitterDistance = 100; split.ResumeLayout(true); }, split, split.Panel1, split.Panel2);
        GeometryStep("split-outer-resume-false", () => split.ResumeLayout(false), split, split.Panel1, split.Panel2);
        GeometryStep("split-explicit-after-false", split.PerformLayout, split, split.Panel1, split.Panel2);
    }

    private static void GeometryStep(string name, Action action, params Control[] controls)
    {
        Step(name, action, controls);
        Record(name + "-bounds", Geometry(controls));
    }

    private static string Geometry(params Control[] controls) => string.Join(";", controls.Select(control => $"{Id(control)}({control.Bounds.X},{control.Bounds.Y},{control.Bounds.Width},{control.Bounds.Height})"));

    private static TraceControl NewControl(string name)
    {
        var control = new TraceControl { Name = name };
        WatchLayout(control);
        return control;
    }

    private static void WatchLayout(TraceControl control) =>
        control.Layout += (sender, e) => Trace($"event:{Id(control)}:sender:{ReferenceEquals(sender, control)}:same:{ReferenceEquals(e, control.ActiveArguments)}:expected:{ReferenceEquals(e, control.ExpectedArguments)}:{Describe(e)}");

    private static void WatchCollection(Control control)
    {
        control.ControlAdded += (_, e) => Trace($"added:{Id(control)}:{Id(e.Control)}");
        control.ControlRemoved += (_, e) => Trace($"removed:{Id(control)}:{Id(e.Control)}");
    }

    private static void Step(string name, Action action, params Control[] controls)
    {
        Events.Clear(); _eventCount = 0; _recording = true;
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
        if (++_eventCount > 128) throw new ProbeRecursionException();
        Events.Add(value);
    }

    private static string State(Control control) => $"{Id(control)}(parent:{Id(control.Parent)},children:{control.Controls.Count},handle:{control.IsHandleCreated},disposed:{control.IsDisposed})";
    private static string Describe(LayoutEventArgs? args) => args is null ? "args:null" : $"affected:{Id(args.AffectedControl)}|component:{(args.AffectedComponent is Control control ? Id(control) : args.AffectedComponent?.GetType().Name ?? "null")}|property:{Text(args.AffectedProperty)}";
    private static string Text(string? text) => text is null ? "null" : "\"" + text.Replace("\\", "\\\\").Replace("\0", "\\0").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"") + "\"";
    private static string Id(Control? control) => control is null ? "null" : control.Name.Length == 0 ? "unnamed" : control.Name;
    private static string ExceptionText(Exception? exception) => exception is null ? "none" : ReferenceEquals(exception, CallbackFailure) ? "original" : exception.GetType().Name + (exception is ArgumentException argument ? ":" + argument.ParamName : "");
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + (value?.ToString() ?? "null"));

    private class TraceControl : Control
    {
        internal bool SkipBase;
        internal bool ThrowBeforeBase;
        internal LayoutEventArgs? ActiveArguments;
        internal LayoutEventArgs? ExpectedArguments;
        internal void DirectLayout(LayoutEventArgs args) => OnLayout(args);
        internal void DirectInit() => InitLayout();
        protected override void OnLayout(LayoutEventArgs e)
        {
            Trace($"on-enter:{Id(this)}:{Describe(e)}");
            LayoutEventArgs? previous = ActiveArguments;
            ActiveArguments = e;
            try
            {
                if (ThrowBeforeBase) throw CallbackFailure;
                if (!SkipBase) base.OnLayout(e);
                Trace($"on-exit:{Id(this)}:{Describe(e)}");
            }
            finally { ActiveArguments = previous; }
        }
        protected override void InitLayout()
        {
            Trace($"init-enter:{Id(this)}:parent:{Id(Parent)}");
            base.InitLayout();
            Trace($"init-exit:{Id(this)}:parent:{Id(Parent)}");
        }
    }

    private sealed class PropertyTraceControl : TraceControl
    {
        internal EventArgs? ExpectedPropertyArguments;

        internal void DirectProperty(string property, EventArgs arguments)
        {
            switch (property)
            {
                case "Padding": OnPaddingChanged(arguments); break;
                case "Margin": OnMarginChanged(arguments); break;
                case "Dock": OnDockChanged(arguments); break;
                case "AutoSize": OnAutoSizeChanged(arguments); break;
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            Trace("property-on-enter:Padding"); base.OnPaddingChanged(e); Trace("property-on-exit:Padding");
        }
        protected override void OnMarginChanged(EventArgs e)
        {
            Trace("property-on-enter:Margin"); base.OnMarginChanged(e); Trace("property-on-exit:Margin");
        }
        protected override void OnDockChanged(EventArgs e)
        {
            Trace("property-on-enter:Dock"); base.OnDockChanged(e); Trace("property-on-exit:Dock");
        }
        protected override void OnAutoSizeChanged(EventArgs e)
        {
            Trace("property-on-enter:AutoSize"); base.OnAutoSizeChanged(e); Trace("property-on-exit:AutoSize");
        }
    }

    private sealed class ProbeRecursionException : Exception { }
}
