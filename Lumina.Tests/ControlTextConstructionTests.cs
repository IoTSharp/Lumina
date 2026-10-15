using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlTextConstructionTests
{
    [ThreadStatic] private static Settings? Current;

    [Fact]
    public void TextConstructors_ExposeTheStandardPublicParametersAndTextVirtualSlots()
    {
        Type[][] signatures = [[typeof(string)], [typeof(string), typeof(int), typeof(int), typeof(int), typeof(int)]];
        string[][] names = [["text"], ["text", "left", "top", "width", "height"]];
        for (int index = 0; index < signatures.Length; index++)
        {
            ConstructorInfo constructor = typeof(Control).GetConstructor(signatures[index])!;
            Assert.NotNull(constructor);
            Assert.True(constructor.IsPublic);
            Assert.False(constructor.IsStatic);
            Assert.True(constructor.IsConstructor);
            CallingConventions calling = constructor.CallingConvention;
            Assert.NotEqual(0, (int)(calling & CallingConventions.HasThis));
            Assert.Equal(0, (int)(calling & (CallingConventions.VarArgs | CallingConventions.ExplicitThis)));
            Assert.Equal(0, (int)(calling & ~(CallingConventions.Standard | CallingConventions.VarArgs
                | CallingConventions.HasThis | CallingConventions.ExplicitThis)));
            // NullableContext can be emitted on the type or the constructor.
            // Its placement is compiler metadata; the parameter contract is not.
            Assert.Empty(constructor.CustomAttributes.Where(attribute =>
                attribute.AttributeType.FullName != "System.Runtime.CompilerServices.NullableContextAttribute"));
            ParameterInfo[] parameters = constructor.GetParameters();
            Assert.Equal(names[index], parameters.Select(parameter => parameter.Name));
            Assert.Equal(signatures[index], parameters.Select(parameter => parameter.ParameterType));
            Assert.All(parameters, parameter =>
            {
                Assert.Equal(ParameterAttributes.None, parameter.Attributes);
                Assert.False(parameter.IsOptional);
                Assert.False(parameter.HasDefaultValue);
                Assert.Empty(parameter.CustomAttributes);
            });
            Assert.Equal((byte)2, EffectiveTextNullability(constructor));
        }

        PropertyInfo property = typeof(Control).GetProperty(nameof(Control.Text))!;
        Assert.Equal(typeof(string), property.PropertyType);
        Assert.Empty(property.GetIndexParameters());
        foreach (MethodInfo accessor in new[] { property.GetMethod!, property.SetMethod! })
        {
            Assert.True(accessor.IsPublic);
            Assert.True(accessor.IsVirtual);
            Assert.False(accessor.IsStatic);
            Assert.False(accessor.IsAbstract);
            Assert.False(accessor.IsFinal);
            Assert.NotEqual(0, (int)(accessor.Attributes & MethodAttributes.NewSlot));
            Assert.Equal(accessor, accessor.GetBaseDefinition());
        }
    }

    public static IEnumerable<object?[]> BaseCases()
    {
        foreach (string? text in Texts())
        {
            yield return [text, false, Rectangle.Empty];
            foreach (Rectangle rectangle in Rectangles()) { yield return [text, true, rectangle]; }
        }
    }

    [Theory]
    [MemberData(nameof(BaseCases))]
    public void BaseConstructors_PreserveManagedTextAndSignedBoundsWithoutCreatingAHandle(
        string? text, bool withBounds, Rectangle rectangle)
        => OnSta(() =>
        {
            using Control control = withBounds
                ? new Control(text, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height) : new Control(text);
            Assert.Equal(text ?? string.Empty, control.Text);
            Assert.Equal(rectangle, control.Bounds);
            Assert.Equal(rectangle.Size, control.Size);
            Assert.Equal(rectangle.Size, control.ClientSize);
            Assert.Equal(new Rectangle(Point.Empty, rectangle.Size), control.ClientRectangle);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, control.Handle);
            Assert.Null(SynchronizationContext.Current);
        });

    public static IEnumerable<object?[]> DispatchCases()
    {
        foreach (bool withBounds in new[] { false, true })
        {
            foreach (string? text in Texts()) { yield return [withBounds, text]; }
        }
    }

    [Theory]
    [MemberData(nameof(DispatchCases))]
    public void Construction_DispatchesTextBeforeSeparateLocationAndSizeOperations(bool withBounds, string? text)
        => OnSta(() =>
        {
            Settings settings = Configure();
            using TraceControl control = Create(withBounds, text);
            List<string> expected = ["default:min", "default:max", "default:size", "enter:set:text", "get:text"];
            if (!string.IsNullOrEmpty(text)) { expected.AddRange(["enter:TextChanged", "event:TextChanged", "exit:TextChanged"]); }
            expected.Add("exit:set:text");
            if (withBounds)
            {
                expected.AddRange(["enter:core", "enter:LocationChanged", "enter:Move", "event:Move", "exit:Move",
                    "event:LocationChanged", "exit:LocationChanged", "exit:core", "enter:core", "enter:SizeChanged",
                    "enter:Resize", "event:Resize", "exit:Resize", "event:SizeChanged", "exit:SizeChanged",
                    "enter:ClientSizeChanged", "event:ClientSizeChanged", "exit:ClientSizeChanged", "exit:core"]);
            }
            expected.Add("derived-constructor");
            Assert.Equal(expected, settings.Calls.Take(3).Concat(settings.Calls.Skip(3).Where(call =>
                call.Point is not "get:min" and not "get:max" and not "default:min" and not "default:max"))
                .Select(call => call.Point));
            Call textSetter = Assert.Single(settings.Calls, call => call.Point == "enter:set:text");
            Assert.Equal(text, textSetter.TextArgument);
            Assert.Equal(Rectangle.Empty, textSetter.Bounds);
            Assert.Equal(string.Empty, textSetter.RawText);
            Assert.All(settings.Calls.Where(call => call.Point is "enter:TextChanged" or "event:TextChanged"), call =>
            {
                Assert.Equal(text, call.RawText);
                Assert.Equal(Rectangle.Empty, call.Bounds);
            });
            Call[] cores = settings.Calls.Where(call => call.Point == "enter:core").ToArray();
            Assert.Equal(withBounds ? 2 : 0, cores.Length);
            if (withBounds)
            {
                Assert.Equal(new Rectangle(7, 9, 0, 0), cores[0].Request);
                Assert.Equal(BoundsSpecified.Location, cores[0].Specified);
                Assert.Equal(new Rectangle(7, 9, 83, 47), cores[1].Request);
                Assert.Equal(BoundsSpecified.Size, cores[1].Specified);
                Assert.Equal(new Rectangle(7, 9, 0, 0), cores[1].Bounds);
            }
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData("fixed", 31, 17, 83, 47)]
    [InlineData("conflict", 101, 59, 101, 59)]
    [InlineData("negative", -3, -5, -3, -5)]
    [InlineData("zero-axis", 0, 17, 83, 47)]
    public void FiveArgumentConstruction_ConsumesConstraintsDuringLocationBeforeApplyingTheRequestedSize(
        string profile, int firstWidth, int firstHeight, int finalWidth, int finalHeight)
        => OnSta(() =>
        {
            Settings settings = Configure(profile);
            using TraceControl control = Create(true, "plain");
            Call[] entries = settings.Calls.Where(call => call.Point == "enter:core").ToArray();
            Call[] exits = settings.Calls.Where(call => call.Point == "exit:core").ToArray();
            Assert.Equal(2, entries.Length);
            Assert.Equal(2, exits.Length);
            Assert.Equal(new Rectangle(7, 9, 0, 0), entries[0].Request);
            Assert.Equal(BoundsSpecified.Location, entries[0].Specified);
            Assert.Equal(new Rectangle(7, 9, firstWidth, firstHeight), exits[0].Bounds);
            Assert.Equal(exits[0].Bounds, entries[1].Bounds);
            Assert.Equal(new Rectangle(7, 9, 83, 47), entries[1].Request);
            Assert.Equal(BoundsSpecified.Size, entries[1].Specified);
            Assert.Equal(new Rectangle(7, 9, finalWidth, finalHeight), control.Bounds);
            Assert.Equal(new Size(finalWidth, finalHeight), control.ClientSize);
            Assert.Equal("plain", entries[0].RawText);
            Assert.Contains(settings.Calls.TakeWhile(call => call != exits[0]), call => call.Point == "event:SizeChanged");
            Assert.True(settings.Calls.IndexOf(Assert.Single(settings.Calls, call => call.Point == "exit:set:text"))
                < settings.Calls.IndexOf(entries[0]));
        });

    public static IEnumerable<object[]> TextOverrideCases()
    {
        foreach (bool withBounds in new[] { false, true })
        {
            foreach (string mode in new[] { "skip-text-setter", "fixed-text-getter", "null-text-getter", "text-suffix", "skip-text-notification", "hidden-text" })
            {
                yield return [withBounds, mode];
            }
        }
    }

    [Theory]
    [MemberData(nameof(TextOverrideCases))]
    public void Construction_UsesTheVirtualTextSlotAndHonorsOverridesThatDoNotCallBase(bool withBounds, string mode)
        => OnSta(() =>
        {
            Settings settings = Configure();
            settings.Mode = mode;
            using TraceControl control = Create(withBounds, "virtual-value");
            string raw = mode is "skip-text-setter" or "fixed-text-getter" ? string.Empty
                : mode == "text-suffix" ? "virtual-value-suffix" : "virtual-value";
            Assert.Equal(raw, control.BaseText);
            Assert.Equal(mode == "fixed-text-getter" ? "virtual-value" : mode == "null-text-getter" ? null : raw, control.Text);
            Assert.Single(settings.Calls, call => call.Point == "enter:set:text");
            Assert.Equal(mode is "skip-text-setter" or "fixed-text-getter" or "skip-text-notification" ? 0 : 1,
                settings.Calls.Count(call => call.Point == "event:TextChanged"));
            Assert.Equal(withBounds ? new Rectangle(7, 9, 83, 47) : Rectangle.Empty, control.Bounds);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void FiveArgumentConstruction_UsesTheBoundsActuallyCommittedByAnOverrideThatSkipsBase()
        => OnSta(() =>
        {
            Settings settings = Configure();
            settings.Mode = "skip-core";
            using TraceControl control = Create(true, "plain");
            Call[] entries = settings.Calls.Where(call => call.Point == "enter:core").ToArray();
            Assert.Equal(2, entries.Length);
            Assert.Equal(new Rectangle(7, 9, 0, 0), entries[0].Request);
            Assert.Equal(BoundsSpecified.Location, entries[0].Specified);
            Assert.Equal(new Rectangle(0, 0, 83, 47), entries[1].Request);
            Assert.Equal(BoundsSpecified.Size, entries[1].Specified);
            Assert.Equal(Rectangle.Empty, control.Bounds);
            Assert.Equal(Size.Empty, control.ClientSize);
            Assert.Equal("plain", control.BaseText);
            Assert.DoesNotContain(settings.Calls, call => call.Point == "event:LocationChanged" || call.Point == "event:SizeChanged");
        });

    public static IEnumerable<object[]> FailureCases()
    {
        foreach (bool withBounds in new[] { false, true })
        {
            foreach (string point in new[] { "default:min", "default:max", "default:size", "enter:set:text", "get:text",
                "enter:TextChanged", "event:TextChanged", "exit:TextChanged", "exit:set:text", "derived-constructor" })
            {
                yield return [withBounds, point];
            }
        }
        foreach (string point in new[] { "enter:core", "exit:core", "enter:LocationChanged", "enter:Move", "enter:SizeChanged", "enter:Resize", "enter:ClientSizeChanged" })
        {
            yield return [true, point];
        }
    }

    [Theory]
    [MemberData(nameof(FailureCases))]
    public void ConstructorFailures_PropagateTheOriginalExceptionAndPreserveTheActualPartialState(bool withBounds, string point)
        => OnSta(() =>
        {
            Settings settings = Configure("fixed");
            var failure = new InvalidOperationException("text constructor");
            settings.AtPoint = (_, observed) => { if (observed == point) { throw failure; } };
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => Create(withBounds, "outer")));
            settings.AtPoint = null;
            using TraceControl control = settings.Observed!;
            Assert.Equal(point, settings.Calls[^1].Point);
            bool emptyText = point is "default:min" or "default:max" or "default:size" or "enter:set:text" or "get:text";
            Assert.Equal(emptyText ? string.Empty : "outer", control.BaseText);
            Rectangle bounds = !withBounds || emptyText || point is "enter:TextChanged" or "event:TextChanged" or "exit:TextChanged" or "exit:set:text" or "enter:core"
                ? Rectangle.Empty : point == "derived-constructor" ? new(7, 9, 83, 47) : new(7, 9, 31, 17);
            Assert.Equal(bounds, control.Bounds);
            Assert.Equal(bounds.Size, control.ClientSize);
            Assert.False(control.IsHandleCreated);
        });

    public static IEnumerable<object[]> ReentryCases()
    {
        foreach (bool withBounds in new[] { false, true })
        {
            foreach (string point in new[] { "default:size", "enter:set:text", "get:text", "enter:TextChanged", "event:TextChanged", "exit:TextChanged", "exit:set:text", "derived-constructor" })
            {
                foreach (string action in new[] { "text", "bounds", "client" }) { yield return [withBounds, point, action]; }
            }
        }
        foreach (string point in new[] { "enter:core", "exit:core" })
        {
            foreach (string action in new[] { "text", "bounds", "client" }) { yield return [true, point, action]; }
        }
    }

    [Theory]
    [MemberData(nameof(ReentryCases))]
    public void ReentrantConstruction_PreservesCallbackChangesUntilTheNextActualConstructorOperation(
        bool withBounds, string point, string action)
        => OnSta(() =>
        {
            Settings settings = Configure("fixed");
            bool reached = false;
            settings.AtPoint = (control, observed) =>
            {
                if (observed != point) { return; }
                settings.AtPoint = null;
                reached = true;
                if (action == "text") { control.Text = "reentered"; }
                else if (action == "bounds") { control.Bounds = new Rectangle(17, 19, 41, 23); }
                else { control.ClientSize = new Size(19, 11); }
            };
            using TraceControl control = Create(withBounds, "outer");
            Assert.True(reached);
            Assert.Equal(action == "text" && point is not "default:size" and not "enter:set:text" and not "get:text" ? "reentered" : "outer", control.BaseText);
            Rectangle bounds;
            Size client;
            if (withBounds)
            {
                bounds = action == "bounds" && point == "derived-constructor" ? new(17, 19, 41, 23)
                    : action == "bounds" && point == "exit:core" ? new(17, 19, 83, 47)
                    : action == "client" && point == "derived-constructor" ? new(7, 9, 31, 17) : new(7, 9, 83, 47);
                client = action == "client" && point == "derived-constructor" ? new(19, 11) : bounds.Size;
            }
            else
            {
                bounds = action == "bounds" ? point == "default:size" ? new(17, 19, 0, 0) : new(17, 19, 41, 23)
                    : action == "client" && point != "default:size" ? new(0, 0, 31, 17) : Rectangle.Empty;
                client = action == "bounds" ? new(41, 23) : action == "client" ? new(19, 11) : Size.Empty;
            }
            Assert.Equal(bounds, control.Bounds);
            Assert.Equal(client, control.ClientSize);
            Assert.False(control.IsHandleCreated);
        });

    public static IEnumerable<object[]> ContextCases()
    {
        foreach (bool autoInstall in new[] { false, true })
        {
            foreach (string context in new[] { "null", "base", "custom" })
            {
                foreach (bool withBounds in new[] { false, true })
                {
                    foreach (string point in new[] { "none", "default:size", "enter:set:text", "get:text", "enter:TextChanged", "enter:core", "derived-constructor" })
                    {
                        yield return [autoInstall, context, withBounds, point];
                    }
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(ContextCases))]
    public void SynchronizationContext_InstallsAfterDefaultsAndBeforeTextEvenWhenLaterConstructorCodeThrows(
        bool autoInstall, string context, bool withBounds, string point)
        => OnSta(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = autoInstall;
            SynchronizationContext? previous = context == "custom" ? new CustomContext()
                : context == "base" ? new SynchronizationContext() : null;
            SynchronizationContext.SetSynchronizationContext(previous);
            Settings settings = Configure("fixed");
            var failure = new InvalidOperationException("text constructor context");
            if (point != "none") { settings.AtPoint = (_, observed) => { if (observed == point) { throw failure; } }; }
            TraceControl? control = null;
            bool throws = point != "none" && (withBounds || point != "enter:core");
            if (throws) { Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => Create(withBounds, "context"))); }
            else { control = Create(withBounds, "context"); }
            settings.AtPoint = null;
            using (control ?? settings.Observed!)
            {
                Assert.Same(previous, Assert.Single(settings.Calls, call => call.Point == "default:size").Context);
                bool installed = autoInstall && context != "custom" && point != "default:size";
                if (installed) { Assert.IsType<WindowsFormsSynchronizationContext>(SynchronizationContext.Current); }
                else { Assert.Same(previous, SynchronizationContext.Current); }
                foreach (Call call in settings.Calls.Where(call => call.Point is "enter:set:text" or "enter:core" or "derived-constructor"))
                {
                    Assert.Same(SynchronizationContext.Current, call.Context);
                }
            }
        });

    private static string?[] Texts() => [null, "", "plain", "virtual-value", "中文🙂", "line\r\nnext\n", "\0", "before\0after"];

    private static byte EffectiveTextNullability(ConstructorInfo constructor)
    {
        byte? parameter = NullableFlag(constructor.GetParameters()[0].CustomAttributes, "NullableAttribute");
        if (parameter is not null) { return parameter.Value; }
        byte? method = NullableFlag(constructor.CustomAttributes, "NullableContextAttribute");
        if (method is not null) { return method.Value; }
        for (Type? type = constructor.DeclaringType; type is not null; type = type.DeclaringType)
        {
            byte? context = NullableFlag(type.CustomAttributes, "NullableContextAttribute");
            if (context is not null) { return context.Value; }
        }
        return 0;
    }

    private static byte? NullableFlag(IEnumerable<CustomAttributeData> attributes, string name)
    {
        CustomAttributeData? attribute = attributes.FirstOrDefault(data => data.AttributeType.FullName == "System.Runtime.CompilerServices." + name);
        if (attribute is null || attribute.ConstructorArguments.Count == 0) { return null; }
        object? value = attribute.ConstructorArguments[0].Value;
        if (value is IEnumerable<CustomAttributeTypedArgument> flags) { value = flags.FirstOrDefault().Value; }
        return value is byte flag ? flag : null;
    }

    private static Rectangle[] Rectangles() => [new(7, 9, 83, 47), Rectangle.Empty, new(0, 0, 0, 47), new(0, 0, 83, 0),
        new(-7, -9, -3, -5), new(-1, 1, -3, 37), new(1, -1, 73, -5), new(1, 1, 1, 1),
        new(int.MinValue, int.MaxValue, int.MinValue, -1), new(int.MaxValue, int.MinValue, -1, int.MinValue),
        new(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue), new(int.MinValue, int.MinValue, int.MinValue, int.MaxValue),
        new(int.MaxValue - 1, int.MinValue + 1, int.MaxValue - 1, int.MinValue + 1)];

    private static Settings Configure(string profile = "empty")
    {
        Current = new Settings
        {
            Minimum = profile switch { "fixed" => new(31, 17), "conflict" => new(101, 59), "negative" => new(-7, -9), "zero-axis" => new(0, 17), _ => Size.Empty },
            Maximum = profile switch { "fixed" => new(101, 59), "conflict" => new(31, 17), "negative" => new(-3, -5), "zero-axis" => new(101, 0), _ => Size.Empty }
        };
        return Current;
    }

    private static TraceControl Create(bool withBounds, string? text)
        => Current!.Mode == "hidden-text"
            ? withBounds ? new HiddenTextControl(text, 7, 9, 83, 47) : new HiddenTextControl(text)
            : withBounds ? new TraceControl(text, 7, 9, 83, 47) : new TraceControl(text);

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            SynchronizationContext.SetSynchronizationContext(null);
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally { Current = null; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The text constructor check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class CustomContext : SynchronizationContext { }
    private sealed class Settings
    {
        internal Size Minimum;
        internal Size Maximum;
        internal string Mode = "normal";
        internal TraceControl? Observed;
        internal readonly List<Call> Calls = [];
        internal Action<TraceControl, string>? AtPoint;
    }
    private sealed record Call(string Point, Rectangle Bounds, Size Client, string RawText, SynchronizationContext? Context,
        string? TextArgument = null, Rectangle? Request = null, BoundsSpecified? Specified = null);

    private class TraceControl : Control
    {
        internal TraceControl(string? text) : base(text) => Point("derived-constructor");
        internal TraceControl(string? text, int left, int top, int width, int height) : base(text, left, top, width, height)
            => Point("derived-constructor");
        internal string BaseText => base.Text;

        [AllowNull]
        public override string Text
        {
            get { Point("get:text"); return Current!.Mode == "fixed-text-getter" ? "virtual-value" : Current.Mode == "null-text-getter" ? null! : base.Text; }
            set
            {
                Point("enter:set:text", value);
                if (Current!.Mode != "skip-text-setter") { base.Text = Current.Mode == "text-suffix" ? value + "-suffix" : value; }
                Point("exit:set:text", value);
            }
        }
        protected override Size DefaultSize { get { Point("default:size"); return Size.Empty; } }
        protected override Size DefaultMinimumSize { get { Point("default:min"); return Current!.Minimum; } }
        protected override Size DefaultMaximumSize { get { Point("default:max"); return Current!.Maximum; } }
        public override Size MinimumSize
        {
            get { Point("get:min"); return base.MinimumSize; }
            set { Point("enter:set:min"); base.MinimumSize = value; Point("exit:set:min"); }
        }
        public override Size MaximumSize
        {
            get { Point("get:max"); return base.MaximumSize; }
            set { Point("enter:set:max"); base.MaximumSize = value; Point("exit:set:max"); }
        }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Point("enter:core", request: new(x, y, width, height), specified: specified);
            if (Current!.Mode != "skip-core") { base.SetBoundsCore(x, y, width, height, specified); }
            Point("exit:core", request: new(x, y, width, height), specified: specified);
        }
        protected override void OnBoundsChanged() { }
        protected override void OnTextChanged(EventArgs e) { Point("enter:TextChanged"); if (Current!.Mode != "skip-text-notification") { base.OnTextChanged(e); } Point("exit:TextChanged"); }
        protected override void OnLocationChanged(EventArgs e) { Point("enter:LocationChanged"); base.OnLocationChanged(e); Point("exit:LocationChanged"); }
        protected override void OnMove(EventArgs e) { Point("enter:Move"); base.OnMove(e); Point("exit:Move"); }
        protected override void OnSizeChanged(EventArgs e) { Point("enter:SizeChanged"); base.OnSizeChanged(e); Point("exit:SizeChanged"); }
        protected override void OnResize(EventArgs e) { Point("enter:Resize"); base.OnResize(e); Point("exit:Resize"); }
        protected override void OnClientSizeChanged(EventArgs e) { Point("enter:ClientSizeChanged"); base.OnClientSizeChanged(e); Point("exit:ClientSizeChanged"); }

        private void Point(string point, string? textArgument = null, Rectangle? request = null, BoundsSpecified? specified = null)
        {
            Settings settings = Current!;
            if (settings.Observed is null)
            {
                settings.Observed = this;
                TextChanged += (_, _) => Point("event:TextChanged");
                LocationChanged += (_, _) => Point("event:LocationChanged");
                Move += (_, _) => Point("event:Move");
                SizeChanged += (_, _) => Point("event:SizeChanged");
                Resize += (_, _) => Point("event:Resize");
                ClientSizeChanged += (_, _) => Point("event:ClientSizeChanged");
            }
            settings.Calls.Add(new Call(point, Bounds, ClientSize, base.Text, SynchronizationContext.Current, textArgument, request, specified));
            settings.AtPoint?.Invoke(this, point);
        }
    }

    private sealed class HiddenTextControl : TraceControl
    {
        internal HiddenTextControl(string? text) : base(text) { }
        internal HiddenTextControl(string? text, int left, int top, int width, int height) : base(text, left, top, width, height) { }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get => throw new InvalidOperationException("hidden getter"); set => throw new InvalidOperationException("hidden setter"); }
    }
}
