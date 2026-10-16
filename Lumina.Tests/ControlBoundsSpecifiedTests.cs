using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlBoundsSpecifiedTests
{
    private static readonly Rectangle InitialBounds = new(11, 13, 73, 37);

    [Fact]
    public void BoundsSpecified_HasStandardFlagsAndValues()
    {
        Assert.True(typeof(BoundsSpecified).IsDefined(typeof(FlagsAttribute), inherit: false));
        Assert.Equal(typeof(int), Enum.GetUnderlyingType(typeof(BoundsSpecified)));
        Assert.Equal(new[] { "None", "X", "Y", "Location", "Width", "Height", "Size", "All" }, Enum.GetNames<BoundsSpecified>());
        Assert.Equal(new[] { 0, 1, 2, 3, 4, 8, 12, 15 }, Enum.GetValues<BoundsSpecified>().Select(value => (int)value));
    }

    [Theory]
    [InlineData(4, false)]
    [InlineData(5, false)]
    [InlineData(5, true)]
    public void SetBoundsMembers_HaveStandardSignaturesAndParameterNames(int parameterCount, bool core)
    {
        Type[] parameterTypes = parameterCount == 4
            ? [typeof(int), typeof(int), typeof(int), typeof(int)]
            : [typeof(int), typeof(int), typeof(int), typeof(int), typeof(BoundsSpecified)];
        MethodInfo method = typeof(Control).GetMethod(core ? "SetBoundsCore" : "SetBounds",
            BindingFlags.Instance | (core ? BindingFlags.NonPublic : BindingFlags.Public), parameterTypes)!;
        Assert.NotNull(method);
        Assert.Equal(typeof(void), method.ReturnType);
        Assert.Equal(core, method.IsFamily);
        Assert.Equal(!core, method.IsPublic);
        Assert.Equal(core, method.IsVirtual);
        Assert.False(method.IsFinal);
        string[] expectedNames = parameterCount == 4 ? ["x", "y", "width", "height"] : ["x", "y", "width", "height", "specified"];
        Assert.Equal(expectedNames, method.GetParameters().Select(parameter => parameter.Name));
    }

    [Theory]
    [InlineData(0, 11, 13, 73, 37)]
    [InlineData(1, 23, 13, 73, 37)]
    [InlineData(2, 11, 29, 73, 37)]
    [InlineData(3, 23, 29, 73, 37)]
    [InlineData(4, 11, 13, 83, 37)]
    [InlineData(5, 23, 13, 83, 37)]
    [InlineData(6, 11, 29, 83, 37)]
    [InlineData(7, 23, 29, 83, 37)]
    [InlineData(8, 11, 13, 73, 47)]
    [InlineData(9, 23, 13, 73, 47)]
    [InlineData(10, 11, 29, 73, 47)]
    [InlineData(11, 23, 29, 73, 47)]
    [InlineData(12, 11, 13, 83, 47)]
    [InlineData(13, 23, 13, 83, 47)]
    [InlineData(14, 11, 29, 83, 47)]
    [InlineData(15, 23, 29, 83, 47)]
    [InlineData(16, 11, 13, 73, 37)]
    [InlineData(17, 23, 13, 73, 37)]
    [InlineData(31, 23, 29, 83, 47)]
    [InlineData(-1, 23, 29, 83, 47)]
    [InlineData(int.MinValue, 11, 13, 73, 37)]
    [InlineData(int.MaxValue, 23, 29, 83, 47)]
    public void SetBounds_PreservesUnspecifiedValuesAndPassesOriginalFlagsToCore(int mask, int x, int y, int width, int height)
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            var expected = new Rectangle(x, y, width, height);
            control.SetBounds(x: 23, y: 29, width: 83, height: 47, specified: (BoundsSpecified)mask);
            Assert.Equal(expected, control.Bounds);
            if (expected == InitialBounds)
            {
                Assert.Empty(control.Calls);
                Assert.Equal(0, control.BoundsChanges);
            }
            else
            {
                Assert.Equal(new CoreCall(expected, (BoundsSpecified)mask, InitialBounds), Assert.Single(control.Calls));
                Assert.Equal(1, control.BoundsChanges);
            }

            control.Calls.Clear();
            control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask);
            control.SetBounds(
                (mask & 1) == 0 ? 199 : x,
                (mask & 2) == 0 ? 211 : y,
                (mask & 4) == 0 ? 223 : width,
                (mask & 8) == 0 ? 227 : height, (BoundsSpecified)mask);
            Assert.Empty(control.Calls);
            Assert.Equal(expected, control.Bounds);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData("four", 15, 23, 29, 83, 47)]
    [InlineData("five", 15, 23, 29, 83, 47)]
    [InlineData("left", 1, 23, 13, 73, 37)]
    [InlineData("top", 2, 11, 29, 73, 37)]
    [InlineData("location", 3, 23, 29, 73, 37)]
    [InlineData("width", 4, 11, 13, 83, 37)]
    [InlineData("height", 8, 11, 13, 73, 47)]
    [InlineData("size", 12, 11, 13, 83, 47)]
    [InlineData("bounds", 15, 23, 29, 83, 47)]
    public void BoundsEntryPoints_ReachVirtualCoreWithTheirStandardFlags(string entry, int mask, int x, int y, int width, int height)
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            Apply(control);
            var expected = new Rectangle(x, y, width, height);
            Assert.Equal(new CoreCall(expected, (BoundsSpecified)mask, InitialBounds), Assert.Single(control.Calls));
            Assert.Equal(expected, control.Bounds);
            Assert.Equal(1, control.BoundsChanges);
            control.Calls.Clear();
            Apply(control);
            Assert.Empty(control.Calls);
            Assert.Equal(1, control.BoundsChanges);
            Assert.False(control.IsHandleCreated);

            void Apply(TraceControl target)
            {
                switch (entry)
                {
                    case "four": target.SetBounds(x: 23, y: 29, width: 83, height: 47); break;
                    case "five": target.SetBounds(x: 23, y: 29, width: 83, height: 47, specified: BoundsSpecified.All); break;
                    case "left": target.Left = 23; break;
                    case "top": target.Top = 29; break;
                    case "location": target.Location = new Point(23, 29); break;
                    case "width": target.Width = 83; break;
                    case "height": target.Height = 47; break;
                    case "size": target.Size = new Size(83, 47); break;
                    case "bounds": target.Bounds = new Rectangle(23, 29, 83, 47); break;
                    default: throw new InvalidOperationException("Unknown test entry point.");
                }
            }
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(12)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(-1)]
    public void DirectCore_UsesItsRectangleWithoutFilteringBySpecified(int mask)
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            var expected = new Rectangle(23, 29, -3, -5);
            control.CallCore(expected, (BoundsSpecified)mask);
            Assert.Equal(expected, control.Bounds);
            Assert.Equal(new CoreCall(expected, (BoundsSpecified)mask, InitialBounds), Assert.Single(control.Calls));
            Assert.Equal(1, control.BoundsChanges);
            control.CallCore(expected, (BoundsSpecified)mask);
            Assert.Equal(2, control.Calls.Count);
            Assert.Equal(new CoreCall(expected, (BoundsSpecified)mask, expected), control.Calls[1]);
            Assert.Equal(1, control.BoundsChanges);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(1, 25, 16, 78, 44)]
    [InlineData(4, 13, 16, 88, 44)]
    [InlineData(15, 25, 32, 88, 54)]
    [InlineData(17, 25, 16, 78, 44)]
    [InlineData(-1, 25, 32, 88, 54)]
    public void Override_CanAdjustComponentsThatWereNotExplicitlySpecified(int mask, int x, int y, int width, int height)
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            control.Transform = true;
            control.SetBounds(23, 29, 83, 47, (BoundsSpecified)mask);
            Assert.Equal(new Rectangle(x, y, width, height), control.Bounds);
            Assert.Single(control.Calls);
            Assert.Equal((BoundsSpecified)mask, control.Calls[0].Specified);
            Assert.Equal(InitialBounds, control.Calls[0].Previous);
            Assert.Equal(1, control.BoundsChanges);
        });

    [Fact]
    public void Override_ReceivesNewPreservedComponentsAfterItsPreviousAdjustment()
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            control.Transform = true;
            control.SetBounds(23, 29, 83, 47, BoundsSpecified.X);
            control.SetBounds(23, 29, 83, 47, BoundsSpecified.X);
            Assert.Equal(new CoreCall(new Rectangle(23, 16, 78, 44), BoundsSpecified.X, new Rectangle(25, 16, 78, 44)), control.Calls[1]);
            Assert.Equal(new Rectangle(25, 19, 83, 51), control.Bounds);
            Assert.Equal(2, control.BoundsChanges);
        });

    [Fact]
    public void Override_CanDeclineChangesWithoutBypassByPublicEntryPoints()
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            control.SkipBase = true;
            control.Left = 23;
            control.Left = 23;
            Assert.Equal(2, control.Calls.Count);
            Assert.All(control.Calls, call => Assert.Equal(new CoreCall(new Rectangle(23, 13, 73, 37), BoundsSpecified.X, InitialBounds), call));
            Assert.Equal(InitialBounds, control.Bounds);
            Assert.Equal(0, control.BoundsChanges);
            control.SetBounds(23, 29, 83, 47, BoundsSpecified.None);
            Assert.Equal(2, control.Calls.Count);
        });

    [Fact]
    public void Override_CanPassNoneToBaseWithoutLosingItsRectangle()
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            control.BaseSpecifiedNone = true;
            control.Left = 23;
            Assert.Equal(new Rectangle(23, 13, 73, 37), control.Bounds);
            Assert.Equal(BoundsSpecified.X, Assert.Single(control.Calls).Specified);
            Assert.Equal(1, control.BoundsChanges);
        });

    [Fact]
    public void Override_ExceptionPropagatesWithoutApplyingBoundsOrNotifying()
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            control.Throw = true;
            var failure = Assert.Throws<InvalidOperationException>(() => control.Width = 83);
            Assert.Equal("test override", failure.Message);
            Assert.Equal(InitialBounds, control.Bounds);
            Assert.Equal(BoundsSpecified.Width, Assert.Single(control.Calls).Specified);
            Assert.Equal(0, control.BoundsChanges);
        });

    [Fact]
    public void LocationAndSizeFlags_PreserveNegativeUnspecifiedDimensionsWithoutCreatingHandle()
        => OnStaThread(() =>
        {
            using TraceControl control = Create();
            control.SetBounds(-7, 9, -3, -5);
            control.Calls.Clear();
            control.Location = new Point(23, 29);
            Assert.Equal(new CoreCall(new Rectangle(23, 29, -3, -5), BoundsSpecified.Location, new Rectangle(-7, 9, -3, -5)), Assert.Single(control.Calls));
            control.Width = 83;
            Assert.Equal(new Rectangle(23, 29, 83, -5), control.Bounds);
            control.Height = 47;
            Assert.Equal(new Rectangle(23, 29, 83, 47), control.Bounds);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(4, 11, 13, 0, 37, 2)]
    [InlineData(8, 11, 13, 73, 0, 2)]
    [InlineData(12, 11, 13, 0, 0, 2)]
    [InlineData(15, 23, 29, 0, 0, 2)]
    [InlineData(16, 11, 13, 73, 37, 0)]
    [InlineData(17, 23, 13, 73, 37, 1)]
    [InlineData(-1, 23, 29, 0, 0, 2)]
    [Trait("Category", "WindowsHandle")]
    public void SpecifiedBounds_WithHandlePreserveOtherComponentsAndNormalizeOnlyInBaseCore(
        int mask, int x, int y, int width, int height, int expectedCalls)
        => OnStaThread(() =>
        {
            using var form = new Form();
            using TraceControl control = Create();
            form.Controls.Add(control);
            form.Show();
            control.Calls.Clear();
            control.BoundsChanges = 0;
            nint handle = control.Handle;
            control.SetBounds(23, 29, -3, -5, (BoundsSpecified)mask);
            control.SetBounds(23, 29, -3, -5, (BoundsSpecified)mask);
            var expected = new Rectangle(x, y, width, height);
            Assert.Equal(expected, control.Bounds);
            Assert.Equal(expectedCalls, control.Calls.Count);
            Assert.Equal(expectedCalls == 0 ? 0 : 1, control.BoundsChanges);
            if ((mask & 4) != 0) { Assert.All(control.Calls, call => Assert.Equal(-3, call.Requested.Width)); }
            if ((mask & 8) != 0) { Assert.All(control.Calls, call => Assert.Equal(-5, call.Requested.Height)); }
            Assert.True(GetWindowRect(handle, out NativeRect bounds));
            var point = new NativePoint { X = bounds.Left, Y = bounds.Top };
            Assert.True(ScreenToClient(form.Handle, ref point));
            Assert.Equal(expected, new Rectangle(point.X, point.Y, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top));
            control.Size = new Size(73, 37);
            Assert.Equal(new Rectangle(x, y, 73, 37), control.Bounds);
            Assert.Equal(handle, control.Handle);
        });

    private static TraceControl Create()
    {
        var control = new TraceControl { Bounds = InitialBounds };
        control.Calls.Clear();
        control.BoundsChanges = 0;
        return control;
    }

    private static void OnStaThread(Action action)
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The specified bounds check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct CoreCall(Rectangle Requested, BoundsSpecified Specified, Rectangle Previous);

    private sealed class TraceControl : Control
    {
        internal readonly List<CoreCall> Calls = [];
        internal int BoundsChanges;
        internal bool SkipBase;
        internal bool Transform;
        internal bool BaseSpecifiedNone;
        internal bool Throw;

        internal void CallCore(Rectangle rectangle, BoundsSpecified specified)
            => SetBoundsCore(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, specified);

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Calls.Add(new CoreCall(new Rectangle(x, y, width, height), specified, Bounds));
            if (Throw) { throw new InvalidOperationException("test override"); }
            if (SkipBase) { return; }
            if (Transform) { x += 2; y += 3; width += 5; height += 7; }
            base.SetBoundsCore(x, y, width, height, BaseSpecifiedNone ? BoundsSpecified.None : specified);
        }

        protected override void OnBoundsChanged()
        {
            BoundsChanges++;
            base.OnBoundsChanged();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect { public int Left; public int Top; public int Right; public int Bottom; }
    [StructLayout(LayoutKind.Sequential)]
    private struct NativePoint { public int X; public int Y; }
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint handle, out NativeRect rectangle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ScreenToClient(nint handle, ref NativePoint point);
}
