using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlTextResetTests
{
    [Fact]
    public void TextMembers_ExposeStandardShapesAndEnglishMetadata()
    {
        PropertyInfo text = typeof(Control).GetProperty(nameof(Control.Text))!;
        Assert.Equal(typeof(string), text.PropertyType);
        Assert.Empty(text.GetIndexParameters());
        Assert.Equal("Appearance", text.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal("The text associated with the control.", text.GetCustomAttribute<DescriptionAttribute>()!.Description);
        Assert.True(text.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
        Assert.True(text.GetCustomAttribute<BindableAttribute>()!.Bindable);
        Assert.Equal(BindingDirection.OneWay, text.GetCustomAttribute<BindableAttribute>()!.Direction);
        Assert.Equal(-517, text.GetCustomAttribute<DispIdAttribute>()!.Value);
        Assert.Null(text.GetCustomAttribute<DefaultValueAttribute>());
        Assert.NotNull(text.SetMethod!.GetParameters()[0].GetCustomAttribute<AllowNullAttribute>());
        Assert.Equal(NullabilityState.NotNull, new NullabilityInfoContext().Create(text).ReadState);
        Assert.Equal(NullabilityState.Nullable, new NullabilityInfoContext().Create(text).WriteState);

        EventInfo changed = typeof(Control).GetEvent(nameof(Control.TextChanged))!;
        Assert.Equal(typeof(EventHandler), changed.EventHandlerType);
        Assert.Equal("Property Changed", changed.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal("Event raised when the value of the Text property is changed on Control.", changed.GetCustomAttribute<DescriptionAttribute>()!.Description);
        Assert.False(changed.AddMethod!.IsVirtual);
        Assert.False(changed.RemoveMethod!.IsVirtual);
        Assert.True(changed.AddMethod.IsPublic);
        Assert.True(changed.RemoveMethod.IsPublic);

        MethodInfo notify = typeof(Control).GetMethod("OnTextChanged", BindingFlags.Instance | BindingFlags.NonPublic)!;
        Assert.True(notify.IsFamily);
        Assert.Equal(EditorBrowsableState.Advanced, notify.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
        Assert.Equal("e", Assert.Single(notify.GetParameters()).Name);
        Assert.Equal(typeof(EventArgs), Assert.Single(notify.GetParameters()).ParameterType);
        MethodInfo reset = typeof(Control).GetMethod(nameof(Control.ResetText))!;
        Assert.True(reset.IsPublic);
        Assert.Empty(reset.GetParameters());
        Assert.Empty(reset.CustomAttributes);
        Assert.Equal(typeof(void), reset.ReturnType);
        foreach (MethodInfo method in new[] { text.GetMethod!, text.SetMethod!, notify, reset })
        {
            Assert.True(method.IsVirtual);
            Assert.False(method.IsFinal);
            Assert.False(method.IsStatic);
            Assert.False(method.IsAbstract);
            Assert.Equal(method, method.GetBaseDefinition());
            Assert.NotEqual(0, (int)(method.Attributes & MethodAttributes.NewSlot));
        }
    }

    public static IEnumerable<object?[]> TextCases()
    {
        foreach (bool disposed in new[] { false, true })
        foreach (string? initial in Texts())
        foreach (string? input in Texts())
            yield return [disposed, initial, input];
    }

    [Theory]
    [MemberData(nameof(TextCases))]
    public void SetAndReset_PreserveFullManagedTextIdentityAndOnlyNotifyOnChanges(bool disposed, string? initial, string? input)
        => OnSta(() =>
        {
            using TraceControl control = Create(initial, disposed: disposed);
            string original = control.BaseText;
            string expected = input ?? string.Empty;
            bool changed = original != expected;
            control.Text = input;
            Assert.Equal(expected, control.BaseText);
            Assert.Same(changed ? expected : original, control.BaseText);
            Assert.Equal(changed ? 1 : 0, control.Calls.Count(call => call.Point == "event:first"));
            Assert.All(control.Calls.Where(call => call.Point == "event:first" || call.Point == "event:second"), call =>
            {
                Assert.Equal(expected, call.RawText);
                Assert.Same(EventArgs.Empty, call.Arguments);
            });
            Assert.Equal(changed
                ? ["enter:set", "get:text", "enter:notify", "event:first", "event:second", "exit:notify", "exit:set"]
                : new[] { "enter:set", "get:text", "exit:set" }, Points(control));

            string stored = control.BaseText;
            string? equalCopy = input is null ? null : new string(input.ToCharArray());
            control.Calls.Clear();
            control.Text = equalCopy;
            Assert.Same(stored, control.BaseText);
            Assert.Equal(["enter:set", "get:text", "exit:set"], Points(control));

            control.Calls.Clear();
            control.ResetText();
            Assert.Same(string.Empty, control.BaseText);
            Assert.Equal(expected.Length == 0 ? 0 : 1, control.Calls.Count(call => call.Point == "event:first"));
            Assert.Equal("enter:reset", control.Calls[0].Point);
            Assert.Same(string.Empty, Assert.Single(control.Calls, call => call.Point == "enter:set").Input);
            Assert.Equal("exit:reset", control.Calls[^1].Point);
            control.Calls.Clear();
            control.ResetText();
            Assert.Equal(["enter:reset", "enter:set", "get:text", "exit:set", "exit:reset"], Points(control));
            AssertUnchangedGeometry(control, disposed);
        });

    public static IEnumerable<object[]> ResetModes()
    {
        foreach (bool disposed in new[] { false, true })
        foreach (string mode in new[] { "normal", "fixed-getter", "empty-getter", "null-getter", "skip-setter", "suffix-setter", "skip-notification", "skip-reset", "hidden" })
            yield return [disposed, mode];
    }

    [Theory]
    [MemberData(nameof(ResetModes))]
    public void ResetText_ConsumesVirtualSlotsAndHonorsOverridesWithoutBase(bool disposed, string mode)
        => OnSta(() =>
        {
            using TraceControl control = Create("seed", mode, disposed);
            control.ResetText();
            string expected = mode is "skip-reset" or "skip-setter" or "empty-getter" ? "seed"
                : mode == "suffix-setter" ? "-suffix" : string.Empty;
            Assert.Equal(expected, control.BaseText);
            Assert.Equal(mode == "skip-reset" ? 0 : 1, control.Calls.Count(call => call.Point == "enter:set"));
            Assert.Equal(mode is "skip-reset" or "skip-setter" ? 0 : 1, control.Calls.Count(call => call.Point == "get:text"));
            Assert.Equal(mode is "skip-reset" or "skip-setter" or "skip-notification" or "empty-getter" ? 0 : 1,
                control.Calls.Count(call => call.Point == "event:first"));
            control.Calls.Clear();
            control.BaseReset();
            Assert.DoesNotContain(control.Calls, call => call.Point is "enter:reset" or "exit:reset");
            Assert.Single(control.Calls, call => call.Point == "enter:set");
            Assert.Equal(mode == "skip-reset" ? string.Empty : expected, control.BaseText);
            Assert.Equal(mode is "fixed-getter" or "null-getter" or "skip-reset" ? 1 : 0,
                control.Calls.Count(call => call.Point == "event:first"));
            AssertUnchangedGeometry(control, disposed);
        });

    [Theory]
    [InlineData("fixed-getter", "virtual", "seed", 0)]
    [InlineData("empty-getter", null, "seed", 0)]
    [InlineData("null-getter", null, "", 1)]
    [InlineData("skip-setter", "virtual", "seed", 0)]
    [InlineData("suffix-setter", null, "-suffix", 1)]
    public void Setter_UsesVirtualGetterForEqualityInsteadOfTheStoredText(string mode, string? input, string expected, int events)
        => OnSta(() =>
        {
            using TraceControl control = Create("seed", mode);
            control.Text = input;
            Assert.Equal(expected, control.BaseText);
            Assert.Equal(events, control.Calls.Count(call => call.Point == "event:first"));
            control.Calls.Clear();
            control.Text = input;
            Assert.Equal(mode == "null-getter" ? 1 : 0, control.Calls.Count(call => call.Point == "event:first"));
        });

    public static IEnumerable<object[]> ExceptionPoints()
    {
        foreach (bool reset in new[] { false, true })
        foreach (string point in new[] { "enter:set", "get:text", "enter:notify", "event:first", "event:second", "exit:notify", "exit:set", "enter:reset", "exit:reset" })
            if (reset || point is not ("enter:reset" or "exit:reset")) yield return [reset, point];
    }

    [Theory]
    [MemberData(nameof(ExceptionPoints))]
    public void CallbackExceptions_PropagateTheOriginalInstanceAndKeepAlreadyCommittedText(bool reset, string point)
        => OnSta(() =>
        {
            using TraceControl control = Create("seed");
            var failure = new InvalidOperationException("original text failure");
            control.AtPoint = (_, current) => { if (current == point) throw failure; };
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => { if (reset) control.ResetText(); else control.Text = "outer"; }));
            control.AtPoint = null;
            Assert.Equal(point is "enter:reset" or "enter:set" or "get:text" ? "seed" : reset ? string.Empty : "outer", control.BaseText);
            Assert.Equal(point, control.Calls[^1].Point);
            AssertUnchangedGeometry(control, false);
        });

    public static IEnumerable<object[]> ReentryPoints()
    {
        foreach (bool reset in new[] { false, true })
        foreach (string point in new[] { "enter:set", "get:text", "enter:notify", "event:first", "event:second", "exit:notify", "exit:set" })
        foreach (string operation in new[] { "text", "reset", "dispose" })
            yield return [reset, point, operation];
    }

    [Theory]
    [MemberData(nameof(ReentryPoints))]
    public void ReentrantChanges_ContinueFromTheCommittedStateWithoutRollingBack(bool reset, string point, string operation)
        => OnSta(() =>
        {
            using TraceControl control = Create("seed");
            control.AtPoint = (current, observed) =>
            {
                if (observed != point) return;
                current.AtPoint = null;
                if (operation == "text") current.Text = "reentered";
                else if (operation == "reset") current.ResetText();
                else current.Dispose();
            };
            if (reset) control.ResetText(); else control.Text = "outer";
            control.AtPoint = null;
            bool beforeCommit = point is "enter:set" or "get:text";
            string expected = operation == "dispose" || beforeCommit ? reset ? string.Empty : "outer"
                : operation == "text" ? "reentered" : string.Empty;
            Assert.Equal(expected, control.BaseText);
            AssertUnchangedGeometry(control, operation == "dispose");
            if (!reset && point == "exit:set" && operation == "reset")
            {
                // The outer setter records its exit before the callback enters
                // ResetText. The nested reset therefore supplies the final call.
                Assert.Equal(["enter:set", "get:text", "enter:notify", "event:first", "event:second", "exit:notify", "exit:set",
                    "enter:reset", "enter:set", "get:text", "enter:notify", "event:first", "event:second", "exit:notify", "exit:set", "exit:reset"], Points(control));
            }
            else
            {
                Assert.Equal(reset ? "exit:reset" : "exit:set", control.Calls[^1].Point);
            }
        });

    [Theory]
    [InlineData(false, "null")]
    [InlineData(false, "empty")]
    [InlineData(false, "custom")]
    [InlineData(true, "null")]
    [InlineData(true, "empty")]
    [InlineData(true, "custom")]
    public void OnTextChanged_ForwardsExactEventArgumentsWithoutReadingTextOrChangingState(bool disposed, string kind)
        => OnSta(() =>
        {
            using TraceControl control = Create("seed", disposed: disposed);
            EventArgs? arguments = kind == "null" ? null : kind == "empty" ? EventArgs.Empty : new EventArgs();
            control.BaseRaise(arguments);
            Assert.Equal(["event:first", "event:second"], Points(control));
            Assert.All(control.Calls, call => Assert.Same(arguments, call.Arguments));
            Assert.Equal("seed", control.BaseText);
            AssertUnchangedGeometry(control, disposed);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EventSubscriptionChanges_ApplyToTheNextInvocation(bool add)
        => OnSta(() =>
        {
            using TraceControl control = Create("seed");
            EventHandler third = (_, e) => control.Record("event:third", arguments: e);
            control.AtPoint = (current, point) =>
            {
                if (point != "event:first") return;
                current.AtPoint = null;
                if (add) current.TextChanged += third; else current.TextChanged -= current.Second;
            };
            control.Text = "first";
            Assert.Equal(["event:first", "event:second"], control.Calls.Where(call => call.Point.StartsWith("event:", StringComparison.Ordinal)).Select(call => call.Point));
            control.Calls.Clear();
            control.Text = "second";
            Assert.Equal(add ? ["event:first", "event:second", "event:third"] : new[] { "event:first" },
                control.Calls.Where(call => call.Point.StartsWith("event:", StringComparison.Ordinal)).Select(call => call.Point));
        });

    [Fact]
    public void DuplicateHandlers_AreRemovedOneAtATime()
        => OnSta(() =>
        {
            using TraceControl control = Create("seed");
            control.TextChanged += control.Second;
            control.Text = "one";
            Assert.Equal(2, control.Calls.Count(call => call.Point == "event:second"));
            control.TextChanged -= control.Second;
            control.Calls.Clear();
            control.Text = "two";
            Assert.Single(control.Calls, call => call.Point == "event:second");
        });

    private static string?[] Texts() => [null, "", "plain", "virtual", "中文🙂", "line\r\nnext\n", "\0", "before\0after"];
    private static string[] Points(TraceControl control) => control.Calls.Select(call => call.Point).ToArray();
    private static TraceControl Create(string? initial, string mode = "normal", bool disposed = false)
    {
        TraceControl control = mode == "hidden" ? new HiddenControl() : new TraceControl();
        control.Text = initial;
        control.Bounds = new Rectangle(11, 13, 71, 43);
        control.Mode = mode;
        if (disposed) control.Dispose();
        control.TextChanged += control.First;
        control.TextChanged += control.Second;
        control.Calls.Clear();
        return control;
    }
    private static void AssertUnchangedGeometry(TraceControl control, bool disposed)
    {
        Assert.Equal(new Rectangle(11, 13, 71, 43), control.Bounds);
        Assert.Equal(new Size(71, 43), control.ClientSize);
        Assert.False(control.IsHandleCreated);
        Assert.Null(control.Parent);
        Assert.Equal(disposed, control.IsDisposed);
        Assert.False(control.Disposing);
    }
    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            SynchronizationContext.SetSynchronizationContext(null);
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The text reset check timed out.");
        if (failure is not null) ExceptionDispatchInfo.Capture(failure).Throw();
    }
    private sealed record Call(string Point, string RawText, string? Input, EventArgs? Arguments);
    private class TraceControl : Control
    {
        internal string Mode = "normal";
        internal readonly List<Call> Calls = [];
        internal Action<TraceControl, string>? AtPoint;
        internal string BaseText => base.Text;
        internal void BaseReset() => base.ResetText();
        internal void BaseRaise(EventArgs? arguments) => base.OnTextChanged(arguments!);
        internal void First(object? sender, EventArgs e) { Assert.Same(this, sender); Record("event:first", arguments: e); }
        internal void Second(object? sender, EventArgs e) { Assert.Same(this, sender); Record("event:second", arguments: e); }
        internal void Record(string point, string? input = null, EventArgs? arguments = null)
        {
            Calls.Add(new(point, base.Text, input, arguments));
            AtPoint?.Invoke(this, point);
        }
        [AllowNull]
        public override string Text
        {
            get { Record("get:text"); return Mode switch { "fixed-getter" => "virtual", "empty-getter" => "", "null-getter" => null!, _ => base.Text }; }
            set
            {
                Record("enter:set", value);
                if (Mode != "skip-setter") base.Text = Mode == "suffix-setter" ? value + "-suffix" : value;
                Record("exit:set", value);
            }
        }
        public override void ResetText()
        {
            Record("enter:reset");
            if (Mode != "skip-reset") base.ResetText();
            Record("exit:reset");
        }
        protected override void OnTextChanged(EventArgs e)
        {
            Record("enter:notify", arguments: e);
            if (Mode != "skip-notification") base.OnTextChanged(e);
            Record("exit:notify", arguments: e);
        }
        protected override void OnBoundsChanged() { }
    }
    private sealed class HiddenControl : TraceControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get => throw new InvalidOperationException("hidden getter"); set => throw new InvalidOperationException("hidden setter"); }
        public new void ResetText() => throw new InvalidOperationException("hidden reset");
        public new event EventHandler? TextChanged { add => throw new InvalidOperationException("hidden add"); remove => throw new InvalidOperationException("hidden remove"); }
        protected new void OnTextChanged(EventArgs e) => throw new InvalidOperationException("hidden notification");
    }
}
