using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlSizeFromClientSizeTests
{
    private static readonly Rectangle Initial = new(11, 13, 73, 37);

    [Fact]
    public void Conversion_HasStandardProtectedVirtualMetadata()
    {
        MethodInfo method = typeof(Control).GetMethod("SizeFromClientSize", BindingFlags.Instance | BindingFlags.NonPublic, [typeof(Size)])!;
        Assert.Equal(typeof(Size), method.ReturnType);
        Assert.Equal(typeof(Control), method.DeclaringType);
        Assert.True(method.IsFamily);
        Assert.False(method.IsFamilyOrAssembly);
        Assert.False(method.IsPublic);
        Assert.False(method.IsStatic);
        Assert.True(method.IsVirtual);
        Assert.False(method.IsFinal);
        Assert.False(method.IsAbstract);
        Assert.NotEqual(0, (int)(method.Attributes & MethodAttributes.NewSlot));
        Assert.Equal(method, method.GetBaseDefinition());
        ParameterInfo parameter = Assert.Single(method.GetParameters());
        Assert.Equal("clientSize", parameter.Name);
        Assert.Equal(typeof(Size), parameter.ParameterType);
    }

    public static IEnumerable<object[]> ConversionCases()
    {
        foreach (object[] context in Contexts())
        {
            foreach ((int width, int height) in new[]
            {
                (83, 47), (0, 0), (-3, -5), (-3, 37), (73, -5), (1, 1),
                (int.MinValue, -1), (-1, int.MinValue), (int.MaxValue, int.MaxValue),
                (int.MinValue, int.MaxValue), (int.MaxValue - 1, int.MinValue + 1)
            })
            {
                yield return [context[0], context[1], width, height];
            }
        }
    }

    [Theory]
    [MemberData(nameof(ConversionCases))]
    public void BaseConversion_PreservesStateAndNotificationsForSignedAndOverflowInputs(string border, bool native, int width, int height)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            Snapshot before = Capture(control);
            var input = new Size(width, height);
            Size expected = WithBorder(input, border);
            Assert.Equal(expected, control.Convert(input));
            Assert.Equal(input, Assert.Single(control.Conversions));
            Assert.Empty(control.Events);
            Assert.Equal(before, Capture(control));
            control.Reset();
            Assert.Equal(expected, control.Convert(input));
            Assert.Equal(before, Capture(control));
            control.Reset();
            Assert.Equal(expected, control.ConvertBase(input));
            Assert.Empty(control.Conversions);
            Assert.Empty(control.Events);
            Assert.Equal(before, Capture(control));
        });

    public static IEnumerable<object[]> OverrideCases()
    {
        foreach (object[] context in Contexts())
        {
            foreach (string mode in new[] { "fixed", "adjust" })
            {
                yield return [context[0], context[1], mode];
            }
        }
    }

    [Theory]
    [MemberData(nameof(OverrideCases))]
    public void DirectConversion_DispatchesOverrideAndAllowsBaseBypass(string border, bool native, string mode)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            Snapshot before = Capture(control);
            control.Mode = mode;
            var input = new Size(83, 47);
            Size baseline = WithBorder(input, border);
            Size expected = mode == "fixed" ? new Size(19, 23) : new Size(baseline.Width + 5, baseline.Height + 7);
            Assert.Equal(expected, control.Convert(input));
            Assert.Equal(input, Assert.Single(control.Conversions));
            Assert.Equal(baseline, control.ConvertBase(input));
            Assert.Single(control.Conversions);
            Assert.Empty(control.Events);
            Assert.Equal(before, Capture(control));
        });

    public static IEnumerable<object[]> RouteCases()
    {
        foreach (object[] context in Contexts())
        {
            foreach (string mode in new[] { "base", "fixed", "adjust" })
            {
                foreach (string entry in new[] { "client", "core", "size" })
                {
                    yield return [context[0], context[1], mode, entry];
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(RouteCases))]
    public void ClientAndOuterSizeRoutes_DoNotConsumeProtectedConversion(string border, bool native, string mode, string entry)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            control.Mode = mode;
            var failure = new InvalidOperationException("conversion must not run");
            control.ActionPoint = "enter:conversion";
            control.AtPoint = () => throw failure;
            var input = new Size(83, 47);
            void Apply()
            {
                if (entry == "client") { control.ClientSize = input; }
                else if (entry == "core") { control.CallClientCore(input); }
                else { control.Size = input; }
            }
            Apply();
            Assert.Empty(control.Conversions);
            Assert.Equal(entry == "size" ? input : WithBorder(input, border), control.Size);
            int borderSize = BorderSize(border);
            Assert.Equal(entry == "size" ? new Size(83 - borderSize, 47 - borderSize) : input, control.ClientSize);
            Assert.Equal(entry == "size"
                ? new[] { "Resize", "SizeChanged", "ClientSizeChanged" }
                : new[] { "Resize", "SizeChanged", "ClientSizeChanged", "ClientSizeChanged" }, control.Events);
            control.Reset();
            Apply();
            Assert.Empty(control.Conversions);
            if (entry == "size") { Assert.Empty(control.Events); }
            else { Assert.Equal("ClientSizeChanged", Assert.Single(control.Events)); }
            Assert.Equal(native, control.IsHandleCreated);
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.Convert(input)));
        });

    public static IEnumerable<object[]> FailureCases()
    {
        foreach (object[] context in Contexts())
        {
            foreach (string point in new[] { "enter:conversion", "exit:conversion" })
            {
                yield return [context[0], context[1], point];
            }
        }
    }

    [Theory]
    [MemberData(nameof(FailureCases))]
    public void OverrideFailure_PropagatesOriginalExceptionSynchronouslyWithoutChangingState(string border, bool native, string point)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            Snapshot before = Capture(control);
            var failure = new InvalidOperationException("conversion override");
            var delivered = new List<Exception>();
            void Handler(object sender, ThreadExceptionEventArgs args) => delivered.Add(args.Exception);
            Application.ThreadException += Handler;
            try
            {
                control.ActionPoint = point;
                control.AtPoint = () => throw failure;
                Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.Convert(new Size(83, 47))));
                Assert.Empty(delivered);
                Assert.Empty(control.Events);
                Assert.Equal(before, Capture(control));
                Assert.Equal(WithBorder(new Size(83, 47), border), control.Convert(new Size(83, 47)));
                Assert.Equal(before, Capture(control));
            }
            finally { Application.ThreadException -= Handler; }
        });

    public static IEnumerable<object[]> ReentryCases()
    {
        foreach (object[] failure in FailureCases())
        {
            foreach (string nested in new[] { "client", "size", "direct" })
            {
                yield return [failure[0], failure[1], failure[2], nested];
            }
        }
    }

    [Theory]
    [MemberData(nameof(ReentryCases))]
    public void ReentrantOverride_PreservesIndependentReturnAndNestedSizeOperation(string border, bool native, string point, string nested)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            nint handle = control.IsHandleCreated ? control.Handle : 0;
            var nestedInput = new Size(97, 61);
            control.ActionPoint = point;
            control.AtPoint = () =>
            {
                if (nested == "client") { control.ClientSize = nestedInput; }
                else if (nested == "size") { control.Size = nestedInput; }
                else { Assert.Equal(WithBorder(nestedInput, border), control.Convert(nestedInput)); }
            };
            Assert.Equal(WithBorder(new Size(83, 47), border), control.Convert(new Size(83, 47)));
            int borderSize = BorderSize(border);
            Assert.Equal(nested == "direct" ? Initial.Size : nested == "client" ? WithBorder(nestedInput, border) : nestedInput, control.Size);
            Assert.Equal(nested == "direct" ? new Size(73 - borderSize, 37 - borderSize)
                : nested == "client" ? nestedInput : new Size(97 - borderSize, 61 - borderSize), control.ClientSize);
            Assert.Equal(nested == "direct" ? 2 : 1, control.Conversions.Count);
            Assert.Equal(nested == "direct" ? Array.Empty<string>() : nested == "client"
                ? new[] { "Resize", "SizeChanged", "ClientSizeChanged", "ClientSizeChanged" }
                : new[] { "Resize", "SizeChanged", "ClientSizeChanged" }, control.Events);
            Assert.Equal(handle, control.IsHandleCreated ? control.Handle : 0);
            Assert.Equal(native, control.IsHandleCreated);
        });

    public static IEnumerable<object[]> CacheCases()
    {
        foreach (object[] context in Contexts())
        {
            foreach (string cache in new[] { "negative", "reentrant" })
            {
                yield return [context[0], context[1], cache];
            }
        }
    }

    [Theory]
    [MemberData(nameof(CacheCases))]
    public void Conversion_DoesNotRefreshNegativeOrReentrantClientCache(string border, bool native, string cache)
        => OnSta(() =>
        {
            using var form = native ? new Form() : null;
            using TraceControl control = Create(form, border);
            if (cache == "negative") { control.ClientSize = new Size(-3, -5); }
            else
            {
                control.ActionPoint = "event:SizeChanged";
                control.AtPoint = () => control.Size = new Size(97, 61);
                control.ClientSize = new Size(83, 47);
            }
            control.Reset();
            Snapshot before = Capture(control);
            Assert.Equal(cache == "negative" ? new Size(-3, -5) : new Size(83, 47), before.ClientSize);
            Assert.Equal(WithBorder(new Size(83, 47), border), control.Convert(new Size(83, 47)));
            Assert.Empty(control.Events);
            Assert.Equal(before, Capture(control));
        });

    public static IEnumerable<object[]> Contexts()
    {
        foreach (string border in new[] { "none", "border", "edge", "both" })
        {
            yield return [border, false];
            yield return [border, true];
        }
    }

    private static int BorderSize(string border) => border switch { "border" => 2, "edge" => 4, "both" => 6, _ => 0 };
    private static Size WithBorder(Size input, string border)
    {
        int nonClient = BorderSize(border);
        return new Size(unchecked(input.Width + nonClient), unchecked(input.Height + nonClient));
    }

    private static TraceControl Create(Form? form, string border)
    {
        var control = new TraceControl(border) { Bounds = Initial };
        if (form is not null)
        {
            form.Controls.Add(control);
            form.Show();
            Assert.NotEqual(0, control.Handle);
        }
        control.Reset();
        return control;
    }

    private static Snapshot Capture(Control control)
    {
        Size? nativeWindow = null;
        Rectangle? nativeClient = null;
        if (control.IsHandleCreated)
        {
            Assert.True(GetWindowRect(control.Handle, out NativeRect window));
            Assert.True(GetClientRect(control.Handle, out NativeRect client));
            nativeWindow = new Size(window.Right - window.Left, window.Bottom - window.Top);
            nativeClient = new Rectangle(client.Left, client.Top, client.Right - client.Left, client.Bottom - client.Top);
        }
        return new Snapshot(control.Bounds, control.ClientSize, control.ClientRectangle, control.IsHandleCreated, control.IsHandleCreated ? control.Handle : 0, nativeWindow, nativeClient);
    }

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The client size conversion check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct Snapshot(Rectangle Bounds, Size ClientSize, Rectangle ClientRectangle, bool Created, nint Handle, Size? NativeWindow, Rectangle? NativeClient);

    private sealed class TraceControl : Control
    {
        internal readonly List<Size> Conversions = [];
        internal new readonly List<string> Events = [];
        private readonly string _border;
        internal string Mode = "base";
        internal string? ActionPoint;
        internal Action? AtPoint;
        protected override uint Style => base.Style | (_border is "border" or "both" ? 0x00800000u : 0u);
        protected override uint ExStyle => base.ExStyle | (_border is "edge" or "both" ? 0x00000200u : 0u);

        internal TraceControl(string border)
        {
            _border = border;
            Move += (_, _) => Event("Move");
            LocationChanged += (_, _) => Event("LocationChanged");
            Resize += (_, _) => Event("Resize");
            SizeChanged += (_, _) => Event("SizeChanged");
            ClientSizeChanged += (_, _) => Event("ClientSizeChanged");
        }

        internal void Reset() { Conversions.Clear(); Events.Clear(); }
        internal Size Convert(Size clientSize) => SizeFromClientSize(clientSize);
        internal Size ConvertBase(Size clientSize) => base.SizeFromClientSize(clientSize);
        internal void CallClientCore(Size input) => SetClientSizeCore(input.Width, input.Height);
        protected override void OnBoundsChanged() { }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            Conversions.Add(clientSize);
            Point("enter:conversion");
            Size result = Mode == "fixed" ? new Size(19, 23) : base.SizeFromClientSize(clientSize);
            if (Mode == "adjust") { result = new Size(unchecked(result.Width + 5), unchecked(result.Height + 7)); }
            Point("exit:conversion");
            return result;
        }

        private void Event(string name) { Events?.Add(name); Point("event:" + name); }
        private void Point(string point)
        {
            if (ActionPoint == point)
            {
                Action? action = AtPoint;
                ActionPoint = null;
                AtPoint = null;
                action?.Invoke();
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect { public int Left; public int Top; public int Right; public int Bottom; }
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint handle, out NativeRect rectangle);
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetClientRect(nint handle, out NativeRect rectangle);
}
