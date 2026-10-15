using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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

internal static class Program
{
    private static readonly List<string> Results = [];
    private static readonly InvalidOperationException Failure = new("text reset probe");
    private static readonly EventArgs CustomArgs = new();

    [STAThread]
    private static void Main()
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
#if REFERENCE
        string? version = typeof(Control).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        if (version is null || !version.StartsWith("10.0.11+", StringComparison.Ordinal))
            throw new InvalidOperationException($"Expected WinForms 10.0.11, found {version}.");
#endif
        RecordMetadata();
        WindowsFormsSynchronizationContext.AutoInstall = false;
        SynchronizationContext.SetSynchronizationContext(null);
        string?[] texts = [null, "", "plain", "virtual", "中文🙂", "line\r\nnext\n", "\0", "before\0after"];
        foreach (bool disposed in new[] { false, true })
        foreach (string? initial in texts)
        foreach (string? input in texts)
        {
            using var control = Create(initial, "normal", disposed);
            string prefix = $"base-{disposed}-{Encode(initial)}-{Encode(input)}";
            Observe(prefix + "-set", control, () => control.Text = input, input);
            Observe(prefix + "-repeat", control, () => control.Text = input, input);
            string? copy = input is null ? null : new string(input.ToCharArray());
            Observe(prefix + "-equal-copy", control, () => control.Text = copy, copy);
            Observe(prefix + "-reset", control, control.ResetText, string.Empty);
            Observe(prefix + "-reset-again", control, control.ResetText, string.Empty);
        }

        foreach (string mode in new[] { "normal", "fixed-getter", "empty-getter", "null-getter", "skip-setter",
            "suffix-setter", "skip-notification", "skip-reset", "hidden" })
        foreach (bool disposed in new[] { false, true })
        foreach (string? input in texts)
        {
            using var control = Create("seed", mode, disposed);
            string prefix = $"derived-{mode}-{disposed}-{Encode(input)}";
            Observe(prefix + "-set", control, () => control.Text = input, input);
            Observe(prefix + "-reset", control, control.ResetText, string.Empty);
            Observe(prefix + "-base-reset", control, control.BaseReset, string.Empty);
        }

        string[] points = ["enter:set", "get:text", "exit:set", "enter:reset", "exit:reset",
            "enter:notify", "event:first", "event:second", "exit:notify"];
        foreach (string operation in new[] { "set", "reset", "raise" })
        foreach (string point in points)
        foreach (string action in new[] { "throw", "set", "reset", "dispose", "remove-second", "add-third" })
        {
            using var control = Create("seed", "normal", false);
            control.ActionPoint = point;
            control.Action = action;
            Observe($"action-{operation}-{point}-{action}", control,
                operation == "set" ? () => control.Text = "outer" : operation == "reset" ? control.ResetText : () => control.Raise(CustomArgs),
                operation == "set" ? "outer" : operation == "reset" ? string.Empty : null);
        }

        foreach (bool disposed in new[] { false, true })
        foreach (bool throughOverride in new[] { false, true })
        foreach (EventArgs? arguments in new EventArgs?[] { null, EventArgs.Empty, CustomArgs })
        {
            using var control = Create("unchanged", "normal", disposed);
            Observe($"raise-{disposed}-{throughOverride}-{Args(arguments)}", control,
                () => { if (throughOverride) control.Raise(arguments); else control.BaseRaise(arguments); }, null);
        }

        using (var control = Create("seed", "normal", false))
        {
            Observe("event-remove", control, () => { control.TextChanged -= control.Second; control.Text = "one"; }, "one");
            Observe("event-add", control, () => { control.TextChanged += control.Second; control.Text = "two"; }, "two");
            Observe("event-duplicate", control, () => { control.TextChanged += control.Second; control.Text = "three"; }, "three");
            Observe("event-remove-last-duplicate", control, () => { control.TextChanged -= control.Second; control.Text = "four"; }, "four");
        }
        Console.WriteLine(string.Join(Environment.NewLine, Results));
    }

    [DynamicDependency("get_Text()", typeof(Control))]
    [DynamicDependency("set_Text(System.String)", typeof(Control))]
    [DynamicDependency("add_TextChanged(System.EventHandler)", typeof(Control))]
    [DynamicDependency("remove_TextChanged(System.EventHandler)", typeof(Control))]
    [DynamicDependency("OnTextChanged(System.EventArgs)", typeof(Control))]
    [DynamicDependency("ResetText()", typeof(Control))]
    private static void RecordMetadata()
    {
        PropertyInfo property = typeof(Control).GetProperty(nameof(Control.Text), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        EventInfo changed = typeof(Control).GetEvent(nameof(Control.TextChanged), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        MethodInfo notification = typeof(Control).GetMethod("OnTextChanged", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        MethodInfo reset = typeof(Control).GetMethod(nameof(Control.ResetText), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        Record("Text-shape", $"type:{property.PropertyType.FullName}|index:{property.GetIndexParameters().Length}");
        Record("Text-attributes", Attributes(property));
        Record("TextChanged-shape", $"type:{changed.EventHandlerType!.FullName}|multicast:{changed.IsMulticast}");
        Record("TextChanged-attributes", Attributes(changed));
        foreach (MethodInfo method in new[] { property.GetMethod!, property.SetMethod!, changed.AddMethod!, changed.RemoveMethod!, notification, reset })
        {
            Record(method.Name + "-shape", $"public:{method.IsPublic}|family:{method.IsFamily}|static:{method.IsStatic}|virtual:{method.IsVirtual}|final:{method.IsFinal}|abstract:{method.IsAbstract}|special:{method.IsSpecialName}|new-slot:{(method.Attributes & MethodAttributes.NewSlot) != 0}|base-self:{method.GetBaseDefinition() == method}|return:{method.ReturnType.FullName}");
            Record(method.Name + "-parameters", string.Join(";", method.GetParameters().Select(p =>
                $"{p.Position}:{p.ParameterType.FullName}:{p.Name}:flags:{(int)p.Attributes}:optional:{p.IsOptional}:default:{p.HasDefaultValue}")));
            Record(method.Name + "-attributes", Attributes(method));
        }
        // Compare the effective source contract, retaining the compiler's raw
        // nullable-context placement separately rather than normalizing it away.
        Record("Text-get-nullability", NullableContract(property.GetMethod!, property.GetMethod!.ReturnParameter, property.CustomAttributes));
        Record("Text-set-nullability", NullableContract(property.SetMethod!, property.SetMethod!.GetParameters()[0], property.CustomAttributes));
    }

    private static string Attributes(MemberInfo member) => string.Join(";", member.GetCustomAttributes().Select(attribute => attribute switch
    {
        AllowNullAttribute => "AllowNull",
        BrowsableAttribute value => "Browsable=" + value.Browsable,
        LocalizableAttribute value => "Localizable=" + value.IsLocalizable,
        BindableAttribute value => "Bindable=" + value.Bindable + ",Direction=" + value.Direction,
        DefaultValueAttribute value => "DefaultValue=" + (value.Value is null ? "null" : Encode(value.Value.ToString())),
        DesignerSerializationVisibilityAttribute value => "DesignerSerializationVisibility=" + value.Visibility,
        EditorBrowsableAttribute value => "EditorBrowsable=" + value.State,
        CategoryAttribute value => "Category=" + value.Category,
        DescriptionAttribute value => "Description=" + value.Description,
        DispIdAttribute value => "DispId=" + value.Value,
        _ => null
    }).Where(value => value is not null).Order(StringComparer.Ordinal));

    private static string NullableContract(MethodInfo method, ParameterInfo parameter, IEnumerable<CustomAttributeData> propertyAttributes)
    {
        byte? parameterFlag = NullableFlag(parameter.CustomAttributes, "NullableAttribute");
        byte? propertyFlag = NullableFlag(propertyAttributes, "NullableAttribute");
        byte? methodFlag = NullableFlag(method.CustomAttributes, "NullableContextAttribute");
        byte? typeFlag = NullableFlag(typeof(Control).CustomAttributes, "NullableContextAttribute");
        bool allowNull = parameter.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(AllowNullAttribute));
        Console.Error.WriteLine($"{method.Name}-nullable-encoding=parameter:{parameterFlag}|property:{propertyFlag}|method:{methodFlag}|type:{typeFlag}|allow-null:{allowNull}");
        return $"flag:{parameterFlag ?? propertyFlag ?? methodFlag ?? typeFlag ?? 0}|allow-null:{allowNull}";
    }

    private static byte? NullableFlag(IEnumerable<CustomAttributeData> attributes, string name)
    {
        CustomAttributeData? attribute = attributes.FirstOrDefault(data => data.AttributeType.FullName == "System.Runtime.CompilerServices." + name);
        if (attribute is null || attribute.ConstructorArguments.Count == 0) return null;
        object? value = attribute.ConstructorArguments[0].Value;
        if (value is IEnumerable<CustomAttributeTypedArgument> flags) value = flags.FirstOrDefault().Value;
        return value is byte flag ? flag : null;
    }

    private static TraceControl Create(string? initial, string mode, bool disposed)
    {
        TraceControl control = mode == "hidden" ? new HiddenControl() : new TraceControl();
        control.Text = initial;
        control.Bounds = new Rectangle(11, 13, 71, 43);
        control.Mode = mode;
        if (disposed) control.Dispose();
        control.TextChanged += control.First;
        control.TextChanged += control.Second;
        control.HandleCreated += (_, _) => control.Point("event:handle");
        control.LocationChanged += (_, _) => control.Point("event:location");
        control.Move += (_, _) => control.Point("event:move");
        control.SizeChanged += (_, _) => control.Point("event:size");
        control.Resize += (_, _) => control.Point("event:resize");
        control.ClientSizeChanged += (_, _) => control.Point("event:client");
        return control;
    }

    private static void Observe(string name, TraceControl control, Action operation, string? input)
    {
        string before = control.BaseText;
        control.Calls = [];
        Exception? failure = null;
        try { operation(); }
        catch (Exception exception) { failure = exception; }
        List<string> calls = control.Calls;
        control.Calls = null;
        control.ActionPoint = null;
        Record(name + "-calls", string.Join(";", calls));
        Record(name + "-exception", failure is null ? "none" : ReferenceEquals(failure, Failure) ? "original" : failure.GetType().Name);
        Record(name + "-state", State(control) + "|virtual:" + Encode(control.Text)
            + "|same-input:" + ReferenceEquals(control.BaseText, input)
            + "|same-before:" + ReferenceEquals(control.BaseText, before)
            + "|same-empty:" + ReferenceEquals(control.BaseText, string.Empty));
    }

    private static string State(TraceControl control) => "text:" + Encode(control.BaseText)
        + $"|bounds:{control.Left},{control.Top},{control.Width},{control.Height}|client:{control.ClientSize.Width},{control.ClientSize.Height}"
        + $"|handle:{control.IsHandleCreated}|parent:{control.Parent is not null}|disposed:{control.IsDisposed}|disposing:{control.Disposing}";
    private static string Encode(string? value) => value is null ? "null" : value.Length + ":" + string.Concat(value.Select(character => ((int)character).ToString("X4", CultureInfo.InvariantCulture)));
    private static string Args(EventArgs? value) => value is null ? "null" : ReferenceEquals(value, EventArgs.Empty) ? "empty" : ReferenceEquals(value, CustomArgs) ? "custom" : "other";
    private static void Record(string name, string value) => Results.Add(name + "=" + value);

    private class TraceControl : Control
    {
        internal string Mode = "normal";
        internal List<string>? Calls;
        internal string? ActionPoint;
        internal string Action = "none";
        internal string BaseText => base.Text;
        internal void BaseReset() => base.ResetText();
        internal void Raise(EventArgs? e) => OnTextChanged(e!);
        internal void BaseRaise(EventArgs? e) => base.OnTextChanged(e!);
        internal void First(object? sender, EventArgs e) => Point("event:first", $"sender:{ReferenceEquals(sender, this)},args:{Args(e)}");
        internal void Second(object? sender, EventArgs e) => Point("event:second", $"sender:{ReferenceEquals(sender, this)},args:{Args(e)}");
        internal void Third(object? sender, EventArgs e) => Point("event:third", $"sender:{ReferenceEquals(sender, this)},args:{Args(e)}");

        internal void Point(string point, string? argument = null)
        {
            if (Calls is null) return;
            Calls.Add(point + (argument is null ? "" : ":" + argument) + ":" + State(this));
            if (ActionPoint != point) return;
            ActionPoint = null;
            switch (Action)
            {
                case "throw": throw Failure;
                case "set": Text = "reentered"; break;
                case "reset": ResetText(); break;
                case "dispose": Dispose(); break;
                case "remove-second": TextChanged -= Second; break;
                case "add-third": TextChanged += Third; break;
            }
        }

        [AllowNull]
        public override string Text
        {
            get { Point("get:text"); return Mode switch { "fixed-getter" => "virtual", "empty-getter" => "", "null-getter" => null!, _ => base.Text }; }
            set
            {
                Point("enter:set", Encode(value));
                if (Mode != "skip-setter") base.Text = Mode == "suffix-setter" ? value + "-suffix" : value;
                Point("exit:set", Encode(value));
            }
        }
        public override void ResetText()
        {
            Point("enter:reset");
            if (Mode != "skip-reset") base.ResetText();
            Point("exit:reset");
        }
        protected override void OnTextChanged(EventArgs e)
        {
            Point("enter:notify", Args(e));
            if (Mode != "skip-notification") base.OnTextChanged(e);
            Point("exit:notify", Args(e));
        }
#if !REFERENCE
        protected override void OnBoundsChanged() { }
#endif
    }

    private sealed class HiddenControl : TraceControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get => throw Failure; set => throw Failure; }
        public new void ResetText() => throw Failure;
        public new event EventHandler? TextChanged { add => throw Failure; remove => throw Failure; }
        protected new void OnTextChanged(EventArgs e) => throw Failure;
    }
}
