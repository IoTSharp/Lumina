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

// Each scenario has an independent STA process and bounded native message pump.
// Outputs are observations, never substitutions for the expected implementation.
internal static class Program
{
    private static readonly List<string> Events = [];
    private static readonly Exception OriginalFailure = new InvalidOperationException("appearance callback failure");
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
        if (args.Length != 1) throw new ArgumentException("Specify exactly one appearance scenario.");
        Record("scenario", args[0]);
        Console.Error.WriteLine("BEGIN " + args[0]);
        switch (args[0])
        {
            case "metadata": Metadata(); break;
            case "defaults": Defaults(); break;
            case "font-values": FontValues(); break;
            case "font-height": FontHeightValues(); break;
            case "color-values": ColorValues(); break;
            case "inheritance": Inheritance(); break;
            case "reparent": Reparent(); break;
            case "virtual-getters": VirtualGetters(); break;
            case "notification-order": NotificationOrder(); break;
            case "overrides": Overrides(); break;
            case "parent-overrides": ParentOverrides(); break;
            case "reentrancy": Reentrancy(); break;
            case "exceptions": Exceptions(); break;
            case "borrowed-font": BorrowedFont(); break;
            case "native-font": NativeFont(); break;
            case "native-color": NativeColor(); break;
            default: throw new ArgumentException("Unknown appearance scenario: " + args[0]);
        }
        Console.Error.WriteLine("END " + args[0]);
        Record("complete", true);
    }

    private static void Metadata()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        // Literal member names let trimming retain only this observed surface.
        // Do not root every non-public Control method for this small metadata probe.
        (string Name, PropertyInfo? Property)[] properties =
        [
            ("Font", typeof(Control).GetProperty("Font", flags)),
            ("FontHeight", typeof(Control).GetProperty("FontHeight", flags)),
            ("BackColor", typeof(Control).GetProperty("BackColor", flags)),
            ("ForeColor", typeof(Control).GetProperty("ForeColor", flags)),
            ("DefaultFont", typeof(Control).GetProperty("DefaultFont", flags)),
            ("DefaultBackColor", typeof(Control).GetProperty("DefaultBackColor", flags)),
            ("DefaultForeColor", typeof(Control).GetProperty("DefaultForeColor", flags))
        ];
        foreach ((string name, PropertyInfo? property) in properties)
        {
            Record("property-" + name, property is null ? "missing" : $"type:{property.PropertyType.Name}|get:{Shape(property.GetMethod)}|set:{Shape(property.SetMethod)}");
            if (property is not null)
            {
                Record("attributes-" + name, $"browsable:{property.GetCustomAttribute<BrowsableAttribute>()?.Browsable.ToString() ?? "absent"}|localizable:{property.GetCustomAttribute<LocalizableAttribute>()?.IsLocalizable.ToString() ?? "absent"}|ambient:[{string.Join(";", property.CustomAttributes.Where(a => a.AttributeType == typeof(AmbientValueAttribute)).Select(a => string.Join(",", a.ConstructorArguments.Select(v => v.Value?.ToString() ?? "null"))))}]");
            }
        }
        (string Name, MethodInfo? Method)[] methods =
        [
            ("ResetFont", typeof(Control).GetMethod("ResetFont", flags, Type.EmptyTypes)),
            ("ResetBackColor", typeof(Control).GetMethod("ResetBackColor", flags, Type.EmptyTypes)),
            ("ResetForeColor", typeof(Control).GetMethod("ResetForeColor", flags, Type.EmptyTypes)),
            ("ShouldSerializeFont", typeof(Control).GetMethod("ShouldSerializeFont", flags, Type.EmptyTypes)),
            ("ShouldSerializeBackColor", typeof(Control).GetMethod("ShouldSerializeBackColor", flags, Type.EmptyTypes)),
            ("ShouldSerializeForeColor", typeof(Control).GetMethod("ShouldSerializeForeColor", flags, Type.EmptyTypes)),
            ("OnFontChanged", typeof(Control).GetMethod("OnFontChanged", flags, [typeof(EventArgs)])),
            ("OnBackColorChanged", typeof(Control).GetMethod("OnBackColorChanged", flags, [typeof(EventArgs)])),
            ("OnForeColorChanged", typeof(Control).GetMethod("OnForeColorChanged", flags, [typeof(EventArgs)])),
            ("OnParentFontChanged", typeof(Control).GetMethod("OnParentFontChanged", flags, [typeof(EventArgs)])),
            ("OnParentBackColorChanged", typeof(Control).GetMethod("OnParentBackColorChanged", flags, [typeof(EventArgs)])),
            ("OnParentForeColorChanged", typeof(Control).GetMethod("OnParentForeColorChanged", flags, [typeof(EventArgs)]))
        ];
        foreach ((string name, MethodInfo? method) in methods) Record("method-" + name, Shape(method));
        (string Name, EventInfo? Event)[] events =
        [
            ("FontChanged", typeof(Control).GetEvent("FontChanged", flags)),
            ("BackColorChanged", typeof(Control).GetEvent("BackColorChanged", flags)),
            ("ForeColorChanged", typeof(Control).GetEvent("ForeColorChanged", flags))
        ];
        foreach ((string name, EventInfo? value) in events)
        {
            Record("event-" + name, value is null ? "missing" : $"type:{value.EventHandlerType?.Name}|add:{Shape(value.AddMethod)}|remove:{Shape(value.RemoveMethod)}");
        }
    }

    private static string Shape(MethodInfo? method) => method is null ? "missing" :
        $"access:{(method.IsPublic ? "public" : method.IsFamily ? "protected" : method.IsFamilyOrAssembly ? "protected-internal" : method.IsAssembly ? "internal" : "private")},static:{method.IsStatic},virtual:{method.IsVirtual},final:{method.IsFinal},return:{method.ReturnType.Name},params:[{string.Join(",", method.GetParameters().Select(p => p.ParameterType.Name))}]";

    private static void Defaults()
    {
        using var first = New("first");
        using var second = New("second");
        Record("default-font", FontText(Control.DefaultFont));
        Record("default-back", ColorText(Control.DefaultBackColor));
        Record("default-fore", ColorText(Control.DefaultForeColor));
        Record("default-identity", $"static:{ReferenceEquals(Control.DefaultFont, Control.DefaultFont)}|first:{ReferenceEquals(first.Font, Control.DefaultFont)}|controls:{ReferenceEquals(first.Font, second.Font)}");
        Step("initial", () => { }, first, second);
        Step("reset-unset", () => { first.ResetFont(); first.ResetBackColor(); first.ResetForeColor(); }, first, second);
        Step("assign-defaults", () => { first.Font = Control.DefaultFont; first.BackColor = Control.DefaultBackColor; first.ForeColor = Control.DefaultForeColor; }, first, second);
        Step("reset-defaults", () => { first.ResetFont(); first.ResetBackColor(); first.ResetForeColor(); }, first, second);
    }

    private static void FontValues()
    {
        using var first = MakeFont(11);
        using var equal = (Font)first.Clone();
        using var other = MakeFont(14, FontStyle.Bold);
        using var control = New("control");
        Step("set-first", () => control.Font = first, control);
        Record("first-identity", ReferenceEquals(control.Font, first));
        Step("same-instance", () => control.Font = first, control);
        Step("equal-instance", () => control.Font = equal, control);
        Record("equal-identity", $"first:{ReferenceEquals(control.Font, first)}|equal:{ReferenceEquals(control.Font, equal)}|equals:{control.Font.Equals(equal)}");
        Step("other", () => control.Font = other, control);
        Step("null", () => control.Font = null!, control);
        Step("null-again", () => control.Font = null!, control);
        Step("set-before-reset", () => control.Font = first, control);
        Step("reset", control.ResetFont, control);
        Step("reset-again", control.ResetFont, control);
        using var parent = New("parent");
        parent.Font = first;
        parent.Controls.Add(control);
        Step("explicit-equals-inherited", () => control.Font = equal, parent, control);
        Record("inherited-equal-identity", ReferenceEquals(control.Font, equal));
        Step("reset-equal-inherited", control.ResetFont, parent, control);
        Record("reset-inherited-identity", ReferenceEquals(control.Font, first));
    }

    private static void FontHeightValues()
    {
        using var font = MakeFont(15);
        using var parent = New("parent");
        using var child = New("child");
        parent.Controls.Add(child);
        Record("default-height", $"parent:{parent.ReadFontHeight()}|child:{child.ReadFontHeight()}|font:{parent.Font.Height}");
        foreach (int value in new[] { 0, 27, -2, -1, int.MaxValue, int.MinValue })
        {
            parent.WriteFontHeight(value);
            Record("parent-height-" + value, $"parent:{parent.ReadFontHeight()}|child:{child.ReadFontHeight()}");
        }
        child.WriteFontHeight(83);
        Record("child-local-height", child.ReadFontHeight());
        Step("parent-font-height-invalidated", () => parent.Font = font, parent, child);
        Record("after-parent-font", $"parent:{parent.ReadFontHeight()}|child:{child.ReadFontHeight()}|font:{font.Height}");
        child.WriteFontHeight(79);
        Step("child-font-height-invalidated", () => child.Font = Control.DefaultFont, parent, child);
        Record("after-child-font", child.ReadFontHeight());
        Step("child-height-reset", child.ResetFont, parent, child);
        Record("after-child-reset", child.ReadFontHeight());
    }

    private static void ColorValues()
    {
        using var control = New("control");
        foreach (bool back in new[] { true, false })
        {
            string prefix = back ? "back-" : "fore-";
            void Set(Color color) { if (back) control.BackColor = color; else control.ForeColor = color; }
            void Reset() { if (back) control.ResetBackColor(); else control.ResetForeColor(); }
            Step(prefix + "empty", () => Set(Color.Empty), control);
            Step(prefix + "red", () => Set(Color.Red), control);
            Step(prefix + "same-red", () => Set(Color.Red), control);
            Step(prefix + "argb-red", () => Set(Color.FromArgb(Color.Red.ToArgb())), control);
            Step(prefix + "known-red", () => Set(Color.FromKnownColor(KnownColor.Red)), control);
            Step(prefix + "reset", Reset, control);
            Step(prefix + "reset-again", Reset, control);
            Step(prefix + "transparent", () => Set(Color.Transparent), control);
            Step(prefix + "partial-alpha", () => Set(Color.FromArgb(127, 20, 40, 60)), control);
            Step(prefix + "empty-reset", () => Set(Color.Empty), control);
        }
    }

    private static void Inheritance()
    {
        using var font = MakeFont(12);
        using var next = MakeFont(16, FontStyle.Italic);
        using var root = New("root");
        using var first = New("first");
        using var second = New("second");
        using var nested = New("nested");
        root.Controls.AddRange([first, second]);
        first.Controls.Add(nested);
        Step("root-all", () => { root.Font = font; root.ForeColor = Color.DarkGreen; root.BackColor = Color.LightYellow; }, root, first, nested, second);
        Record("inherited-font-identities", $"first:{ReferenceEquals(first.Font, font)}|nested:{ReferenceEquals(nested.Font, font)}|second:{ReferenceEquals(second.Font, font)}");
        Step("first-explicit-equal", () => { first.Font = font; first.BackColor = root.BackColor; first.ForeColor = root.ForeColor; }, root, first, nested, second);
        Step("root-new-values", () => { root.Font = next; root.ForeColor = Color.Navy; root.BackColor = Color.Beige; }, root, first, nested, second);
        Step("first-reset", () => { first.ResetFont(); first.ResetForeColor(); first.ResetBackColor(); }, root, first, nested, second);
        Step("root-reset", () => { root.ResetFont(); root.ResetForeColor(); root.ResetBackColor(); }, root, first, nested, second);
        Step("direct-parent-notifications", () => { first.RaiseParentFont(EventArgs.Empty); first.RaiseParentFore(EventArgs.Empty); first.RaiseParentBack(EventArgs.Empty); }, root, first, nested, second);
    }

    private static void Reparent()
    {
        using var one = MakeFont(10);
        using var two = MakeFont(14);
        using var first = New("first");
        using var second = New("second");
        using var child = New("child");
        using var nested = New("nested");
        first.Font = one; first.BackColor = Color.AliceBlue; first.ForeColor = Color.DarkRed;
        second.Font = two; second.BackColor = Color.Bisque; second.ForeColor = Color.DarkBlue;
        child.Controls.Add(nested);
        Step("add-first", () => first.Controls.Add(child), first, second, child, nested);
        Step("move-second", () => child.Parent = second, first, second, child, nested);
        Step("same-parent", () => child.Parent = second, first, second, child, nested);
        Step("remove", () => second.Controls.Remove(child), first, second, child, nested);
        child.Font = one; child.BackColor = Color.LightPink; child.ForeColor = Color.Black;
        Step("explicit-add", () => first.Controls.Add(child), first, second, child, nested);
        Step("explicit-move", () => second.Controls.Add(child), first, second, child, nested);
        Step("explicit-remove", () => child.Parent = null, first, second, child, nested);
    }

    private static void VirtualGetters()
    {
        using var local = MakeFont(12);
        using var synthetic = MakeFont(18, FontStyle.Bold);
        using var parent = new GetterControl { Name = "parent", SyntheticFont = synthetic };
        using var child = New("child");
        parent.Font = local;
        parent.Controls.Add(child);
        Step("virtual-getters", () => { Record("parent-public", Appearance(parent)); Record("child-public", Appearance(child)); }, child);
        Record("font-source", $"local:{ReferenceEquals(child.Font, local)}|synthetic:{ReferenceEquals(child.Font, synthetic)}");
        Step("virtual-parent-reset", parent.ResetFont, child);
        Record("font-default-source", ReferenceEquals(child.Font, Control.DefaultFont));
        using var dispatch = new PropertyControl { Name = "dispatch" };
        Watch(dispatch);
        Control baseView = dispatch;
        Step("virtual-setters", () => { baseView.Font = local; baseView.BackColor = Color.Brown; baseView.ForeColor = Color.Blue; }, dispatch);
        Step("virtual-resets", () => { baseView.ResetFont(); baseView.ResetBackColor(); baseView.ResetForeColor(); }, dispatch);
    }

    private static void NotificationOrder()
    {
        using var font = MakeFont(13);
        using var root = New("root");
        using var first = New("first");
        using var second = New("second");
        using var nested = New("nested");
        root.Controls.AddRange([first, second]); first.Controls.Add(nested);
        Step("font-depth-first", () => root.Font = font, root, first, nested, second);
        Step("fore-depth-first", () => root.ForeColor = Color.Purple, root, first, nested, second);
        Step("back-depth-first", () => root.BackColor = Color.Plum, root, first, nested, second);
        root.Controls.SetChildIndex(second, 0);
        Step("font-reordered", root.ResetFont, root, second, first, nested);
        Step("direct-font-custom-args", () => first.RaiseFont(new EventArgs()), root, first, nested, second);
        Step("direct-back-null-args", () => first.RaiseBack(null!), root, first, nested, second);
        Step("direct-fore-empty-args", () => first.RaiseFore(EventArgs.Empty), root, first, nested, second);
    }

    private static void Overrides()
    {
        using var font = MakeFont(14);
        using var root = New("root");
        using var child = New("child");
        root.Controls.Add(child);
        root.SkipKind = "font";
        Step("skip-font-base", () => root.Font = font, root, child);
        root.SkipKind = "back";
        Step("skip-back-base", () => root.BackColor = Color.Coral, root, child);
        root.SkipKind = "fore";
        Step("skip-fore-base", () => root.ForeColor = Color.Maroon, root, child);
        root.SkipKind = null;
        Step("manual-base-font", () => root.RaiseFont(EventArgs.Empty), root, child);
        Step("manual-base-back", () => root.RaiseBack(EventArgs.Empty), root, child);
        Step("manual-base-fore", () => root.RaiseFore(EventArgs.Empty), root, child);
    }

    private static void ParentOverrides()
    {
        using var font = MakeFont(13);
        using var root = New("root");
        using var child = New("child");
        using var nested = New("nested");
        root.Controls.Add(child); child.Controls.Add(nested);
        child.SkipKind = "parent-font";
        Step("skip-parent-font", () => root.Font = font, root, child, nested);
        child.SkipKind = "parent-fore";
        Step("skip-parent-fore", () => root.ForeColor = Color.Teal, root, child, nested);
        child.SkipKind = "parent-back";
        Step("skip-parent-back", () => root.BackColor = Color.Azure, root, child, nested);
        child.SkipKind = null;
        Step("manual-parent-font", () => child.RaiseParentFont(new EventArgs()), root, child, nested);
        Step("manual-parent-fore", () => child.RaiseParentFore(null!), root, child, nested);
        Step("manual-parent-back", () => child.RaiseParentBack(EventArgs.Empty), root, child, nested);
        child.Font = font; child.ForeColor = Color.Red; child.BackColor = Color.Linen;
        Step("explicit-parent-notify", () => { child.RaiseParentFont(EventArgs.Empty); child.RaiseParentFore(EventArgs.Empty); child.RaiseParentBack(EventArgs.Empty); }, root, child, nested);
    }

    private static void Reentrancy()
    {
        using var firstFont = MakeFont(11);
        using var secondFont = MakeFont(17);
        using var root = New("root");
        using var first = New("first");
        using var second = New("second");
        root.Controls.AddRange([first, second]);
        root.CallbackKind = "font"; root.Callback = () => root.Font = secondFont;
        Step("reenter-font", () => root.Font = firstFont, root, first, second);
        root.CallbackKind = "back"; root.Callback = () => root.BackColor = Color.Gold;
        Step("reenter-back", () => root.BackColor = Color.Pink, root, first, second);
        root.CallbackKind = "fore"; root.Callback = () => root.ForeColor = Color.DarkOrange;
        Step("reenter-fore", () => root.ForeColor = Color.Navy, root, first, second);
        first.CallbackKind = "fore"; first.Callback = () => root.Controls.Remove(first);
        Step("remove-during-propagation", () => root.ForeColor = Color.DarkViolet, root, first, second);
        root.Controls.Add(first);
        first.CallbackKind = "back"; first.Callback = () => second.BackColor = Color.Aqua;
        Step("sibling-explicit-during-propagation", () => root.BackColor = Color.Beige, root, first, second);
    }

    private static void Exceptions()
    {
        using var font = MakeFont(14);
        using var root = New("root");
        using var child = New("child");
        root.Controls.Add(child);
        root.ThrowKind = "font";
        Step("font-override-throws", () => root.Font = font, root, child);
        root.ThrowKind = "back";
        Step("back-override-throws", () => root.BackColor = Color.Khaki, root, child);
        root.ThrowKind = "fore";
        Step("fore-override-throws", () => root.ForeColor = Color.Olive, root, child);
        root.ThrowKind = null;
        EventHandler throwing = (_, _) => throw OriginalFailure;
        root.FontChanged += throwing;
        Step("font-event-throws", root.ResetFont, root, child);
        root.FontChanged -= throwing;
        root.BackColorChanged += throwing;
        Step("back-event-throws", root.ResetBackColor, root, child);
        root.BackColorChanged -= throwing;
        root.ForeColorChanged += throwing;
        Step("fore-event-throws", root.ResetForeColor, root, child);
        root.ForeColorChanged -= throwing;
        child.ThrowKind = "parent-font";
        Step("parent-font-throws", () => root.Font = font, root, child);
        child.ThrowKind = null;
        Step("recover", root.ResetFont, root, child);
    }

    private static void BorrowedFont()
    {
        using var font = MakeFont(14, FontStyle.Bold);
        var root = New("root");
        var child = New("child");
        root.Font = font; root.Controls.Add(child);
        Step("dispose-tree", root.Dispose, root, child);
        Record("borrowed-after-dispose", FontText(font) + ":height:" + font.Height);
        Step("disposed-setters", () => { child.Font = Control.DefaultFont; child.BackColor = Color.Red; child.ForeColor = Color.Blue; }, child);
        using var textFont = MakeFont(15, FontStyle.Italic);
        var text = new TextBox { Font = textFont };
        Control baseView = text;
        Record("textbox-font-identity", $"typed:{ReferenceEquals(text.Font, textFont)}|base:{ReferenceEquals(baseView.Font, textFont)}|same:{ReferenceEquals(text.Font, baseView.Font)}");
        text.Dispose();
        Record("textbox-borrowed-after-dispose", FontText(textFont) + ":height:" + textFont.Height);
    }

    private static void NativeFont()
    {
        using var first = MakeFont(11);
        using var equal = (Font)first.Clone();
        using var second = MakeFont(16, FontStyle.Bold);
        using var form = new ProbeForm { Name = "form", ClientSize = new Size(320, 180), Font = first };
        using var control = New("control");
        using var text = new TextBox { Name = "text", Bounds = new Rectangle(10, 60, 180, 28) };
        control.Bounds = new Rectangle(10, 10, 180, 28);
        form.Controls.AddRange([control, text]);
        form.Show(); Pump();
        NativeFonts("created", control, text);
        nint controlHandle = control.Handle, textHandle = text.Handle;
        form.Font = equal; Pump();
        NativeFonts("equal-form-font", control, text);
        form.Font = second; Pump();
        NativeFonts("changed-form-font", control, text);
        control.Font = first; text.Font = first; Pump();
        NativeFonts("local-child-font", control, text);
        control.ResetFont(); text.ResetFont(); Pump();
        NativeFonts("reset-child-font", control, text);
        form.HandleDestroyed += (_, _) => NativeFonts("form-destroyed-event", control, text);
        form.HandleCreated += (_, _) => NativeFonts("form-created-event", control, text);
        nint immediatelyBeforeControl = control.Handle, immediatelyBeforeText = text.Handle;
        form.Rebuild(); Pump();
        NativeFonts("recreated-form", control, text);
        Record("children-preserved", $"control:{control.Handle == controlHandle}|text:{text.Handle == textHandle}");
        Record("recreate-children-preserved", $"control:{control.Handle == immediatelyBeforeControl}|text:{text.Handle == immediatelyBeforeText}");
        form.Font = first; Pump();
        NativeFonts("after-recreate-font", control, text);
        nint formHandle = form.Handle;
        nint finalControlHandle = control.Handle, finalTextHandle = text.Handle;
        form.Close(); Pump();
        Record("closed", $"form:{IsWindow(formHandle)}|control:{IsWindow(controlHandle)}|text:{IsWindow(textHandle)}|disposed:{form.IsDisposed},{control.IsDisposed},{text.IsDisposed}");
        Record("closed-final-handles", $"form:{IsWindow(formHandle)}|control:{IsWindow(finalControlHandle)}|text:{IsWindow(finalTextHandle)}");
        Record("native-borrowed-fonts", $"first:{first.Height}|equal:{equal.Height}|second:{second.Height}");
    }

    private static void NativeFonts(string name, params Control[] controls)
    {
        foreach (Control control in controls)
            Record(name + "-" + Id(control), control.IsHandleCreated
                ? $"managed:{FontText(control.Font)}|native:{NativeFontText(SendMessageW(control.Handle, 0x0031, 0, 0))}"
                : "missing-handle");
    }

    private static unsafe string NativeFontText(nint font)
    {
        if (font == 0) return "zero";
        byte* buffer = stackalloc byte[92];
        int copied = GetObjectW(font, 92, (nint)buffer);
        if (copied == 0) return "invalid";
        string face = Marshal.PtrToStringUni((nint)(buffer + 28), 32)!.TrimEnd('\0');
        return $"bytes:{copied}:height:{*(int*)buffer}:width:{*(int*)(buffer + 4)}:weight:{*(int*)(buffer + 16)}:italic:{buffer[20]}:underline:{buffer[21]}:strikeout:{buffer[22]}:charset:{buffer[23]}:face:{face}";
    }

    private static void NativeColor()
    {
        using var form = new ProbeForm { Name = "form", ClientSize = new Size(320, 180), BackColor = Color.LemonChiffon, ForeColor = Color.DarkBlue };
        using var text = new TextBox { Name = "text", Bounds = new Rectangle(10, 10, 180, 28) };
        using var virtualText = new GetterTextBox { Name = "virtual", Bounds = new Rectangle(10, 60, 180, 28) };
        form.Controls.AddRange([text, virtualText]);
        form.Show(); Pump();
        NativeColors("inherited", text);
        NativeColors("virtual-without-local", virtualText);
        virtualText.BackColor = Color.Red; virtualText.ForeColor = Color.Blue; Pump();
        NativeColors("virtual-with-local", virtualText);
        text.BackColor = Color.MistyRose; text.ForeColor = Color.DarkGreen; Pump();
        NativeColors("local", text);
        form.BackColor = Color.Azure; form.ForeColor = Color.Maroon; Pump();
        NativeColors("parent-changed-local", text);
        text.ResetBackColor(); text.ResetForeColor(); Pump();
        NativeColors("reset", text);
        form.Rebuild(); Pump();
        NativeColors("recreated", text);
        nint formHandle = form.Handle, textHandle = text.Handle, virtualHandle = virtualText.Handle;
        form.Close(); Pump();
        Record("color-closed", $"form:{IsWindow(formHandle)}|text:{IsWindow(textHandle)}");
        Record("virtual-color-closed", IsWindow(virtualHandle));
    }

    private static unsafe void NativeColors(string name, Control control)
    {
        nint dc = CreateCompatibleDC(0);
        if (dc == 0) throw new Win32Exception(Marshal.GetLastWin32Error(), "CreateCompatibleDC failed.");
        try
        {
            nint brush = SendMessageW(control.Parent!.Handle, 0x0133, dc, control.Handle);
            byte* buffer = stackalloc byte[16];
            int copied = brush == 0 ? 0 : GetObjectW(brush, 16, (nint)buffer);
            string brushText = copied == 0 ? (brush == 0 ? "zero" : "invalid") : $"style:{*(uint*)buffer}:color:{*(uint*)(buffer + 4):X8}";
            Record(name, $"managed-back:{ColorText(control.BackColor)}|managed-fore:{ColorText(control.ForeColor)}|native-back:{GetBkColor(dc):X8}|native-fore:{GetTextColor(dc):X8}|brush:{brushText}");
        }
        finally { if (!DeleteDC(dc)) throw new Win32Exception(Marshal.GetLastWin32Error(), "DeleteDC failed."); }
    }

    private static ProbeControl New(string name)
    {
        var control = new ProbeControl { Name = name };
        Watch(control);
        return control;
    }

    private static void Watch(Control control)
    {
        control.FontChanged += (_, e) => Trace("event-font:" + Id(control) + ":" + EventText(e) + ":" + FontText(control.Font));
        control.BackColorChanged += (_, e) => Trace("event-back:" + Id(control) + ":" + EventText(e) + ":" + ColorText(control.BackColor));
        control.ForeColorChanged += (_, e) => Trace("event-fore:" + Id(control) + ":" + EventText(e) + ":" + ColorText(control.ForeColor));
        control.ParentChanged += (_, _) => Trace("event-parent:" + Id(control) + ":" + Id(control.Parent));
        control.Layout += (_, e) => Trace("layout:" + Id(control) + ":" + Id(e.AffectedControl) + ":" + (e.AffectedProperty ?? "null"));
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

    private static void Trace(string text)
    {
        if (!_recording) return;
        if (Events.Count >= 1024) throw new ProbeRecursionException();
        Events.Add(text);
    }

    private static Font MakeFont(float size, FontStyle style = FontStyle.Regular) => new("Segoe UI", size, style, GraphicsUnit.Point);
    private static string Id(Control? control) => control is null ? "null" : control.Name.Length == 0 ? "unnamed" : control.Name;
    private static string FontText(Font font) => $"{font.Name},{font.Size.ToString("R", CultureInfo.InvariantCulture)},{(int)font.Style},{(int)font.Unit},{font.GdiCharSet},{font.GdiVerticalFont}";
    private static string ColorText(Color color) => $"{color.Name},{color.ToArgb():X8},empty:{color.IsEmpty},known:{color.IsKnownColor},system:{color.IsSystemColor}";
    private static string Appearance(Control control) => $"font:{FontText(control.Font)}:back:{ColorText(control.BackColor)}:fore:{ColorText(control.ForeColor)}";
    private static string State(Control control) => $"{Id(control)}:parent:{Id(control.Parent)}:{Appearance(control)}:handle:{control.IsHandleCreated}:disposed:{control.IsDisposed}";
    private static string EventText(EventArgs? e) => e is null ? "null" : ReferenceEquals(e, EventArgs.Empty) ? "empty" : "custom";
    private static string ExceptionText(Exception? exception) => exception is null ? "none" : ReferenceEquals(exception, OriginalFailure) ? "original" : exception.GetType().Name + (exception is ArgumentException argument ? ":" + argument.ParamName : "");
    private static void Record(string name, object? value) => Console.WriteLine(name + "=" + (value?.ToString() ?? "null"));

    private class ProbeControl : Control
    {
        internal string? SkipKind, ThrowKind, CallbackKind;
        internal Action? Callback;
        internal int ReadFontHeight() => FontHeight;
        internal void WriteFontHeight(int value) => FontHeight = value;
        internal void RaiseFont(EventArgs e) => OnFontChanged(e);
        internal void RaiseBack(EventArgs e) => OnBackColorChanged(e);
        internal void RaiseFore(EventArgs e) => OnForeColorChanged(e);
        internal void RaiseParentFont(EventArgs e) => OnParentFontChanged(e);
        internal void RaiseParentBack(EventArgs e) => OnParentBackColorChanged(e);
        internal void RaiseParentFore(EventArgs e) => OnParentForeColorChanged(e);
        private void Before(string kind, EventArgs e)
        {
            Trace("on-" + kind + "-enter:" + Id(this) + ":" + EventText(e));
            if (ThrowKind == kind) throw OriginalFailure;
            if (CallbackKind == kind && Callback is { } callback) { Callback = null; callback(); }
        }
        private void After(string kind) => Trace("on-" + kind + "-exit:" + Id(this));
        protected override void OnFontChanged(EventArgs e) { Before("font", e); if (SkipKind != "font") base.OnFontChanged(e); After("font"); }
        protected override void OnBackColorChanged(EventArgs e) { Before("back", e); if (SkipKind != "back") base.OnBackColorChanged(e); After("back"); }
        protected override void OnForeColorChanged(EventArgs e) { Before("fore", e); if (SkipKind != "fore") base.OnForeColorChanged(e); After("fore"); }
        protected override void OnParentFontChanged(EventArgs e) { Before("parent-font", e); if (SkipKind != "parent-font") base.OnParentFontChanged(e); After("parent-font"); }
        protected override void OnParentBackColorChanged(EventArgs e) { Before("parent-back", e); if (SkipKind != "parent-back") base.OnParentBackColorChanged(e); After("parent-back"); }
        protected override void OnParentForeColorChanged(EventArgs e) { Before("parent-fore", e); if (SkipKind != "parent-fore") base.OnParentForeColorChanged(e); After("parent-fore"); }
    }

    private sealed class GetterControl : Control
    {
        internal Font SyntheticFont = null!;
        public override Font Font { get { Trace("virtual-font-get"); return SyntheticFont; } set => base.Font = value; }
        public override Color BackColor { get { Trace("virtual-back-get"); return Color.PeachPuff; } set => base.BackColor = value; }
        public override Color ForeColor { get { Trace("virtual-fore-get"); return Color.DarkMagenta; } set => base.ForeColor = value; }
    }

    private sealed class PropertyControl : ProbeControl
    {
        public override Font Font { get { Trace("virtual-font-get"); return base.Font; } set { Trace("virtual-font-set:" + (value is null ? "null" : FontText(value))); base.Font = value!; } }
        public override Color BackColor { get { Trace("virtual-back-get"); return base.BackColor; } set { Trace("virtual-back-set:" + ColorText(value)); base.BackColor = value; } }
        public override Color ForeColor { get { Trace("virtual-fore-get"); return base.ForeColor; } set { Trace("virtual-fore-set:" + ColorText(value)); base.ForeColor = value; } }
    }

    private sealed class ProbeForm : Form { internal void Rebuild() => RecreateHandle(); }
    private sealed class GetterTextBox : TextBox
    {
        public override Color BackColor { get => Color.LightCyan; set => base.BackColor = value; }
        public override Color ForeColor { get => Color.DarkViolet; set => base.ForeColor = value; }
    }
    private sealed class ProbeRecursionException : Exception { }

    private static void Pump()
    {
        long deadline = Environment.TickCount64 + 2000;
        int count = 0;
        while (PeekMessageW(out NativeMessage message, 0, 0, 0, 1))
        {
            if (message.Message == 0x0012) { PostQuitMessage(unchecked((int)message.WParam)); return; }
            if (++count > 4096 || Environment.TickCount64 > deadline) throw new TimeoutException("Appearance message pump did not become idle.");
            _ = TranslateMessage(in message); _ = DispatchMessageW(in message);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeMessage { public nint HWnd; public uint Message; public nuint WParam; public nint LParam; public uint Time; public Point Point; public uint Private; }
    [DllImport("user32.dll")] private static extern nint SendMessageW(nint window, uint message, nint wParam, nint lParam);
    [DllImport("gdi32.dll", SetLastError = true)] private static extern int GetObjectW(nint value, int count, nint buffer);
    [DllImport("gdi32.dll", SetLastError = true)] private static extern nint CreateCompatibleDC(nint dc);
    [DllImport("gdi32.dll")] private static extern uint GetBkColor(nint dc);
    [DllImport("gdi32.dll")] private static extern uint GetTextColor(nint dc);
    [DllImport("gdi32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool DeleteDC(nint dc);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(nint window);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool PeekMessageW(out NativeMessage message, nint window, uint minimum, uint maximum, uint remove);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool TranslateMessage(in NativeMessage message);
    [DllImport("user32.dll")] private static extern nint DispatchMessageW(in NativeMessage message);
    [DllImport("user32.dll")] private static extern void PostQuitMessage(int exitCode);
}
