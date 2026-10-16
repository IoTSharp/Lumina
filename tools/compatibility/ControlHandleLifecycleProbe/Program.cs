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

// Each scenario is run in its own STA process by the driver. The output records
// observations, including failures, rather than substituting expected outcomes.
// Reading state never asks a control without a handle to create one.
internal static class Program
{
    private static readonly List<string> Events = [];
    private static readonly InvalidOperationException CallbackFailure = new("lifecycle callback failure");
    private static bool _quitObserved;

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
        if (args.Length != 1) throw new ArgumentException("Specify exactly one lifecycle scenario.");
        Record("scenario", args[0]);
        Console.Error.WriteLine("BEGIN " + args[0]);
        switch (args[0])
        {
            case "metadata": Metadata(); break;
            case "lazy": Lazy(); break;
            case "visible-tree": VisibleTree(); break;
            case "hidden-tree": HiddenTree(); break;
            case "destroy": Destroy(); break;
            case "recreate": Recreate(); break;
            case "external": ExternalDestroy(); break;
            case "mapping": Mapping(); break;
            case "reparent": Reparent(); break;
            case "posting": Posting(); break;
            case "form-lazy": FormLazy(); break;
            case "form-recreate": FormRecreate(); break;
            case "form-destroy": FormDestroy(); break;
            case "form-external": FormExternalDestroy(); break;
            case "form-close-uncreated": FormCloseUncreated(); break;
            case "exceptions-reentrancy": ExceptionsAndReentrancy(); break;
            default: throw new ArgumentException("Unknown lifecycle scenario: " + args[0]);
        }
        Console.Error.WriteLine("END " + args[0]);
        Record("complete", true);
    }

    private static void Metadata()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        foreach (string name in new[] { "CreateControl", "CreateHandle", "DestroyHandle", "RecreateHandle" })
        {
            MethodInfo? method = typeof(Control).GetMethod(name, flags, Type.EmptyTypes);
            Record("method-" + name, MethodShape(method));
        }
        foreach (string name in new[] { "OnCreateControl", "OnHandleCreated", "OnHandleDestroyed" })
        {
            Type[] parameters = name == "OnCreateControl" ? Type.EmptyTypes : [typeof(EventArgs)];
            Record("method-" + name, MethodShape(typeof(Control).GetMethod(name, flags, parameters)));
        }
        foreach (string name in new[] { "FromHandle", "FromChildHandle" })
            Record("method-" + name, MethodShape(typeof(Control).GetMethod(name, flags, [typeof(nint)])));
        foreach (string name in new[] { "Handle", "Created", "IsHandleCreated", "RecreatingHandle" })
        {
            PropertyInfo? property = typeof(Control).GetProperty(name, flags);
            Record("property-" + name, property is null ? "missing" : $"type:{property.PropertyType.Name}|get:{MethodShape(property.GetMethod)}|set:{MethodShape(property.SetMethod)}");
        }
        foreach (string name in new[] { "HandleCreated", "HandleDestroyed" })
        {
            EventInfo? eventInfo = typeof(Control).GetEvent(name, flags);
            Record("event-" + name, eventInfo is null ? "missing" : $"type:{eventInfo.EventHandlerType?.Name}|add:{MethodShape(eventInfo.AddMethod)}|remove:{MethodShape(eventInfo.RemoveMethod)}");
        }
    }

    private static string MethodShape(MethodInfo? method) => method is null ? "missing" :
        $"access:{(method.IsPublic ? "public" : method.IsFamily ? "protected" : method.IsFamilyOrAssembly ? "protected-internal" : method.IsAssembly ? "internal" : "private")},static:{method.IsStatic},virtual:{method.IsVirtual},final:{method.IsFinal},return:{method.ReturnType.Name},params:[{string.Join(",", method.GetParameters().Select(p => p.ParameterType.Name))}]";

    private static void Lazy()
    {
        using var root = NewControl("root");
        using var child = NewControl("child");
        using var grandchild = NewControl("grandchild");
        root.Controls.Add(child);
        child.Controls.Add(grandchild);
        Step("initial", () => { }, root, child, grandchild);
        Step("child-handle", () => _ = child.Handle, root, child, grandchild);
        nint first = child.Handle;
        Step("child-handle-again", () => Record("same-handle", child.Handle == first), root, child, grandchild);
        Step("child-create-control", child.CreateControl, root, child, grandchild);
        Step("root-create-control", root.CreateControl, root, child, grandchild);
        Step("repeat-create-control", root.CreateControl, root, child, grandchild);
        Step("dispose-root", root.Dispose, root, child, grandchild);
        Record("old-child-window", IsWindow(first));
    }

    private static void VisibleTree()
    {
        using var root = NewControl("root");
        using var first = NewControl("first");
        using var second = NewControl("second");
        using var nested = NewControl("nested");
        root.Controls.AddRange([first, second]);
        first.Controls.Add(nested);
        Step("create-root", root.CreateControl, root, first, nested, second);
        Record("native-parent-relations", $"first:{NativeParentIs(first, root)}|nested:{NativeParentIs(nested, first)}|second:{NativeParentIs(second, root)}");
        Step("create-root-again", root.CreateControl, root, first, nested, second);
        using var late = NewControl("late");
        Step("add-visible-to-created", () => root.Controls.Add(late), root, late);
        Step("dispose-root", root.Dispose, root, first, nested, second, late);
    }

    private static void HiddenTree()
    {
        using var root = NewControl("root");
        using var visible = NewControl("visible");
        using var hidden = NewControl("hidden");
        using var nested = NewControl("nested");
        hidden.Visible = false;
        root.Controls.AddRange([visible, hidden]);
        hidden.Controls.Add(nested);
        root.Visible = false;
        Step("hidden-root-create", root.CreateControl, root, visible, hidden, nested);
        Step("hidden-nested-handle", () => _ = nested.Handle, root, visible, hidden, nested);
        Step("hidden-direct-create", hidden.DirectCreate, root, visible, hidden, nested);
        Step("show-root", () => root.Visible = true, root, visible, hidden, nested);
        Step("show-hidden", () => hidden.Visible = true, root, visible, hidden, nested);
        Step("hide-root", () => root.Visible = false, root, visible, hidden, nested);
        Step("dispose-root", root.Dispose, root, visible, hidden, nested);
    }

    private static void Destroy()
    {
        using var root = NewControl("root");
        using var child = NewControl("child");
        root.Controls.Add(child);
        Step("destroy-before-create", root.DirectDestroy, root, child);
        Step("recreate-before-create", root.Rebuild, root, child);
        Step("direct-create", root.DirectCreate, root, child);
        Step("direct-create-again", root.DirectCreate, root, child);
        Step("create-control", root.CreateControl, root, child);
        nint rootHandle = ExistingHandle(root), childHandle = ExistingHandle(child);
        Step("destroy-handle", root.DirectDestroy, root, child);
        Record("destroyed-relations", $"root-valid:{IsWindow(rootHandle)}|child-valid:{IsWindow(childHandle)}|root-map:{Id(Control.FromHandle(rootHandle))}|child-map:{Id(Control.FromHandle(childHandle))}");
        Step("destroy-again", root.DirectDestroy, root, child);
        Step("create-after-destroy", root.CreateControl, root, child);
        Step("dispose", root.Dispose, root, child);
    }

    private static void Recreate()
    {
        using var root = NewControl("root");
        using var child = NewControl("child");
        using var hidden = NewControl("hidden");
        using var nested = NewControl("nested");
        root.Text = "lifecycle-中文";
        root.Bounds = new Rectangle(11, 13, 180, 95);
        child.Bounds = new Rectangle(7, 9, 55, 23);
        child.Enabled = false;
        hidden.Visible = false;
        root.Controls.AddRange([child, hidden]);
        child.Controls.Add(nested);
        Step("create-tree", root.CreateControl, root, child, hidden, nested);
        Step("create-hidden-handle", () => _ = hidden.Handle, root, child, hidden, nested);
        nint oldRoot = ExistingHandle(root), oldChild = ExistingHandle(child), oldHidden = ExistingHandle(hidden), oldNested = ExistingHandle(nested);
        Step("recreate-root", root.Rebuild, root, child, hidden, nested);
        Record("recreate-relations", $"old-root-valid:{IsWindow(oldRoot)}|root-same:{ExistingHandle(root) == oldRoot}|child-same:{ExistingHandle(child) == oldChild}|hidden-same:{ExistingHandle(hidden) == oldHidden}|nested-same:{ExistingHandle(nested) == oldNested}|child-parent:{NativeParentIs(child, root)}|hidden-parent:{NativeParentIs(hidden, root)}|nested-parent:{NativeParentIs(nested, child)}|old-root-map:{Id(Control.FromHandle(oldRoot))}");
        Record("recreate-state", $"root-text:{root.Text}|root-bounds:{Rect(root.Bounds)}|child-bounds:{Rect(child.Bounds)}|child-enabled:{child.Enabled}|hidden-visible:{hidden.Visible}|child-native-enabled:{IsWindowEnabled(ExistingHandle(child))}|hidden-native-visible:{IsWindowVisible(ExistingHandle(hidden))}");
        Step("recreate-child", child.Rebuild, root, child, hidden, nested);
        Record("recreate-child-relations", $"old-child-valid:{IsWindow(oldChild)}|nested-same:{ExistingHandle(nested) == oldNested}|child-parent:{NativeParentIs(child, root)}|nested-parent:{NativeParentIs(nested, child)}");
        Step("dispose-root", root.Dispose, root, child, hidden, nested);
    }

    private static void ExternalDestroy()
    {
        using var root = NewControl("root");
        using var child = NewControl("child");
        root.Controls.Add(child);
        Step("create", root.CreateControl, root, child);
        nint oldRoot = ExistingHandle(root), oldChild = ExistingHandle(child);
        Step("external-destroy-root", () => Record("destroy-return", DestroyWindow(oldRoot)), root, child);
        Record("external-destroy-relations", $"root-valid:{IsWindow(oldRoot)}|child-valid:{IsWindow(oldChild)}|root-map:{Id(Control.FromHandle(oldRoot))}|child-map:{Id(Control.FromHandle(oldChild))}");
        Step("create-after-external", root.CreateControl, root, child);
        Step("read-after-external", () => _ = root.Handle, root, child);
        Step("dispose", root.Dispose, root, child);
    }

    private static void Mapping()
    {
        using var root = NewControl("root");
        using var child = NewControl("child");
        root.Controls.Add(child);
        root.CreateControl();
        nint rootHandle = ExistingHandle(root), childHandle = ExistingHandle(child);
        nint nativeChild = CreateWindowExW(0, "STATIC", "raw child", 0x40000000, 0, 0, 10, 10, childHandle, 0, 0, 0);
        if (nativeChild == 0) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        nint nativeGrandchild = CreateWindowExW(0, "STATIC", "raw grandchild", 0x40000000, 0, 0, 5, 5, nativeChild, 0, 0, 0);
        if (nativeGrandchild == 0) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        nint foreign = CreateWindowExW(0, "STATIC", "foreign", 0x80000000, 0, 0, 10, 10, 0, 0, 0, 0);
        if (foreign == 0) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        try
        {
            RecordLookup("zero", 0);
            RecordLookup("invalid", -1);
            RecordLookup("root", rootHandle);
            RecordLookup("child", childHandle);
            RecordLookup("native-child", nativeChild);
            RecordLookup("native-grandchild", nativeGrandchild);
            RecordLookup("foreign", foreign);
            Step("destroy-mapped-child", child.DirectDestroy, root, child);
            RecordLookup("destroyed-child", childHandle);
            RecordLookup("destroyed-native-child", nativeChild);
            RecordLookup("destroyed-native-grandchild", nativeGrandchild);
        }
        finally { _ = DestroyWindow(foreign); }
    }

    private static void Reparent()
    {
        using var form = NewForm("form");
        using var source = NewControl("source");
        using var destination = NewControl("destination");
        using var child = NewControl("child");
        form.Controls.AddRange([source, destination]);
        source.Controls.Add(child);
        Step("show", () => { form.Show(); Pump(); }, form, source, destination, child);
        nint childHandle = ExistingHandle(child);
        Step("hide-source", () => source.Visible = false, form, source, destination, child);
        Step("move-child-to-visible", () => child.Parent = destination, form, source, destination, child);
        Record("reparent-relations", $"same-handle:{ExistingHandle(child) == childHandle}|valid:{IsWindow(childHandle)}|parent:{NativeParentIs(child, destination)}|managed-visible:{child.Visible}|native-visible:{IsWindowVisible(childHandle)}");
        Step("close", form.Close, form, source, destination, child);
    }

    private static void Posting()
    {
        using var root = NewControl("root");
        using var child = NewControl("child");
        root.Controls.Add(child);
        root.CreateControl();
        int executed = 0;
        IAsyncResult? before = null, after = null, childResult = null;
        Step("queue-before-recreate", () => before = root.BeginInvoke(new Func<int>(() => { executed++; Events.Add("callback:before"); return 41; })), root, child);
        Record("queued-completed", before?.IsCompleted);
        Step("recreate-with-pending", root.Rebuild, root, child);
        Step("queue-after-recreate", () => after = root.BeginInvoke(new Func<int>(() => { executed++; Events.Add("callback:after"); return 43; })), root, child);
        Step("child-queue", () => childResult = child.BeginInvoke(new Func<int>(() => { executed++; Events.Add("callback:child"); return 47; })), root, child);
        Step("pump", () => PumpUntil(() => before?.IsCompleted == true && after?.IsCompleted == true && childResult?.IsCompleted == true), root, child);
        Record("posting-completion", $"before:{before?.IsCompleted}|after:{after?.IsCompleted}|child:{childResult?.IsCompleted}|executed:{executed}|quit:{_quitObserved}");
        if (before?.IsCompleted == true) Step("end-before", () => Record("before-result", root.EndInvoke(before)), root);
        else Record("end-before", "not-run-incomplete");
        if (after?.IsCompleted == true) Step("end-after", () => Record("after-result", root.EndInvoke(after)), root);
        else Record("end-after", "not-run-incomplete");
        if (childResult?.IsCompleted == true) Step("end-child", () => Record("child-result", child.EndInvoke(childResult)), child);
        else Record("end-child", "not-run-incomplete");
        Step("dispose", root.Dispose, root, child);
    }

    private static void FormLazy()
    {
        using var form = NewForm("form");
        using var child = NewControl("child");
        form.Controls.Add(child);
        Step("initial", () => { }, form, child);
        Step("create-hidden-form", form.CreateControl, form, child);
        Step("form-handle", () => _ = form.Handle, form, child);
        Step("create-hidden-form-with-handle", form.CreateControl, form, child);
        Step("show-form", () => { form.Show(); Pump(); }, form, child);
        using var late = NewControl("late");
        Step("add-visible-child-to-shown-form", () => form.Controls.Add(late), form, child, late);
        Step("show-again", () => { form.Show(); Pump(); }, form, child);
        Step("hide-form", form.Hide, form, child);
        Step("show-after-hide", () => { form.Show(); Pump(); }, form, child);
        Step("close", form.Close, form, child, late);
    }

    private static void FormRecreate()
    {
        using var form = NewForm("form");
        using var child = NewControl("child");
        form.Controls.Add(child);
        Step("show", () => { form.Show(); Pump(); }, form, child);
        nint oldForm = ExistingHandle(form), oldChild = ExistingHandle(child);
        IAsyncResult? pending = null;
        Step("queue", () => pending = form.BeginInvoke(new Func<int>(() => { Events.Add("callback:form"); return 53; })), form, child);
        Step("recreate-form", form.Rebuild, form, child);
        Record("form-recreate-relations", $"old-form-valid:{IsWindow(oldForm)}|child-same:{ExistingHandle(child) == oldChild}|child-parent:{NativeParentIs(child, form)}|form-native-visible:{IsWindowVisible(ExistingHandle(form))}");
        Step("pump", () => PumpUntil(() => pending?.IsCompleted == true), form, child);
        Record("form-pending", $"completed:{pending?.IsCompleted}|quit:{_quitObserved}");
        if (pending?.IsCompleted == true) Step("end", () => Record("form-result", form.EndInvoke(pending)), form, child);
        else Record("end", "not-run-incomplete");
        Step("close", form.Close, form, child);
    }

    private static void FormDestroy()
    {
        using var form = NewForm("form");
        using var child = NewControl("child");
        form.Controls.Add(child);
        Step("show", () => { form.Show(); Pump(); }, form, child);
        nint oldForm = ExistingHandle(form), oldChild = ExistingHandle(child);
        Step("destroy-form-handle", form.DirectDestroy, form, child);
        Record("form-destroy-relations", $"form-valid:{IsWindow(oldForm)}|child-valid:{IsWindow(oldChild)}|form-map:{Id(Control.FromHandle(oldForm))}");
        Step("recreate-control", form.CreateControl, form, child);
        Step("show-after-destroy", () => { form.Show(); Pump(); }, form, child);
        Step("close", form.Close, form, child);
    }

    private static void FormExternalDestroy()
    {
        using var form = NewForm("form");
        using var child = NewControl("child");
        form.Controls.Add(child);
        Step("show", () => { form.Show(); Pump(); }, form, child);
        nint oldForm = ExistingHandle(form), oldChild = ExistingHandle(child);
        Step("external-destroy-form", () => Record("destroy-return", DestroyWindow(oldForm)), form, child);
        Record("form-external-relations", $"form-valid:{IsWindow(oldForm)}|child-valid:{IsWindow(oldChild)}|form-map:{Id(Control.FromHandle(oldForm))}");
        Step("form-handle-after-external", () => _ = form.Handle, form, child);
        Step("show-after-external", () => { form.Show(); Pump(); }, form, child);
        Step("close", form.Close, form, child);
    }

    private static void FormCloseUncreated()
    {
        using var form = NewForm("form");
        using var child = NewControl("child");
        form.Controls.Add(child);
        Step("close-uncreated", form.Close, form, child);
        Step("close-again", form.Close, form, child);
    }

    private static void ExceptionsAndReentrancy()
    {
        // Native callbacks may report a user exception through ThreadException.
        // Record that route, including exception identity, without opening UI.
        Application.ThreadException += RecordThreadException;
        try
        {
            using (var control = NewPolicyControl("skip-create"))
            {
                control.SkipCreate = true;
                Step("override-skip-create", control.CreateControl, control);
                Step("override-skip-create-handle", () => Record("returned-zero", control.Handle == 0), control);
                control.SkipCreate = false;
                Step("override-create-restored", control.CreateControl, control);
                Step("override-handle-restored", () => _ = control.Handle, control);
                Step("override-create-dispose", control.Dispose, control);
            }
            using (var control = NewPolicyControl("skip-hooks"))
            {
                control.SkipCreated = true;
                control.SkipDestroyed = true;
                Step("override-skip-created-hook", control.CreateControl, control);
                nint oldHandle = ExistingHandle(control);
                Step("override-skip-destroyed-hook", control.DirectDestroy, control);
                Record("skip-hook-old-map", Id(Control.FromHandle(oldHandle)));
            }
            using (var control = NewPolicyControl("skip-destroy"))
            {
                control.CreateControl();
                control.SkipDestroy = true;
                Step("override-skip-destroy", control.DirectDestroy, control);
                control.SkipDestroy = false;
                Step("override-destroy-restored", control.DirectDestroy, control);
            }
            using (var control = NewPolicyControl("throw-create"))
            {
                control.ThrowCreate = true;
                Step("override-create-throws", control.CreateControl, control);
                control.ThrowCreate = false;
                Step("override-create-retry", control.CreateControl, control);
                Step("override-create-retry-dispose", control.Dispose, control);
            }
            using (var control = NewPolicyControl("throw-on-create"))
            {
                control.ThrowOnCreateControl = true;
                Step("override-on-create-throws", control.CreateControl, control);
                control.ThrowOnCreateControl = false;
                Step("override-on-create-retry", control.CreateControl, control);
                Step("override-on-create-dispose", control.Dispose, control);
            }
            using (var control = NewPolicyControl("throw-event"))
            {
                EventHandler throwing = (_, _) => throw CallbackFailure;
                control.HandleCreated += throwing;
                Step("handle-created-event-throws", control.CreateControl, control);
                control.HandleCreated -= throwing;
                Step("handle-created-event-retry", control.CreateControl, control);
                Step("handle-created-event-dispose", control.Dispose, control);
            }
            using (var control = NewPolicyControl("reentrant"))
            {
                bool entered = false;
                control.HandleCreated += (_, _) =>
                {
                    if (entered) { Events.Add("callback-reentered"); return; }
                    entered = true;
                    nint existing = ExistingHandle(control);
                    Events.Add("callback-handle-same:" + (control.Handle == existing));
                    ObserveCallback("callback-create-control", control.CreateControl);
                    ObserveCallback("callback-create-handle", control.DirectCreate);
                    ObserveCallback("callback-dispose", control.Dispose);
                };
                Step("creation-callback-reentrancy", () => _ = control.Handle, control);
                Step("creation-callback-dispose-after", control.Dispose, control);
            }
            foreach (bool createFirst in new[] { false, true })
            {
                using var control = NewPolicyControl(createFirst ? "disposed-created" : "disposed-uncreated");
                if (createFirst) control.CreateControl();
                Step("disposed-" + createFirst, control.Dispose, control);
                Step("handle-after-dispose-" + createFirst, () => _ = control.Handle, control);
                Step("create-control-after-dispose-" + createFirst, control.CreateControl, control);
                Step("create-handle-after-dispose-" + createFirst, control.DirectCreate, control);
                Step("recreate-after-dispose-" + createFirst, control.Rebuild, control);
            }
            DisposeWithDestroyOverride("skip-base");
            DisposeWithDestroyOverride("throw-once");
            DisposeWithDestroyOverride("throw-always");
        }
        finally { Application.ThreadException -= RecordThreadException; }
    }

    private static void DisposeWithDestroyOverride(string mode)
    {
        using var container = new System.ComponentModel.Container();
        using var control = NewPolicyControl("dispose-" + mode);
        using var child = NewControl("dispose-" + mode + "-child");
        control.Controls.Add(child);
        container.Add(control, "sited-parent");
        container.Add(child, "sited-child");
        Control.ControlCollection retainedChildren = control.Controls;
        int disposedEvents = 0, childDisposedEvents = 0;
        control.Disposed += (_, _) => disposedEvents++;
        child.Disposed += (_, _) => childDisposedEvents++;
        control.CreateControl();
        nint oldHandle = ExistingHandle(control), oldChildHandle = ExistingHandle(child);
        int initialDestroyCalls = control.DestroyCalls;
        control.SkipDestroy = mode == "skip-base";
        control.ThrowDestroyRemaining = mode == "throw-once" ? 1 : 0;
        control.ThrowDestroyAlways = mode == "throw-always";
        try
        {
            Step("dispose-override-" + mode, control.Dispose, control, child);
            Record("dispose-override-observations-" + mode,
                $"destroy-calls:{control.DestroyCalls - initialDestroyCalls}|disposed-events:{disposedEvents}|child-disposed-events:{childDisposedEvents}|site:{control.Site is not null}|child-site:{child.Site is not null}|container-count:{container.Components.Count}|children:{control.Controls.Count}|retained-children:{retainedChildren.Count}|child-parent:{Id(child.Parent)}|old-window:{IsWindow(oldHandle)}|old-map:{Id(Control.FromHandle(oldHandle))}|old-child-window:{IsWindow(oldChildHandle)}|old-child-map:{Id(Control.FromHandle(oldChildHandle))}");
        }
        finally
        {
            // A derived DestroyHandle may intentionally leave a real HWND alive.
            // Restore the implementation before cleanup and observe real disposal.
            control.SkipDestroy = false;
            control.ThrowDestroyRemaining = 0;
            control.ThrowDestroyAlways = false;
            Step("dispose-override-cleanup-destroy-" + mode, control.DirectDestroy, control, child);
            Step("dispose-override-cleanup-dispose-" + mode, () => { control.Dispose(); child.Dispose(); }, control, child);
            Record("dispose-override-cleanup-windows-" + mode, $"parent:{IsWindow(oldHandle)}|child:{IsWindow(oldChildHandle)}");
            if (IsWindow(oldHandle) || IsWindow(oldChildHandle))
                throw new InvalidOperationException("Lifecycle fixture cleanup left a native window alive: " + mode);
        }
    }

    private static PolicyControl NewPolicyControl(string name)
    {
        var control = new PolicyControl { Name = name };
        Watch(control);
        return control;
    }

    private static void RecordThreadException(object? sender, ThreadExceptionEventArgs e)
        => Events.Add("thread-exception:" + ExceptionText(e.Exception));

    private static void ObserveCallback(string name, Action action)
    {
        Exception? failure = null;
        try { action(); }
        catch (Exception exception) { failure = exception; }
        Events.Add(name + ":" + ExceptionText(failure));
    }

    private static TraceControl NewControl(string name)
    {
        var control = new TraceControl { Name = name };
        Watch(control);
        return control;
    }

    private static TraceForm NewForm(string name)
    {
        var form = new TraceForm { Name = name, Text = "Lifecycle probe", StartPosition = FormStartPosition.Manual, Bounds = new Rectangle(25, 25, 250, 150) };
        Watch(form);
        form.Load += (_, _) => Trace("load", form);
        form.Shown += (_, _) => Trace("shown", form);
        form.FormClosing += (_, _) => Trace("form-closing", form);
#pragma warning disable CS0618 // Closed is deliberately observed on both existing surfaces.
        form.Closed += (_, _) => Trace("closed", form);
#pragma warning restore CS0618
        return form;
    }

    private static void Watch(Control control)
    {
        control.HandleCreated += (_, _) => Trace("event-created", control);
        control.HandleDestroyed += (_, _) => Trace("event-destroyed", control);
        control.VisibleChanged += (_, _) => Trace("event-visible", control);
        control.Disposed += (_, _) => Trace("event-disposed", control);
    }

    private static void Step(string name, Action action, params Control[] controls)
    {
        Events.Clear();
        Exception? failure = null;
        try { action(); }
        catch (Exception exception) { failure = exception; }
        Record(name, $"exception:{ExceptionText(failure)}|events:[{string.Join(";", Events)}]|state:[{string.Join(";", controls.Select(State))}]");
    }

    private static string State(Control control)
    {
        nint handle = ExistingHandle(control);
        return $"{Id(control)}(created:{control.Created},handle:{control.IsHandleCreated},recreating:{control.RecreatingHandle},visible:{control.Visible},disposed:{control.IsDisposed},disposing:{control.Disposing},native:{IsWindow(handle)},native-visible:{IsWindowVisible(handle)},map:{Id(Control.FromHandle(handle))})";
    }

    private static string ExceptionText(Exception? exception) => exception is null ? "none" :
        ReferenceEquals(exception, CallbackFailure) ? "original" : exception.GetType().Name + (exception is ArgumentException argument ? ":" + argument.ParamName : "");

    private static void Trace(string name, Control control) => Events.Add(name + ":" + State(control));
    private static nint ExistingHandle(Control control) => control.IsHandleCreated ? control.Handle : 0;
    private static bool NativeParentIs(Control child, Control parent) => child.IsHandleCreated && parent.IsHandleCreated && GetParent(ExistingHandle(child)) == ExistingHandle(parent);
    private static string Id(Control? control) => control is null ? "null" : control.Name.Length == 0 ? "unnamed" : control.Name;
    private static string Rect(Rectangle value) => $"{value.X},{value.Y},{value.Width},{value.Height}";
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + (value?.ToString() ?? "null"));
    private static void RecordLookup(string name, nint handle) => Record("lookup-" + name, $"direct:{Id(Control.FromHandle(handle))}|ancestor:{Id(Control.FromChildHandle(handle))}|valid:{IsWindow(handle)}");

    // Identical native pumping keeps this probe independent of unfinished public
    // Application.DoEvents coverage. WM_QUIT remains an explicit observation.
    private static void Pump()
    {
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (message.Message == 0x0012) { _quitObserved = true; continue; }
            _ = TranslateMessage(in message);
            _ = DispatchMessageW(in message);
        }
    }

    private static void PumpUntil(Func<bool> completed)
    {
        long deadline = Environment.TickCount64 + 2000;
        do { Pump(); if (completed()) return; Thread.Sleep(1); } while (Environment.TickCount64 < deadline);
    }

    private sealed class TraceControl : Control
    {
        internal bool IsRecreating => RecreatingHandle;
        internal void DirectCreate() => CreateHandle();
        internal void DirectDestroy() => DestroyHandle();
        internal void Rebuild() => RecreateHandle();
        protected override void CreateHandle() { Trace("create-enter", this); base.CreateHandle(); Trace("create-exit", this); }
        protected override void DestroyHandle() { Trace("destroy-enter", this); base.DestroyHandle(); Trace("destroy-exit", this); }
        protected override void OnCreateControl() { Trace("on-create-enter", this); base.OnCreateControl(); Trace("on-create-exit", this); }
        protected override void OnHandleCreated(EventArgs e) { Trace("on-created-enter", this); base.OnHandleCreated(e); Trace("on-created-exit", this); }
        protected override void OnHandleDestroyed(EventArgs e) { Trace("on-destroyed-enter", this); base.OnHandleDestroyed(e); Trace("on-destroyed-exit", this); }
    }

    private sealed class TraceForm : Form
    {
        internal bool IsRecreating => RecreatingHandle;
        internal void DirectDestroy() => DestroyHandle();
        internal void Rebuild() => RecreateHandle();
        protected override void CreateHandle() { Trace("create-enter", this); base.CreateHandle(); Trace("create-exit", this); }
        protected override void DestroyHandle() { Trace("destroy-enter", this); base.DestroyHandle(); Trace("destroy-exit", this); }
        protected override void OnCreateControl() { Trace("on-create-enter", this); base.OnCreateControl(); Trace("on-create-exit", this); }
        protected override void OnHandleCreated(EventArgs e) { Trace("on-created-enter", this); base.OnHandleCreated(e); Trace("on-created-exit", this); }
        protected override void OnHandleDestroyed(EventArgs e) { Trace("on-destroyed-enter", this); base.OnHandleDestroyed(e); Trace("on-destroyed-exit", this); }
    }

    private sealed class PolicyControl : Control
    {
        internal bool SkipCreate;
        internal bool SkipDestroy;
        internal bool SkipCreated;
        internal bool SkipDestroyed;
        internal bool ThrowCreate;
        internal bool ThrowOnCreateControl;
        internal bool ThrowDestroyAlways;
        internal int ThrowDestroyRemaining;
        internal int DestroyCalls;
        internal void DirectCreate() => CreateHandle();
        internal void DirectDestroy() => DestroyHandle();
        internal void Rebuild() => RecreateHandle();
        protected override void CreateHandle()
        {
            Trace("create-enter", this);
            if (ThrowCreate) throw CallbackFailure;
            if (!SkipCreate) base.CreateHandle();
            Trace("create-exit", this);
        }
        protected override void DestroyHandle()
        {
            DestroyCalls++;
            Trace("destroy-enter", this);
            if (ThrowDestroyAlways || ThrowDestroyRemaining > 0)
            {
                if (ThrowDestroyRemaining > 0) ThrowDestroyRemaining--;
                throw CallbackFailure;
            }
            if (!SkipDestroy) base.DestroyHandle();
            Trace("destroy-exit", this);
        }
        protected override void OnCreateControl()
        {
            Trace("on-create-enter", this);
            if (ThrowOnCreateControl) throw CallbackFailure;
            base.OnCreateControl();
            Trace("on-create-exit", this);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            Trace("on-created-enter", this);
            if (!SkipCreated) base.OnHandleCreated(e);
            Trace("on-created-exit", this);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            Trace("on-destroyed-enter", this);
            if (!SkipDestroyed) base.OnHandleDestroyed(e);
            Trace("on-destroyed-exit", this);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage { public nint HWnd; public uint Message; public nuint WParam; public nint LParam; public uint Time; public Point Point; public uint Private; }
    [DllImport("user32.dll")] private static extern nint GetParent(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindowVisible(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindowEnabled(nint window);
    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool DestroyWindow(nint window);
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern nint CreateWindowExW(uint exStyle, string className, string title, uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool PeekMessageW(out NativeMessage message, nint window, uint min, uint max, uint remove);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")] private static extern nint DispatchMessageW(in NativeMessage message);
}
