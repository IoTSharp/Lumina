using System.ComponentModel;
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

// Identical source/scenarios for WinForms 10.0.11, Lumina JIT and Native AOT.
// stdout is compared verbatim. Programmatic focus is not physical input.
internal static class Program
{
    private static readonly List<Control> Observed = [];
    private static readonly List<string> Trace = [];
    private static readonly Exception Failure = new InvalidOperationException("control028 focus callback failure");
    private static readonly string[] EventNames = ["GotFocus", "LostFocus", "Enter", "Leave"];
    private static bool _recording;
    private static EventArgs? _expectedArgs;

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
        Application.ThreadException += (_, e) =>
        {
            Emit("thread-exception:" + ExceptionText(e.Exception));
            Console.Error.WriteLine(e.Exception);
        };
        if (args.Length != 1) throw new ArgumentException("Specify one Control028 scenario.");
        Record("scenario", args[0]);
        Record("input-evidence", "managed-api-and-programmatic-native-focus;physical-device-not-verified");
        switch (args[0])
        {
            case "canselect-uncreated": CanSelect(false); break;
            case "canselect-created": CanSelect(true); break;
            case "container-lookup": ContainerLookup(); break;
            case "getnext-order": GetNextOrder(); break;
            case "getnext-nested": GetNextNested(); break;
            case "select-uncreated": SelectUncreated(); break;
            case "select-hooks": SelectHooks(); break;
            case "select-filters": SelectFilters(); break;
            case "selectnext-matrix": SelectNextMatrix(); break;
            case "selectnext-empty": SelectNextEmpty(); break;
            case "direct-events": Direct("normal"); break;
            case "direct-null": Direct("null"); break;
            case "direct-skip": Direct("skip"); break;
            case "direct-exception": Direct("exception"); break;
            case "direct-reentrant": DirectReentrant(); break;
            case "direct-helpers": DirectHelpers(); break;
            case "active-public": ActivePublic(); break;
            case "active-interface": ActiveInterface(); break;
            case "active-reentrant": ActiveReentrant(); break;
            case "container-select": ContainerSelect(); break;
            case "form-select-empty": FormSelect(false); break;
            case "form-select": FormSelect(true); break;
            case "native-focus": NativeFocus(); break;
            case "native-nested": NativeNested(); break;
            case "native-container-empty": NativeContainer("empty"); break;
            case "native-container-active": NativeContainer("active"); break;
            case "native-container-hidden": NativeContainer("hidden"); break;
            case "native-reentrant": NativeReentrant(); break;
            case "raw-focus-messages": RawFocusMessages(); break;
            case "consumer-textbox": ConsumerTextBox(); break;
            default: throw new ArgumentException("Unknown scenario " + args[0]);
        }
        Record("complete", true);
    }

    private static void CanSelect(bool created)
    {
        using ProbeForm form = NewForm();
        ProbeControl parent = New("parent"), child = New("child");
        parent.Controls.Add(child); form.Controls.Add(parent);
        using ProbeControl detached = New("detached");
        Snapshot("initial");
        if (created) { Show(form); Snapshot("shown"); }
        Step("tabstop-false", () => child.TabStop = false);
        Step("selectable-false", () => child.Style(ControlStyles.Selectable, false));
        Step("selectable-true", () => child.Style(ControlStyles.Selectable, true));
        Step("parent-disabled", () => parent.Enabled = false);
        Step("parent-reenabled", () => parent.Enabled = true);
        Step("parent-hidden", () => parent.Visible = false);
        Step("parent-reshown", () => parent.Visible = true);
        Step("child-disabled", () => child.Enabled = false);
        Step("child-reenabled", () => child.Enabled = true);
        Step("child-hidden", () => child.Visible = false);
        Step("child-reshown", () => child.Visible = true);
        Step("detached-dispose", detached.Dispose);
        Step("child-dispose", child.Dispose);
        Close(form);
    }

    private static void ContainerLookup()
    {
        using ProbeControl detached = New("detached");
        using ProbeContainer root = NewContainer("root");
        ProbeControl plain = New("plain"), leaf = New("leaf");
        ProbeContainer nested = NewContainer("nested");
        InterfaceContainer custom = Observe(new InterfaceContainer { Name = "custom" });
        ProbeControl customLeaf = New("custom-leaf");
        root.Controls.Add(plain); plain.Controls.Add(nested); nested.Controls.Add(leaf);
        root.Controls.Add(custom); custom.Controls.Add(customLeaf);
        Step("lookup-default", LookupAll);
        Step("custom-container-style", () => { custom.Style(true); LookupAll(); });
        Step("custom-clear-style", () => { custom.Style(false); LookupAll(); });
        Step("plain-container-style-no-interface", () => { plain.Style(ControlStyles.ContainerControl, true); LookupAll(); });
        using SplitContainer split = Observe(new SplitContainer { Name = "split" });
        root.Controls.Add(split);
        Step("split-container-lookup", () => Record("split-owner", Name(split.GetContainerControl())));
        void LookupAll()
        {
            foreach (Control c in Observed) Record(c.Name + ".container", Name(c.GetContainerControl()));
        }
    }

    private static void GetNextOrder()
    {
        using ProbeContainer root = NewContainer("root");
        ProbeControl a = New("a", 2), b = New("b", 0), c = New("c", 2), d = New("d", 1);
        root.Controls.AddRange([a, b, c, d]);
        using ProbeControl outside = New("outside");
        Step("initial-order", Query);
        Step("duplicate-reorder", () => { root.Controls.SetChildIndex(c, 0); Query(); });
        Step("hidden-disabled-still-traversed", () => { b.Visible = false; c.Enabled = false; d.TabStop = false; Query(); });
        void Query()
        {
            Record("collection", string.Join(",", root.Controls.Cast<Control>().Select(c => c.Name)));
            foreach (bool forward in new[] { true, false })
                foreach (Control? start in new Control?[] { null, root, a, b, c, d, outside })
                    Record("next:" + Name(start) + ":" + forward, Name(root.GetNextControl(start, forward)));
        }
    }

    private static void GetNextNested()
    {
        using Tree t = new();
        Step("nested-navigation", () =>
        {
            foreach (bool forward in new[] { true, false })
            {
                foreach (Control? start in t.Starts)
                    Record("next:" + Name(start) + ":" + forward, Name(t.Root.GetNextControl(start, forward)));
                Control? current = null;
                for (int i = 0; i < 20; i++)
                {
                    current = t.Root.GetNextControl(current, forward);
                    Record("walk:" + forward + ":" + i, Name(current));
                    if (current is null) break;
                }
            }
        });
    }

    private static void SelectUncreated()
    {
        using ProbeControl detached = New("detached");
        Step("detached-select", detached.Select);
        using Tree t = new();
        Step("leaf-select", t.Inner.Select);
        Step("leaf-select-again", t.Inner.Select);
        Step("other-select", t.Last.Select);
        Step("nested-select", t.Nested.Select);
        Step("root-select", t.Root.Select);
        Step("protected-backward", () => t.First.SelectDirected(true, false));
    }

    private static void SelectHooks()
    {
        using ProbeContainer root = NewContainer("root");
        ProbeControl c = New("target"); root.Controls.Add(c);
        Step("public-default-dispatch", c.Select);
        foreach (bool directed in new[] { false, true })
            foreach (bool forward in new[] { false, true })
                Step("protected:" + directed + ":" + forward, () => c.SelectDirected(directed, forward));
        root.ActiveControl = null; c.SkipSelectBase = true;
        Step("override-without-base", c.Select);
        c.SkipSelectBase = false; c.ThrowSelect = true;
        Step("override-throws-original", c.Select);
    }

    private static void SelectFilters()
    {
        foreach (string filter in new[] { "disabled", "hidden", "tabstop", "style", "disposed" })
        {
            using ProbeContainer root = NewContainer("root-" + filter);
            ProbeControl c = New("target-" + filter); root.Controls.Add(c);
            switch (filter)
            {
                case "disabled": c.Enabled = false; break;
                case "hidden": c.Visible = false; break;
                case "tabstop": c.TabStop = false; break;
                case "style": c.Style(ControlStyles.Selectable, false); break;
                case "disposed": c.Dispose(); break;
            }
            Step("select-" + filter, c.Select);
        }
    }

    private static void SelectNextMatrix()
    {
        using Tree t = new();
        t.First.Enabled = false; t.Plain.TabStop = false; t.Nested.TabStop = false;
        t.Hidden.Visible = false; t.NoStop.TabStop = false;
        foreach (bool forward in new[] { true, false })
            foreach (bool tabStopOnly in new[] { true, false })
                foreach (bool nested in new[] { true, false })
                    foreach (bool wrap in new[] { false, true })
                        foreach (Control? start in t.Starts)
                        {
                            t.Root.ActiveControl = null;
                            Step("next:" + Name(start) + ":f=" + forward + ":tab=" + tabStopOnly + ":nested=" + nested + ":wrap=" + wrap,
                                () => Record("selected", t.Root.SelectNextControl(start, forward, tabStopOnly, nested, wrap)));
                        }
    }

    private static void SelectNextEmpty()
    {
        using ProbeContainer root = NewContainer("root");
        Step("empty", () => Record("selected", root.SelectNextControl(null, true, true, true, true)));
        ProbeControl only = New("only"); root.Controls.Add(only);
        Step("only-wrap-self", () => Record("selected", root.SelectNextControl(only, true, true, true, true)));
        Step("only-no-wrap", () => Record("selected", root.SelectNextControl(only, true, true, true, false)));
        only.Enabled = false;
        Step("none-selectable", () => Record("selected", root.SelectNextControl(null, false, false, true, true)));
        only.Enabled = true; only.SkipSelectBase = true;
        Step("selected-override-skips-base", () => Record("selected", root.SelectNextControl(null, true, true, true, false)));
        only.ThrowSelect = true;
        Step("selected-override-throws", () => Record("selected", root.SelectNextControl(null, true, true, true, false)));
    }

    private static void Direct(string mode)
    {
        foreach (string eventName in EventNames)
        {
            using ProbeControl c = New("target-" + eventName);
            c.SkipEventBase = mode == "skip";
            c.FailingEvent = mode == "exception" ? eventName : null;
            _expectedArgs = mode == "null" ? null : new EventArgs();
            Step("raise-" + eventName, () => c.Raise(eventName, _expectedArgs!));
        }
        _expectedArgs = null;
    }

    private static void DirectReentrant()
    {
        using ProbeControl c = New("target");
        bool entered = false;
        c.GotFocus += (_, _) => { if (!entered) { entered = true; c.Raise("LostFocus", EventArgs.Empty); } };
        Step("nested-lost-during-got", () => c.Raise("GotFocus", EventArgs.Empty));
        c.Leave += (_, _) => c.Dispose();
        Step("dispose-during-leave", () => c.Raise("Leave", EventArgs.Empty));
        Step("got-after-dispose", () => c.Raise("GotFocus", EventArgs.Empty));
    }

    private static void DirectHelpers()
    {
        using ProbeControl caller = New("caller"), target = New("target");
        foreach (bool got in new[] { true, false })
        {
            _expectedArgs = new EventArgs();
            Step("helper-null-target:" + got, () => caller.CallFocus(got, null, _expectedArgs));
            Step("helper-other-target:" + got, () => caller.CallFocus(got, target, _expectedArgs));
            _expectedArgs = null;
            Step("helper-null-args:" + got, () => caller.CallFocus(got, target, null!));
            target.FailingEvent = got ? "GotFocus" : "LostFocus";
            Step("helper-original-exception:" + got, () => caller.CallFocus(got, target, EventArgs.Empty));
            target.FailingEvent = null;
        }
    }

    private static void ActivePublic()
    {
        using Tree t = new();
        Step("nested-child", () => t.Root.ActiveControl = t.Inner);
        Step("nested-sibling", () => t.Root.ActiveControl = t.InnerLast);
        Step("plain-descendant", () => t.Root.ActiveControl = t.PlainChild);
        Step("nested-container", () => t.Root.ActiveControl = t.Nested);
        Step("clear", () => t.Root.ActiveControl = null);
        Step("outside", () => t.Root.ActiveControl = t.Outside);
        Step("self", () => t.Root.ActiveControl = t.Root);
    }

    private static void ActiveInterface()
    {
        using Tree t = new();
        IContainerControl cc = t.Root;
        foreach (Control? target in new Control?[] { t.Inner, t.Last, t.Outside, t.Root, null })
            Step("activate:" + Name(target), () => Record("activated", cc.ActivateControl(target!)));
    }

    private static void ActiveReentrant()
    {
        using Tree t = new();
        bool entered = false;
        t.Inner.Enter += (_, _) =>
        {
            Emit("reenter-selection-before:" + Activity());
            if (!entered) { entered = true; t.Root.ActiveControl = t.Last; }
            Emit("reenter-selection-after:" + Activity());
        };
        Step("enter-redirects", () => t.Root.ActiveControl = t.Inner);
        bool left = false;
        t.Last.Leave += (_, _) => { if (!left) { left = true; t.Root.ActiveControl = t.PlainChild; } };
        Step("leave-redirects", () => t.Root.ActiveControl = t.First);
    }

    private static void ContainerSelect()
    {
        using Tree t = new();
        foreach (bool directed in new[] { false, true })
            foreach (bool forward in new[] { false, true })
            {
                t.Root.ActiveControl = null;
                Step("empty:" + directed + ":" + forward, () => t.Nested.SelectDirected(directed, forward));
                t.Nested.ActiveControl = t.InnerLast;
                Step("active:" + directed + ":" + forward, () => t.Nested.SelectDirected(directed, forward));
            }
    }

    private static void FormSelect(bool children)
    {
        using ProbeForm form = NewForm();
        if (children) form.Controls.AddRange([New("first", 0), New("last", 1)]);
        Snapshot("before");
        Step("public-select-uncreated", form.Select);
        Step("directed-forward", () => form.SelectDirected(true, true));
        Step("directed-backward", () => form.SelectDirected(true, false));
        Show(form);
        Step("public-select-shown", form.Select);
        Step("shown-forward", () => form.SelectDirected(true, true));
        Step("shown-backward", () => form.SelectDirected(true, false));
        Close(form);
    }

    private static void NativeFocus()
    {
        using ProbeForm form = NewForm();
        ProbeControl a = New("a"), b = New("b", 1);
        form.Controls.AddRange([a, b]); Show(form);
        Step("focus-a", () => Record("focus-return", a.Focus()));
        Step("focus-a-again", () => Record("focus-return", a.Focus()));
        Step("focus-b", () => Record("focus-return", b.Focus()));
        Step("native-a", () => SetFocus(a.Handle));
        Step("native-clear", () => SetFocus(0));
        Step("select-b", b.Select);
        Step("focus-disabled", () => { b.Enabled = false; Record("focus-return", b.Focus()); });
        Step("focus-hidden", () => { a.Visible = false; Record("focus-return", a.Focus()); });
        Close(form);
    }

    private static void NativeNested()
    {
        using ProbeForm form = NewForm();
        ProbeContainer nested = NewContainer("nested");
        ProbeControl a = New("a"), b = New("b", 1), outside = New("outside", 1);
        nested.Controls.AddRange([a, b]); form.Controls.AddRange([nested, outside]); Show(form);
        Step("focus-nested-a", () => Record("focus-return", a.Focus()));
        Step("native-nested-b", () => SetFocus(b.Handle));
        Step("select-outside", outside.Select);
        Step("select-nested-a", a.Select);
        Step("active-nested-b", () => form.ActiveControl = b);
        Step("native-form-restore", () => SetFocus(form.Handle));
        Step("clear-focus", () => SetFocus(0));
        Close(form);
    }

    private static void NativeContainer(string mode)
    {
        using ProbeForm form = NewForm();
        ProbeContainer nested = NewContainer("nested");
        ProbeControl child = New("child"), other = New("other", 1);
        nested.Controls.Add(child); form.Controls.AddRange([nested, other]); Show(form);
        other.Focus();
        nested.ActiveControl = mode == "empty" ? null : child;
        if (mode == "hidden") child.Visible = false;
        SetFocus(0);
        Step("native-container:" + mode, () => SetFocus(nested.Handle));
        Step("native-container-repeat:" + mode, () => SetFocus(nested.Handle));
        Close(form);
    }

    private static void NativeReentrant()
    {
        using ProbeForm form = NewForm();
        ProbeControl a = New("a"), b = New("b", 1); form.Controls.AddRange([a, b]); Show(form);
        bool redirected = false;
        a.GotFocus += (_, _) => { if (!redirected) { redirected = true; Record("nested-focus-return", b.Focus()); } };
        Step("got-redirects-focus", () => Record("focus-return", a.Focus()));
        Step("native-clear-after-redirect", () => SetFocus(0));
        Close(form);
    }

    private static void RawFocusMessages()
    {
        using ProbeForm form = NewForm();
        ProbeControl a = New("a"), b = New("b", 1); form.Controls.AddRange([a, b]); Show(form);
        Step("raw-setfocus", () => a.Dispatch(0x0007, b.Handle));
        Step("raw-setfocus-repeat", () => a.Dispatch(0x0007, b.Handle));
        Step("raw-killfocus", () => a.Dispatch(0x0008, b.Handle));
        Step("raw-killfocus-repeat", () => a.Dispatch(0x0008, 0));
        Step("send-setfocus", () => Record("message-result", SendMessageW(a.Handle, 0x0007, b.Handle, 0)));
        Step("send-killfocus", () => Record("message-result", SendMessageW(a.Handle, 0x0008, b.Handle, 0)));
        Close(form);
    }

    private static void ConsumerTextBox()
    {
        using ProbeForm form = NewForm();
        TextBox text = Observe(new TextBox { Name = "text", Bounds = new Rectangle(10, 10, 120, 30) });
        ProbeControl other = New("other", 1);
        form.Controls.AddRange([text, other]); Show(form);
        Step("textbox-focus", () => Record("focus-return", text.Focus()));
        Step("other-focus", () => Record("focus-return", other.Focus()));
        Step("textbox-select", text.Select);
        Step("other-native", () => SetFocus(other.Handle));
        Step("textbox-native", () => SetFocus(text.Handle));
        Step("native-clear", () => SetFocus(0));
        Close(form);
    }

    private static T Observe<T>(T c) where T : Control
    {
        Observed.Add(c);
        c.GotFocus += (_, e) => Event(c, "GotFocus", e);
        c.LostFocus += (_, e) => Event(c, "LostFocus", e);
        c.Enter += (_, e) => Event(c, "Enter", e);
        c.Leave += (_, e) => Event(c, "Leave", e);
        return c;
    }
    private static ProbeControl New(string name, int tab = 0) => Observe(new ProbeControl { Name = name, TabIndex = tab, Bounds = new Rectangle(10 + tab * 100, 10, 80, 30) });
    private static ProbeContainer NewContainer(string name) => Observe(new ProbeContainer { Name = name, Bounds = new Rectangle(10, 50, 260, 150) });
    private static ProbeForm NewForm() => Observe(new ProbeForm { Name = "form", Text = "Control028 focus probe", ClientSize = new Size(420, 300), StartPosition = FormStartPosition.Manual, Location = new Point(120, 120) });
    private static void Show(Form form)
    {
        Step("show", () => { form.Show(); Pump(); });
        Step("show-clear-programmatic-focus", () => SetFocus(0));
    }

    private static void Close(Form form)
    {
        nint[] handles = Observed.Where(c => c.IsHandleCreated).Select(c => c.Handle).ToArray();
        Step("normal-close", form.Close);
        form.Dispose(); Pump();
        int survivors = handles.Count(IsWindow);
        Record("native-window-survivors", survivors);
        if (survivors != 0) throw new InvalidOperationException("A probe native window survived normal close/disposal.");
    }

    private static void Step(string name, Action action)
    {
        Record("step", name);
        Trace.Clear(); _recording = true;
        try { action(); Record("result", "success"); }
        catch (Exception ex)
        {
            Record("exception", ExceptionText(ex));
            Record("original-exception", ReferenceEquals(ex, Failure));
            Console.Error.WriteLine(ex);
        }
        finally { _recording = false; }
        foreach (string entry in Trace) Record("trace", entry);
        Snapshot("after");
    }

    private static void Snapshot(string label)
    {
        Record(label + ".active", Activity());
        Record(label + ".native-focus", NativeOwner());
        foreach (Control c in Observed)
            Record(label + ".state:" + c.Name, "select=" + c.CanSelect + ";focus=" + c.Focused + ";contains=" + c.ContainsFocus + ";handle=" + c.IsHandleCreated + ";disposed=" + c.IsDisposed);
    }
    private static string Activity() => string.Join(",", Observed.OfType<ContainerControl>().Select(c => c.Name + ":" + Name(c.ActiveControl)));
    private static string NativeOwner()
    {
        nint focus = GetFocus();
        if (focus == 0) return "null";
        return Observed.FirstOrDefault(c => c.IsHandleCreated && c.Handle == focus)?.Name ?? "unmapped-native-window";
    }
    private static string Name(object? c) => c is Control control ? control.Name : c is null ? "null" : c.GetType().FullName!;
    private static string Args(EventArgs? e) => e is null ? "null" : ReferenceEquals(e, EventArgs.Empty) ? "empty" : ReferenceEquals(e, _expectedArgs) ? "supplied" : e.GetType().FullName!;
    private static string ExceptionText(Exception ex) => ex.GetType().FullName + ":" + ex.Message;
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + value);
    private static void Emit(string value) { if (_recording) Trace.Add(value); }
    private static void Hook(Control c, string stage) => Emit(c.Name + ":" + stage + ":active=" + Activity() + ":native=" + NativeOwner());
    private static void Event(Control c, string name, EventArgs? e)
    {
        Hook(c, "event-" + name + ":args=" + Args(e));
        if (c is ProbeControl p && p.FailingEvent == name) throw Failure;
    }

    private sealed class Tree : IDisposable
    {
        internal readonly ProbeContainer Root = NewContainer("root");
        internal readonly ProbeControl First = New("first", 0);
        internal readonly ProbeControl Plain = New("plain", 1);
        internal readonly ProbeControl PlainChild = New("plain-child", 0);
        internal readonly ProbeControl Hidden = New("hidden", 2);
        internal readonly ProbeControl NoStop = New("no-stop", 2);
        internal readonly ProbeContainer Nested = NewContainer("nested");
        internal readonly ProbeControl Inner = New("inner", 0);
        internal readonly ProbeControl InnerLast = New("inner-last", 1);
        internal readonly ProbeControl Last = New("last", 4);
        internal readonly ProbeControl Outside = New("outside");
        internal Control?[] Starts => [null, Root, First, Plain, PlainChild, Hidden, NoStop, Nested, Inner, InnerLast, Last, Outside];
        internal Tree()
        {
            Nested.TabIndex = 3;
            Plain.Controls.Add(PlainChild); Nested.Controls.AddRange([Inner, InnerLast]);
            Root.Controls.AddRange([First, Plain, Hidden, NoStop, Nested, Last]);
        }
        public void Dispose() { Root.Dispose(); Outside.Dispose(); }
    }

    private sealed class ProbeControl : Control
    {
        internal bool SkipSelectBase, ThrowSelect, SkipEventBase;
        internal string? FailingEvent;
        internal void Style(ControlStyles style, bool value) => SetStyle(style, value);
        internal void SelectDirected(bool directed, bool forward) => Select(directed, forward);
        internal void CallFocus(bool got, Control? target, EventArgs e) { if (got) InvokeGotFocus(target, e); else InvokeLostFocus(target, e); }
        internal void Raise(string name, EventArgs e)
        {
            switch (name)
            {
                case "GotFocus": OnGotFocus(e); break;
                case "LostFocus": OnLostFocus(e); break;
                case "Enter": OnEnter(e); break;
                case "Leave": OnLeave(e); break;
                default: throw new ArgumentException(name);
            }
        }
        internal void Dispatch(int msg, nint wparam)
        {
            Message m = Message.Create(Handle, msg, wparam, 0);
            WndProc(ref m);
            Record("message-result", m.Result);
        }
        protected override void Select(bool directed, bool forward)
        {
            Hook(this, "select-before:" + directed + ":" + forward);
            if (ThrowSelect) throw Failure;
            if (!SkipSelectBase) base.Select(directed, forward);
            Hook(this, "select-after");
        }
        protected override void OnGotFocus(EventArgs e) { Hook(this, "got-before:" + Args(e)); if (!SkipEventBase) base.OnGotFocus(e); Hook(this, "got-after"); }
        protected override void OnLostFocus(EventArgs e) { Hook(this, "lost-before:" + Args(e)); if (!SkipEventBase) base.OnLostFocus(e); Hook(this, "lost-after"); }
        protected override void OnEnter(EventArgs e) { Hook(this, "enter-before:" + Args(e)); if (!SkipEventBase) base.OnEnter(e); Hook(this, "enter-after"); }
        protected override void OnLeave(EventArgs e) { Hook(this, "leave-before:" + Args(e)); if (!SkipEventBase) base.OnLeave(e); Hook(this, "leave-after"); }
        protected override void WndProc(ref Message m)
        {
            bool focus = m.Msg is 0x0007 or 0x0008;
            if (focus) Hook(this, "wnd-before:" + m.Msg);
            base.WndProc(ref m);
            if (focus) Hook(this, "wnd-after:" + m.Msg + ":result=" + m.Result);
        }
        protected override void DefWndProc(ref Message m)
        {
            bool focus = m.Msg is 0x0007 or 0x0008;
            if (focus) Hook(this, "def-before:" + m.Msg);
            base.DefWndProc(ref m);
            if (focus) Hook(this, "def-after:" + m.Msg + ":result=" + m.Result);
        }
    }
    private sealed class ProbeContainer : ContainerControl
    {
        internal void SelectDirected(bool directed, bool forward) => Select(directed, forward);
        protected override void Select(bool directed, bool forward)
        {
            Hook(this, "select-before:" + directed + ":" + forward);
            base.Select(directed, forward);
            Hook(this, "select-after");
        }
    }
    private sealed class ProbeForm : Form
    {
        internal void SelectDirected(bool directed, bool forward) => Select(directed, forward);
        protected override void Select(bool directed, bool forward)
        {
            Hook(this, "select-before:" + directed + ":" + forward);
            base.Select(directed, forward);
            Hook(this, "select-after");
        }
    }
    private sealed class InterfaceContainer : Control, IContainerControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control? ActiveControl { get; set; }
        public bool ActivateControl(Control active) { ActiveControl = active; return ReferenceEquals(active, ActiveControl); }
        internal void Style(bool value) => SetStyle(ControlStyles.ContainerControl, value);
    }

    // Keep both targets on the same bounded native pump. Public DoEvents is
    // outside this focus/navigation scope; WM_QUIT is retained as an observation.
    private static void Pump()
    {
        long deadline = Environment.TickCount64 + 2000;
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (++count > 4096 || Environment.TickCount64 > deadline)
                throw new TimeoutException("Control028 message pump did not become idle.");
            if (message.Message == 0x0012) { Record("pump-quit", true); continue; }
            _ = TranslateMessage(in message);
            _ = DispatchMessageW(in message);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage { public nint Window; public uint Message; public nuint WParam; public nint LParam; public uint Time; public Point Point; public uint Private; }
    [DllImport("user32.dll")] private static extern nint GetFocus();
    [DllImport("user32.dll")] private static extern nint SetFocus(nint handle);
    [DllImport("user32.dll")] private static extern nint SendMessageW(nint hwnd, uint msg, nint wparam, nint lparam);
    [DllImport("user32.dll")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(nint hwnd);
    [DllImport("user32.dll")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")] private static extern nint DispatchMessageW(in NativeMessage message);
}
