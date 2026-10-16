using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlBackgroundImageTests
{
    [Fact]
    public void StandardBackgroundImageSurfacePreservesDefaultsAndVirtualExtensionPoints()
        => OnSta(() =>
        {
            using var control = new ProbeControl();
            Assert.Null(control.BackgroundImage);
            Assert.Equal(ImageLayout.Tile, control.BackgroundImageLayout);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(new[] { 0, 1, 2, 3, 4 }, Enum.GetValues<ImageLayout>().Select(value => (int)value));
            foreach (string name in new[] { nameof(Control.BackgroundImage), nameof(Control.BackgroundImageLayout) })
            {
                PropertyInfo property = typeof(Control).GetProperty(name)!;
                Assert.True(property.GetMethod!.IsVirtual);
                Assert.True(property.SetMethod!.IsVirtual);
                Assert.True(property.GetCustomAttribute<LocalizableAttribute>()!.IsLocalizable);
            }
            Assert.Null(typeof(Control).GetProperty(nameof(Control.BackgroundImage))!.GetCustomAttribute<DefaultValueAttribute>()!.Value);
            Assert.Equal(ImageLayout.Tile, typeof(Control).GetProperty(nameof(Control.BackgroundImageLayout))!
                .GetCustomAttribute<DefaultValueAttribute>()!.Value);
            foreach (string name in new[] { "OnBackgroundImageChanged", "OnBackgroundImageLayoutChanged", "OnParentBackgroundImageChanged" })
            {
                MethodInfo method = typeof(Control).GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic)!;
                Assert.True(method.IsFamily && method.IsVirtual);
            }
        });

    [Fact]
    public void ParentImageChangeNotifiesDescendantsWithoutInheritingOrReplacingTheirImages()
        => OnSta(() =>
        {
            using var parent = new ProbeControl();
            using var child = new ProbeControl();
            using var grandchild = new ProbeControl();
            using var local = new ProbeControl();
            using var parentImage = new Bitmap(3, 2);
            using var childImage = new Bitmap(5, 4);
            parent.Controls.AddRange(child, local);
            child.Controls.Add(grandchild);
            local.BackgroundImage = childImage;
            List<string> trace = [];
            parent.BackgroundImageChanged += (_, _) => trace.Add("parent");
            child.BackgroundImageChanged += (_, _) => trace.Add("child");
            grandchild.BackgroundImageChanged += (_, _) => trace.Add("grandchild");
            local.BackgroundImageChanged += (_, _) => trace.Add("local");

            parent.BackgroundImage = parentImage;

            Assert.Equal(new[] { "parent", "child", "grandchild", "local" }, trace);
            Assert.Null(child.BackgroundImage);
            Assert.Null(grandchild.BackgroundImage);
            Assert.Same(childImage, local.BackgroundImage);
            parent.BackgroundImage = parentImage;
            Assert.Equal(4, trace.Count);
            Assert.False(parent.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void ParentImageNotificationUsesTheLiveChildCollectionDuringRemoval()
        => OnSta(() =>
        {
            using var parent = new ProbeControl();
            using var first = new ProbeControl();
            using var second = new ProbeControl();
            using var image = new Bitmap(3, 2);
            parent.Controls.AddRange(first, second);
            List<string> trace = [];
            first.BackgroundImageChanged += (_, _) => { trace.Add("first"); parent.Controls.Remove(first); };
            second.BackgroundImageChanged += (_, _) => trace.Add("second");

            parent.BackgroundImage = image;

            Assert.Equal(new[] { "first" }, trace);
            Assert.Same(second, Assert.Single(parent.Controls.Cast<Control>()));
        });

    [Fact]
    public void LayoutChangesStayLocalAndResizeAndBufferStylesRemainEnabled()
        => OnSta(() =>
        {
            using var control = new ProbeControl();
            using var child = new ProbeControl();
            using var image = new Bitmap(2, 2, PixelFormat.Format32bppArgb);
            image.SetPixel(0, 0, Color.FromArgb(128, Color.Red));
            using var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            using var decoded = new Bitmap(stream);
            Assert.NotEqual(0, decoded.Flags & (int)ImageFlags.HasAlpha);
            control.Controls.Add(child);
            control.BackgroundImage = decoded;
            int parentEvents = 0, childEvents = 0;
            control.BackgroundImageLayoutChanged += (_, _) => parentEvents++;
            child.BackgroundImageLayoutChanged += (_, _) => childEvents++;

            control.BackgroundImageLayout = ImageLayout.Center;
            Assert.True(control.ReadStyle(ControlStyles.ResizeRedraw));
            Assert.True(control.Buffered);
            control.BackgroundImageLayout = ImageLayout.Tile;
            control.BackgroundImageLayout = ImageLayout.Tile;

            Assert.True(control.ReadStyle(ControlStyles.ResizeRedraw));
            Assert.True(control.Buffered);
            Assert.Equal(2, parentEvents);
            Assert.Equal(0, childEvents);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void InvalidLayoutLeavesTheImageLayoutAndStylesUntouched(int value)
        => OnSta(() =>
        {
            using var control = new ProbeControl();
            InvalidEnumArgumentException error = Assert.Throws<InvalidEnumArgumentException>(() => control.BackgroundImageLayout = (ImageLayout)value);
            Assert.Equal("value", error.ParamName);
            Assert.Equal(ImageLayout.Tile, control.BackgroundImageLayout);
            Assert.False(control.ReadStyle(ControlStyles.ResizeRedraw));
            Assert.False(control.Buffered);
        });

    [Fact]
    public void ImageAndLayoutCallbacksKeepReentrantAssignmentsAndOriginalExceptions()
        => OnSta(() =>
        {
            using var control = new ProbeControl();
            using var first = new Bitmap(2, 2);
            using var second = new Bitmap(3, 3);
            List<Image?> images = [];
            control.BackgroundImageChanged += (_, _) =>
            {
                images.Add(control.BackgroundImage);
                if (ReferenceEquals(control.BackgroundImage, first)) { control.BackgroundImage = second; }
            };
            control.BackgroundImage = first;
            Assert.Equal(new Image?[] { first, second }, images);
            Assert.Same(second, control.BackgroundImage);

            Exception failure = new InvalidOperationException("background layout callback");
            control.BackgroundImageLayoutChanged += (_, _) => throw failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.BackgroundImageLayout = ImageLayout.Center));
            Assert.Equal(ImageLayout.Center, control.BackgroundImageLayout);
            Assert.True(control.ReadStyle(ControlStyles.ResizeRedraw));
        });

    [Fact]
    public void ControlBorrowsImagesAndDisposedImageFailureDoesNotCommitTheNewLayout()
        => OnSta(() =>
        {
            using var first = new Bitmap(2, 2);
            using var second = new Bitmap(3, 3);
            var control = new ProbeControl { BackgroundImage = first };
            control.BackgroundImage = second;
            Assert.Equal(2, first.Width);
            control.Dispose();
            Assert.Same(second, control.BackgroundImage);
            Assert.Equal(3, second.Width);

            using var disposedImage = new Bitmap(2, 2);
            disposedImage.Dispose();
            using var invalid = new ProbeControl { BackgroundImage = disposedImage };
            Assert.Throws<ArgumentException>(() => invalid.BackgroundImageLayout = ImageLayout.Center);
            Assert.Equal(ImageLayout.Tile, invalid.BackgroundImageLayout);
            Assert.True(invalid.ReadStyle(ControlStyles.ResizeRedraw));
            using var output = new Bitmap(10, 10);
            using Graphics graphics = Graphics.FromImage(output);
            using PaintEventArgs args = new(graphics, new Rectangle(0, 0, 10, 10));
            Assert.Throws<ArgumentException>(() => invalid.RaiseBackground(args));
            graphics.Clear(Color.Blue);
            Assert.Equal(Color.Blue.ToArgb(), output.GetPixel(1, 1).ToArgb());
        });

    [Theory]
    [InlineData(ImageLayout.None, true, false, false)]
    [InlineData(ImageLayout.Tile, true, true, true)]
    [InlineData(ImageLayout.Center, false, false, false)]
    [InlineData(ImageLayout.Stretch, true, true, true)]
    [InlineData(ImageLayout.Zoom, false, true, false)]
    public void NativeClientBackgroundPlacesAndScalesImageWithoutTakingOwnership(ImageLayout layout, bool topLeft, bool leftMiddle, bool bottomRight)
        => OnSta(() =>
        {
            using var image = new Bitmap(4, 2, PixelFormat.Format24bppRgb);
            using (Graphics imageGraphics = Graphics.FromImage(image)) { imageGraphics.Clear(Color.Red); }
            using var control = new ProbeControl { Size = new Size(10, 10), BackColor = Color.Blue, BackgroundImage = image, BackgroundImageLayout = layout };
            _ = control.Handle;
            using var output = new Bitmap(10, 10);
            using Graphics graphics = Graphics.FromImage(output);
            using PaintEventArgs args = new(graphics, control.ClientRectangle);
            control.RaiseBackground(args);
            Assert.Equal((topLeft ? Color.Red : Color.Blue).ToArgb(), output.GetPixel(0, 0).ToArgb());
            Assert.Equal((leftMiddle ? Color.Red : Color.Blue).ToArgb(), output.GetPixel(1, 3).ToArgb());
            Assert.Equal((bottomRight ? Color.Red : Color.Blue).ToArgb(), output.GetPixel(9, 9).ToArgb());
            Assert.Equal(Color.Red.ToArgb(), output.GetPixel(layout == ImageLayout.None ? 1 : 5, layout == ImageLayout.None ? 1 : 5).ToArgb());
            Assert.Equal(4, image.Width);
        });

    [Fact]
    public void BackgroundBeforeHandleCreationDoesNotPaintOrMaterializeAWindow()
        => OnSta(() =>
        {
            using var image = new Bitmap(2, 2);
            using var control = new ProbeControl { Size = new Size(20, 20), BackgroundImage = image };
            using var output = new Bitmap(20, 20);
            using Graphics graphics = Graphics.FromImage(output);
            graphics.Clear(Color.Fuchsia);
            using PaintEventArgs args = new(graphics, new Rectangle(0, 0, 20, 20));
            control.RaiseBackground(args);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(Color.Fuchsia.ToArgb(), output.GetPixel(5, 5).ToArgb());
        });

    [Fact]
    public void TransparentParentPaintingUsesItsOwnGraphicsAndRestoresCallerStateOnFailure()
        => OnSta(() =>
        {
            using var parent = new ProbeControl { Size = new Size(64, 48), BackColor = Color.FromArgb(20, 40, 60) };
            using var child = new ProbeControl { Bounds = new Rectangle(9, 7, 24, 20) };
            child.EnableTransparency();
            child.BackColor = Color.Transparent;
            parent.Controls.Add(child);
            _ = parent.Handle;
            _ = child.Handle;
            using var output = new Bitmap(40, 36);
            using Graphics graphics = Graphics.FromImage(output);
            graphics.TranslateTransform(2, 3);
            graphics.SetClip(new Rectangle(1, 2, 18, 15));
            RectangleF clipBefore = graphics.ClipBounds;
            using Matrix transformBefore = graphics.Transform;
            Exception failure = new InvalidOperationException("parent background failed");
            Graphics? parentGraphics = null;
            parent.BackgroundCallback = args =>
            {
                parentGraphics = args.Graphics;
                Assert.NotSame(graphics, args.Graphics);
                Assert.Equal(new Rectangle(9, 7, 24, 20), args.ClipRectangle);
                using Matrix parentTransform = args.Graphics.Transform;
                Assert.True(parentTransform.IsIdentity);
                args.Graphics.TranslateTransform(11, 13);
                args.Graphics.SetClip(new Rectangle(3, 4, 5, 6));
                throw failure;
            };
            using PaintEventArgs args = new(graphics, new Rectangle(1, 2, 18, 15));

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => child.RaiseBackground(args)));

            Assert.Equal(clipBefore, graphics.ClipBounds);
            using Matrix transformAfter = graphics.Transform;
            Assert.Equal(transformBefore.Elements, transformAfter.Elements);
            Assert.Throws<ArgumentException>(() => _ = parentGraphics!.ClipBounds);
            nint hdc = graphics.GetHdc();
            graphics.ReleaseHdc(hdc);
            parent.BackgroundCallback = null;
            child.RaiseBackground(args);
            Assert.Equal(clipBefore, graphics.ClipBounds);
        });

    [Fact]
    public void TransparentBackgroundKeepsTheOriginalParentForBothCallbacksDuringReparenting()
        => OnSta(() =>
        {
            using var parent = new ProbeControl { Size = new Size(64, 48) };
            using var replacement = new ProbeControl { Size = new Size(64, 48) };
            using var child = new ProbeControl { Bounds = new Rectangle(9, 7, 24, 20) };
            child.EnableTransparency();
            child.BackColor = Color.Transparent;
            parent.Controls.Add(child);
            _ = parent.Handle;
            _ = replacement.Handle;
            _ = child.Handle;
            List<string> trace = [];
            parent.BackgroundCallback = _ =>
            {
                trace.Add("original-background");
                replacement.Controls.Add(child);
            };
            parent.Paint += (_, _) => trace.Add("original-paint");
            replacement.Paint += (_, _) => trace.Add("replacement-paint");
            using var output = new Bitmap(24, 20);
            using Graphics graphics = Graphics.FromImage(output);
            using PaintEventArgs args = new(graphics, child.ClientRectangle);

            child.RaiseBackground(args);

            Assert.Same(replacement, child.Parent);
            Assert.Equal(new[] { "original-background", "original-paint" }, trace);
        });

    [Fact]
    public void PartialAlphaBackgroundComposesWithTheParentsActualClientSurface()
        => OnSta(() =>
        {
            using var parent = new ProbeControl { Size = new Size(64, 48), BackColor = Color.FromArgb(20, 40, 60) };
            using var child = new ProbeControl { Bounds = new Rectangle(9, 7, 24, 20) };
            child.EnableTransparency();
            child.BackColor = Color.FromArgb(128, 200, 100, 50);
            parent.Controls.Add(child);
            _ = parent.Handle;
            _ = child.Handle;
            using var output = new Bitmap(24, 20);
            using Graphics graphics = Graphics.FromImage(output);
            graphics.Clear(Color.Fuchsia);
            using PaintEventArgs args = new(graphics, child.ClientRectangle);
            child.RaiseBackground(args);
            Assert.Equal(Color.FromArgb(110, 70, 55).ToArgb(), output.GetPixel(5, 5).ToArgb());
        });

    [Fact]
    public void TransparentMoveInvalidatesAfterTheMoveEventAndParentInvalidationTranslatesItsRectangle()
        => OnSta(() =>
        {
            using var parent = new ProbeControl { Size = new Size(64, 48) };
            using var child = new ProbeControl { Bounds = new Rectangle(9, 7, 24, 20) };
            child.EnableTransparency();
            child.BackColor = Color.Transparent;
            parent.Controls.Add(child);
            _ = parent.Handle;
            _ = child.Handle;
            List<string> trace = [];
            Rectangle invalidated = Rectangle.Empty;
            child.Move += (_, _) => trace.Add("move");
            child.Invalidated += (_, args) => { trace.Add("invalidate"); invalidated = args.InvalidRect; };

            child.RaiseMove();

            Assert.Equal(new[] { "move", "invalidate" }, trace);
            Assert.Equal(child.ClientRectangle, invalidated);
            trace.Clear();
            parent.Invalidate(new Rectangle(12, 10, 8, 6));
            Assert.Equal(new[] { "invalidate" }, trace);
            Assert.Equal(new Rectangle(3, 3, 8, 6), invalidated);
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        Thread thread = new(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The background image check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ProbeControl : Control
    {
        internal Action<PaintEventArgs>? BackgroundCallback;
        internal bool Buffered => DoubleBuffered;
        internal bool ReadStyle(ControlStyles style) => GetStyle(style);
        internal void EnableTransparency() => SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        internal void RaiseBackground(PaintEventArgs args) => OnPaintBackground(args);
        internal void RaiseMove() => OnMove(EventArgs.Empty);

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            BackgroundCallback?.Invoke(e);
            base.OnPaintBackground(e);
        }
    }
}
