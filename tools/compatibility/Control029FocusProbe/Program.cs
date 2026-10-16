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

// Batch 029 fixed-reference probe. The source is intentionally shared by the
// WinForms 10.0.11 reference, Lumina JIT and Lumina Native AOT runs. Every
// scenario records raw managed/native observations; the driver compares only
// stdout ordinally and retains stderr plus original exceptions verbatim.
internal static class Program
{
    private static readonly List<Control> Observed = [];
    private static readonly List<string> Trace = [];
    private static readonly Exception Failure = new InvalidOperationException("control029 validation callback failure");
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

        if (args.Length != 1) throw new ArgumentException("Specify one Control029 scenario.");
        Record("scenario", args[0]);
        Record("input-evidence", "managed-api-and-programmatic-native-focus;physical-device-not-verified");
        switch (args[0])
        {
            case "enabled-focused": EnabledFocused(); break;
            case "enabled-ancestor": EnabledAncestor(); break;
            case "visible-focused": VisibleFocused(); break;
            case "visible-ancestor": VisibleAncestor(); break;
            case "tabstop-selectable": TabStopSelectable(); break;
            case "remove-focused": RemoveFocused(); break;
            case "remove-nested": RemoveNested(); break;
            case "destroy-focused": DestroyFocused(); break;
            case "nested-candidates": NestedCandidates(); break;
            case "validation-prevent-cancel": Validation(AutoValidate.EnablePreventFocusChange, true, true, false); break;
            case "validation-allow-cancel": Validation(AutoValidate.EnableAllowFocusChange, true, true, false); break;
            case "validation-disable-cancel": Validation(AutoValidate.Disable, true, true, false); break;
            case "validation-causes-false": Validation(AutoValidate.EnablePreventFocusChange, false, true, false); break;
            case "validation-reentrant": Validation(AutoValidate.EnablePreventFocusChange, true, true, true); break;
            case "validation-exception": Validation(AutoValidate.EnablePreventFocusChange, true, false, false, throwValidation: true); break;
            case "validation-children": ValidationChildren(); break;
            default: throw new ArgumentException("Unknown scenario " + args[0]);
        }
        Record("complete", true);
    }

    private static void EnabledFocused()
    {
        using ProbeForm form = NewForm();
        ProbeControl first = New("first"), second = New("second", 1);
        form.Controls.AddRange([first, second]);
        Show(form);
        Step("focus-first", () => Record("focus-return", first.Focus()));
        Step("disable-focused", () => first.Enabled = false);
        Step("reenable-focused", () => first.Enabled = true);
        Step("focus-second", () => Record("focus-return", second.Focus()));
        Close(form);
    }

    private static void EnabledAncestor()
    {
        using ProbeForm form = NewForm();
        ProbeContainer root = NewContainer("root");
        ProbeControl first = New("first"), second = New("second", 1);
        root.Controls.AddRange([first, second]);
        form.Controls.Add(root);
        Show(form);
        Step("focus-first", () => Record("focus-return", first.Focus()));
        Step("disable-ancestor", () => root.Enabled = false);
        Step("reenable-ancestor", () => root.Enabled = true);
        Step("focus-second", () => Record("focus-return", second.Focus()));
        Close(form);
    }

    private static void VisibleFocused()
    {
        using ProbeForm form = NewForm();
        ProbeControl first = New("first"), second = New("second", 1);
        form.Controls.AddRange([first, second]);
        Show(form);
        Step("focus-first", () => Record("focus-return", first.Focus()));
        Step("hide-focused", () => first.Visible = false);
        Step("reshow-focused", () => first.Visible = true);
        Step("focus-second", () => Record("focus-return", second.Focus()));
        Close(form);
    }

    private static void VisibleAncestor()
    {
        using ProbeForm form = NewForm();
        ProbeContainer root = NewContainer("root");
        ProbeControl first = New("first"), second = New("second", 1);
        root.Controls.AddRange([first, second]);
        form.Controls.Add(root);
        Show(form);
        Step("focus-first", () => Record("focus-return", first.Focus()));
        Step("hide-ancestor", () => root.Visible = false);
        Step("reshow-ancestor", () => root.Visible = true);
        Step("focus-second", () => Record("focus-return", second.Focus()));
        Close(form);
    }

    private static void TabStopSelectable()
    {
        using ProbeForm form = NewForm();
        ProbeControl first = New("first"), second = New("second", 1), third = New("third", 2);
        form.Controls.AddRange([first, second, third]);
        Show(form);
        Step("focus-first", () => Record("focus-return", first.Focus()));
        Step("tabstop-false", () => first.TabStop = false);
        Step("next-after-tabstop", () => Record("selected", form.SelectNextControl(first, true, true, true, false)));
        Step("selectable-false", () => second.Style(ControlStyles.Selectable, false));
        Step("next-after-selectable", () => Record("selected", form.SelectNextControl(first, true, false, true, false)));
        Step("selectable-true", () => second.Style(ControlStyles.Selectable, true));
        Step("disable-third", () => third.Enabled = false);
        Step("next-after-disabled", () => Record("selected", form.SelectNextControl(first, true, false, true, true)));
        Close(form);
    }

    private static void RemoveFocused()
    {
        using ProbeForm form = NewForm();
        ProbeContainer root = NewContainer("root");
        ProbeControl first = New("first"), second = New("second", 1), third = New("third", 2);
        root.Controls.AddRange([first, second, third]);
        form.Controls.Add(root);
        Show(form);
        Step("focus-first", () => Record("focus-return", first.Focus()));
        Step("remove-focused", () => root.Controls.Remove(first));
        Step("dispose-removed", first.Dispose);
        Step("remove-active-second", () => root.Controls.Remove(second));
        Step("dispose-removed-second", second.Dispose);
        Close(form);
    }

    private static void RemoveNested()
    {
        using ProbeForm form = NewForm();
        ProbeContainer root = NewContainer("root");
        ProbeContainer nested = NewContainer("nested");
        ProbeControl inner = New("inner"), innerLast = New("inner-last", 1), outside = New("outside", 2);
        nested.Controls.AddRange([inner, innerLast]);
        root.Controls.AddRange([nested, outside]);
        form.Controls.Add(root);
        Show(form);
        Step("focus-inner", () => Record("focus-return", inner.Focus()));
        Step("remove-nested", () => root.Controls.Remove(nested));
        Step("dispose-nested", () =>
        {
            nested.Dispose();
            inner.Dispose();
            innerLast.Dispose();
        });
        Step("focus-outside", () => Record("focus-return", outside.Focus()));
        Close(form);
    }

    private static void DestroyFocused()
    {
        using ProbeForm form = NewForm();
        ProbeControl first = New("first"), second = New("second", 1);
        form.Controls.AddRange([first, second]);
        Show(form);
        Step("focus-first", () => Record("focus-return", first.Focus()));
        Step("destroy-focused-handle", first.DestroyNativeHandle);
        Step("recreate-first", first.CreateControl);
        Step("focus-second", () => Record("focus-return", second.Focus()));
        Close(form);
    }

    private static void NestedCandidates()
    {
        using ProbeForm form = NewForm();
        ProbeContainer outer = NewContainer("outer");
        ProbeContainer nested = NewContainer("nested");
        ProbeControl inner = New("inner"), nestedLast = New("nested-last", 1), outside = New("outside", 2);
        nested.Controls.AddRange([inner, nestedLast]);
        outer.Controls.AddRange([nested, outside]);
        form.Controls.Add(outer);
        Show(form);
        Step("focus-inner", () => Record("focus-return", inner.Focus()));
        Step("disable-inner", () => inner.Enabled = false);
        Step("hide-nested", () => nested.Visible = false);
        Step("reshow-nested", () => nested.Visible = true);
        Step("nested-select-next", () => Record("selected", outer.SelectNextControl(nested, true, true, true, true)));
        Step("remove-nested-last", () => nested.Controls.Remove(nestedLast));
        Step("dispose-removed-nested-last", nestedLast.Dispose);
        Step("focus-outside", () => Record("focus-return", outside.Focus()));
        Close(form);
    }

    private static void Validation(AutoValidate autoValidate, bool targetCausesValidation, bool cancel,
        bool reentrant, bool throwValidation = false)
    {
        using ProbeForm form = NewForm();
        form.AutoValidate = autoValidate;
        ProbeControl source = New("source"), target = New("target", 1);
        source.CancelValidation = cancel;
        source.ThrowValidation = throwValidation;
        source.ReenterValidation = reentrant;
        source.ValidationContainer = form;
        target.CausesValidation = targetCausesValidation;
        form.Controls.AddRange([source, target]);
        Show(form);
        Step("focus-source", () => Record("focus-return", source.Focus()));
        Step("select-target", () => target.Select());
        Step("explicit-validate", () => Record("validate", form.Validate()));
        Step("explicit-validate-check-auto", () => Record("validate-check-auto", form.Validate(true)));
        Close(form);
    }

    private static void ValidationChildren()
    {
        using ProbeForm form = NewForm();
        ProbeContainer nested = NewContainer("nested");
        ProbeControl first = New("first"), disabled = New("disabled", 1), hidden = New("hidden", 2), noTab = New("no-tab", 3);
        ProbeControl inner = New("inner"), innerLast = New("inner-last", 1);
        first.CancelValidation = true;
        disabled.Enabled = false;
        hidden.Visible = false;
        noTab.TabStop = false;
        nested.Controls.AddRange([inner, innerLast]);
        form.Controls.AddRange([first, disabled, hidden, noTab, nested]);
        Step("validate-children-default", () => Record("validate-children", form.ValidateChildren()));
        Step("validate-children-immediate", () => Record("validate-children", form.ValidateChildren(ValidationConstraints.ImmediateChildren)));
        Step("validate-children-enabled-visible-tab", () => Record("validate-children", form.ValidateChildren(ValidationConstraints.Enabled | ValidationConstraints.Visible | ValidationConstraints.TabStop)));
        Close(form);
    }

    private static T Observe<T>(T control) where T : Control
    {
        Observed.Add(control);
        control.GotFocus += (_, e) => Hook(control, "event-GotFocus:args=" + Args(e));
        control.LostFocus += (_, e) => Hook(control, "event-LostFocus:args=" + Args(e));
        control.Enter += (_, e) => Hook(control, "event-Enter:args=" + Args(e));
        control.Leave += (_, e) => Hook(control, "event-Leave:args=" + Args(e));
        control.Validating += (_, e) =>
        {
            Hook(control, "event-Validating:cancel-before=" + e.Cancel);
            if (control is ProbeControl p)
            {
                if (p.ReenterValidation && !p.ReentrantEntered && p.ValidationContainer is not null)
                {
                    p.ReentrantEntered = true;
                    bool nested = p.ValidationContainer.Validate();
                    Emit("reentrant-validate=" + nested);
                }

                if (p.ThrowValidation) throw Failure;
                if (p.CancelValidation) e.Cancel = true;
            }
            Hook(control, "event-Validating:cancel-after=" + e.Cancel);
        };
        control.Validated += (_, e) => Hook(control, "event-Validated:args=" + Args(e));
        return control;
    }

    private static ProbeControl New(string name, int tab = 0)
        => Observe(new ProbeControl { Name = name, TabIndex = tab, Bounds = new Rectangle(10 + tab * 100, 10, 90, 30) });

    private static ProbeContainer NewContainer(string name)
        => Observe(new ProbeContainer { Name = name, Bounds = new Rectangle(10, 50, 330, 180) });

    private static ProbeForm NewForm()
        => Observe(new ProbeForm
        {
            Name = "form",
            Text = "Control029 focus and validation probe",
            ClientSize = new Size(500, 360),
            StartPosition = FormStartPosition.Manual,
            Location = new Point(120, 120)
        });

    private static void Show(Form form)
    {
        Step("show", () => { form.Show(); Pump(); });
        Step("show-clear-programmatic-focus", () => SetFocus(0));
    }

    private static void Close(Form form)
    {
        HwndEvidence("before-close");
        nint[] handles = Observed.Where(c => c.IsHandleCreated).Select(c => c.Handle).ToArray();
        Step("normal-close", form.Close);
        form.Dispose();
        Pump();
        HwndEvidence("after-close");
        int survivors = handles.Count(IsWindow);
        Record("native-window-survivors", survivors);
        if (survivors != 0) throw new InvalidOperationException("A probe native window survived normal close/disposal.");
    }

    private static void Step(string name, Action action)
    {
        Record("step", name);
        Trace.Clear();
        _recording = true;
        try
        {
            action();
            Record("result", "success");
        }
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
        {
            Record(label + ".state:" + c.Name,
                "select=" + c.CanSelect + ";focus=" + c.Focused + ";contains=" + c.ContainsFocus
                + ";handle=" + c.IsHandleCreated + ";disposed=" + c.IsDisposed + ";enabled=" + c.Enabled
                + ";visible=" + c.Visible + ";tabstop=" + c.TabStop + ";parent=" + Name(c.Parent));
        }
    }

    private static void HwndEvidence(string stage)
    {
        foreach (Control c in Observed.Where(c => c.IsHandleCreated))
            Console.Error.WriteLine("hwnd:" + stage + ":" + c.Name + "=0x" + c.Handle.ToInt64().ToString("X", CultureInfo.InvariantCulture));
    }

    private static string Activity()
        => string.Join(",", Observed.OfType<ContainerControl>().Select(c => c.Name + ":" + Name(c.ActiveControl)));

    private static string NativeOwner()
    {
        nint focus = GetFocus();
        if (focus == 0) return "null";
        return Observed.FirstOrDefault(c => c.IsHandleCreated && c.Handle == focus)?.Name ?? "unmapped-native-window";
    }

    private static string Name(object? value) => value is Control control ? control.Name : value is null ? "null" : value.GetType().FullName!;
    private static string Args(EventArgs? value) => value is null ? "null" : ReferenceEquals(value, EventArgs.Empty) ? "empty" : value.GetType().FullName!;
    private static string ExceptionText(Exception ex) => ex.GetType().FullName + ":" + ex.Message;
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + value);
    private static void Emit(string value) { if (_recording) Trace.Add(value); }
    private static void Hook(Control control, string stage) => Emit(control.Name + ":" + stage + ":active=" + Activity() + ":native=" + NativeOwner());

    private static void Pump()
    {
        long deadline = Environment.TickCount64 + 2000;
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (++count > 4096 || Environment.TickCount64 > deadline)
                throw new TimeoutException("Control029 message pump did not become idle.");
            if (message.Message == 0x0012) { Record("pump-quit", true); continue; }
            _ = TranslateMessage(in message);
            _ = DispatchMessageW(in message);
        }
    }

    private sealed class ProbeControl : Control
    {
        internal bool CancelValidation;
        internal bool ThrowValidation;
        internal bool ReenterValidation;
        internal bool ReentrantEntered;
        internal ContainerControl? ValidationContainer;
        internal void Style(ControlStyles style, bool value) => SetStyle(style, value);
        internal void DestroyNativeHandle() => DestroyHandle();

        protected override void OnGotFocus(EventArgs e) { Hook(this, "got-before"); base.OnGotFocus(e); Hook(this, "got-after"); }
        protected override void OnLostFocus(EventArgs e) { Hook(this, "lost-before"); base.OnLostFocus(e); Hook(this, "lost-after"); }
        protected override void OnEnter(EventArgs e) { Hook(this, "enter-before"); base.OnEnter(e); Hook(this, "enter-after"); }
        protected override void OnLeave(EventArgs e) { Hook(this, "leave-before"); base.OnLeave(e); Hook(this, "leave-after"); }
        protected override void OnValidating(CancelEventArgs e) { Hook(this, "validating-before"); base.OnValidating(e); Hook(this, "validating-after:" + e.Cancel); }
        protected override void OnValidated(EventArgs e) { Hook(this, "validated-before"); base.OnValidated(e); Hook(this, "validated-after"); }
    }

    private sealed class ProbeContainer : ContainerControl
    {
        protected override void OnGotFocus(EventArgs e) { Hook(this, "got-before"); base.OnGotFocus(e); Hook(this, "got-after"); }
        protected override void OnLostFocus(EventArgs e) { Hook(this, "lost-before"); base.OnLostFocus(e); Hook(this, "lost-after"); }
        protected override void OnEnter(EventArgs e) { Hook(this, "enter-before"); base.OnEnter(e); Hook(this, "enter-after"); }
        protected override void OnLeave(EventArgs e) { Hook(this, "leave-before"); base.OnLeave(e); Hook(this, "leave-after"); }
        protected override void OnValidating(CancelEventArgs e) { Hook(this, "validating-before"); base.OnValidating(e); Hook(this, "validating-after:" + e.Cancel); }
        protected override void OnValidated(EventArgs e) { Hook(this, "validated-before"); base.OnValidated(e); Hook(this, "validated-after"); }
    }

    private sealed class ProbeForm : Form
    {
        protected override void OnGotFocus(EventArgs e) { Hook(this, "got-before"); base.OnGotFocus(e); Hook(this, "got-after"); }
        protected override void OnLostFocus(EventArgs e) { Hook(this, "lost-before"); base.OnLostFocus(e); Hook(this, "lost-after"); }
        protected override void OnEnter(EventArgs e) { Hook(this, "enter-before"); base.OnEnter(e); Hook(this, "enter-after"); }
        protected override void OnLeave(EventArgs e) { Hook(this, "leave-before"); base.OnLeave(e); Hook(this, "leave-after"); }
        protected override void OnValidating(CancelEventArgs e) { Hook(this, "validating-before"); base.OnValidating(e); Hook(this, "validating-after:" + e.Cancel); }
        protected override void OnValidated(EventArgs e) { Hook(this, "validated-before"); base.OnValidated(e); Hook(this, "validated-after"); }
    }

    private struct NativeMessage
    {
        public nint Window;
        public uint Message;
        public nuint WParam;
        public nint LParam;
        public uint Time;
        public Point Point;
        public uint Private;
    }

    [DllImport("user32.dll")] private static extern nint GetFocus();
    [DllImport("user32.dll")] private static extern nint SetFocus(nint handle);
    [DllImport("user32.dll")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(nint hwnd);
    [DllImport("user32.dll")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll")][return: MarshalAs(UnmanagedType.Bool)] private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")] private static extern nint DispatchMessageW(in NativeMessage message);
}
