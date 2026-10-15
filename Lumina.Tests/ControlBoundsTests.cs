using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlBoundsTests
{
    [Theory]
    [InlineData(0, 37)]
    [InlineData(73, 0)]
    [InlineData(0, 0)]
    [InlineData(-3, 37)]
    [InlineData(73, -5)]
    [InlineData(-3, -5)]
    [InlineData(int.MinValue, 37)]
    [InlineData(73, int.MinValue)]
    public void SetBounds_WithoutOwnHandlePreservesRequestedDimensionsAndPosition(int width, int height)
        => OnStaThread(() =>
        {
            using var control = new Control();
            control.SetBounds(-7, 9, width, height);
            AssertBounds(control, new Rectangle(-7, 9, width, height));
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, control.Handle);

            control.SetBounds(-7, 9, width, height);
            AssertBounds(control, new Rectangle(-7, 9, width, height));
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(-7)]
    [InlineData(int.MinValue)]
    public void WidthAndHeight_WithoutOwnHandlePreserveTheOtherDimensionAndLocation(int value)
        => OnStaThread(() =>
        {
            using var control = new Control { Bounds = new Rectangle(11, 13, 73, 37) };
            control.Width = value;
            AssertBounds(control, new Rectangle(11, 13, value, 37));
            control.Height = value;
            AssertBounds(control, new Rectangle(11, 13, value, value));
            control.Width = value;
            control.Height = value;
            AssertBounds(control, new Rectangle(11, 13, value, value));
            Assert.False(control.IsHandleCreated);

            control.Size = new Size(73, 37);
            AssertBounds(control, new Rectangle(11, 13, 73, 37));
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-3, -5)]
    [InlineData(-3, 17)]
    [InlineData(31, -5)]
    public void SizeAndBounds_WithoutOwnHandlePreserveZeroAndNegativeRequests(int width, int height)
        => OnStaThread(() =>
        {
            using var control = new Control { Bounds = new Rectangle(11, 13, 73, 37) };
            control.Size = new Size(width, height);
            AssertBounds(control, new Rectangle(11, 13, width, height));
            control.Bounds = new Rectangle(-7, -11, width, height);
            AssertBounds(control, new Rectangle(-7, -11, width, height));
            control.Location = new Point(31, 41);
            AssertBounds(control, new Rectangle(31, 41, width, height));
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void OwnedControlWithoutOwnHandle_DoesNotNormalizeOrUseItsOwnersHandle()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new HandlelessControl { Visible = false };
            form.Controls.Add(control);
            form.Show();
            Assert.NotEqual(0, form.Handle);

            control.SetBounds(11, 13, -3, -5);
            AssertBounds(control, new Rectangle(11, 13, -3, -5));
            control.Width = 0;
            AssertBounds(control, new Rectangle(11, 13, 0, -5));
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, control.Handle);
        });

    [Theory]
    [InlineData(0, 37, 0, 37)]
    [InlineData(73, 0, 73, 0)]
    [InlineData(0, 0, 0, 0)]
    [InlineData(-3, 37, 0, 37)]
    [InlineData(73, -5, 73, 0)]
    [InlineData(-3, -5, 0, 0)]
    [InlineData(int.MinValue, 37, 0, 37)]
    [InlineData(73, int.MinValue, 73, 0)]
    [Trait("Category", "WindowsHandle")]
    public void SetBounds_WithOwnHandleNormalizesNativeDimensionsToZeroAndCanRestore(
        int width, int height, int expectedWidth, int expectedHeight)
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new Control { Bounds = new Rectangle(11, 13, 73, 37) };
            form.Controls.Add(control);
            form.Show();
            nint handle = control.Handle;
            var expected = new Rectangle(-7, 9, expectedWidth, expectedHeight);

            control.SetBounds(-7, 9, width, height);
            AssertBounds(control, expected);
            AssertNativeBounds(control, form, expected);
            control.SetBounds(-7, 9, width, height);
            AssertBounds(control, expected);
            AssertNativeBounds(control, form, expected);
            Assert.Equal(handle, control.Handle);

            control.Size = new Size(73, 37);
            var restored = new Rectangle(-7, 9, 73, 37);
            AssertBounds(control, restored);
            AssertNativeBounds(control, form, restored);
            Assert.Equal(handle, control.Handle);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void SizePropertyFamilies_WithOwnHandleUpdateRealZeroBoundsAndPreserveLocation()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new Control { Bounds = new Rectangle(11, 13, 73, 37) };
            form.Controls.Add(control);
            form.Show();
            nint handle = control.Handle;

            control.Width = 0;
            Check(new Rectangle(11, 13, 0, 37));
            control.Height = -5;
            Check(new Rectangle(11, 13, 0, 0));
            control.Size = new Size(-3, 17);
            Check(new Rectangle(11, 13, 0, 17));
            control.Bounds = new Rectangle(-7, -11, 31, -5);
            Check(new Rectangle(-7, -11, 31, 0));
            control.Size = Size.Empty;
            control.Location = new Point(31, 41);
            Check(new Rectangle(31, 41, 0, 0));
            control.Width = -13;
            control.Height = -19;
            Check(new Rectangle(31, 41, 0, 0));
            control.Size = new Size(73, 37);
            Check(new Rectangle(31, 41, 73, 37));

            void Check(Rectangle expected)
            {
                AssertBounds(control, expected);
                AssertNativeBounds(control, form, expected);
                Assert.Equal(handle, control.Handle);
            }
        });

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 37, 0, 37)]
    [InlineData(73, 0, 73, 0)]
    [InlineData(-3, 37, 0, 37)]
    [InlineData(73, -5, 73, 0)]
    [InlineData(-3, -5, 0, 0)]
    [InlineData(int.MinValue, 37, 0, 0)]
    [InlineData(73, int.MinValue, 73, 0)]
    [Trait("Category", "WindowsHandle")]
    public void CreatingOwnHandle_NormalizesOriginalNativeDimensionsBeforeHandleCreated(
        int width, int height, int expectedWidth, int expectedHeight)
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new Control { Bounds = new Rectangle(7, 9, width, height) };
            Rectangle? atHandleCreation = null;
            control.HandleCreated += (_, _) => atHandleCreation = control.Bounds;
            form.Controls.Add(control);
            AssertBounds(control, new Rectangle(7, 9, width, height));
            Assert.False(control.IsHandleCreated);
            form.Show();

            var expected = new Rectangle(7, 9, expectedWidth, expectedHeight);
            Assert.Equal(expected, atHandleCreation);
            AssertBounds(control, expected);
            AssertNativeBounds(control, form, expected);
            nint handle = control.Handle;
            control.Size = new Size(73, 37);
            var restored = new Rectangle(7, 9, 73, 37);
            AssertBounds(control, restored);
            AssertNativeBounds(control, form, restored);
            Assert.Equal(handle, control.Handle);
        });

    private static void AssertBounds(Control control, Rectangle expected)
    {
        bool hadHandle = control.IsHandleCreated;
        Assert.Equal(expected, control.Bounds);
        Assert.Equal(expected.X, control.Left);
        Assert.Equal(expected.Y, control.Top);
        Assert.Equal(expected.Width, control.Width);
        Assert.Equal(expected.Height, control.Height);
        Assert.Equal(expected.Location, control.Location);
        Assert.Equal(expected.Size, control.Size);
        Assert.Equal(expected.Right, control.Right);
        Assert.Equal(expected.Bottom, control.Bottom);
        Assert.Equal(hadHandle, control.IsHandleCreated);
    }

    private static void AssertNativeBounds(Control control, Form parent, Rectangle expected)
    {
        Assert.True(control.IsHandleCreated);
        Assert.True(GetWindowRect(control.Handle, out NativeRect bounds));
        var location = new NativePoint { X = bounds.Left, Y = bounds.Top };
        Assert.True(ScreenToClient(parent.Handle, ref location));
        Assert.Equal(expected, new Rectangle(location.X, location.Y,
            bounds.Right - bounds.Left, bounds.Bottom - bounds.Top));
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The native bounds check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class HandlelessControl : Control
    {
        protected override bool ShouldCreateNativeHandle => false;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NativePoint
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint handle, out NativeRect bounds);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ScreenToClient(nint handle, ref NativePoint point);
}

[CollectionDefinition(ControlBoundsCollection.Name, DisableParallelization = true)]
public sealed class ControlBoundsCollection
{
    public const string Name = "Control native bounds";
}
