using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxHiddenMembersTests
{
    [Theory]
    [InlineData(nameof(TextBoxBase.BackgroundImage), true)]
    [InlineData(nameof(TextBoxBase.BackgroundImageLayout), true)]
    [InlineData(nameof(TextBoxBase.Padding), false)]
    public void HiddenPropertiesKeepTheirStandardDeclarationAndDesignerVisibility(string name, bool virtualMember)
    {
        PropertyInfo property = typeof(TextBoxBase).GetProperty(name,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(TextBoxBase), property.DeclaringType);
        Assert.True(property.GetMethod!.IsPublic);
        Assert.True(property.SetMethod!.IsPublic);
        Assert.Equal(virtualMember, property.GetMethod.IsVirtual);
        Assert.Equal(virtualMember, property.SetMethod.IsVirtual);
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>(false)!.Browsable);
        Assert.Equal(EditorBrowsableState.Never,
            property.GetCustomAttribute<EditorBrowsableAttribute>(false)!.State);
        if (virtualMember)
        {
            Assert.Equal(typeof(Control), property.GetMethod.GetBaseDefinition().DeclaringType);
            Assert.Equal(typeof(Control), property.SetMethod.GetBaseDefinition().DeclaringType);
        }
        else
        {
            Assert.Equal(DesignerSerializationVisibility.Hidden,
                property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>(false)!.Visibility);
        }
    }

    [Theory]
    [InlineData(nameof(TextBoxBase.BackgroundImageChanged), false, typeof(EventHandler))]
    [InlineData(nameof(TextBoxBase.BackgroundImageLayoutChanged), false, typeof(EventHandler))]
    [InlineData(nameof(TextBoxBase.PaddingChanged), false, typeof(EventHandler))]
    [InlineData(nameof(TextBoxBase.MouseClick), true, typeof(MouseEventHandler))]
    public void RedeclaredEventsPreserveTheirDelegateAndStatementCompletionContract(
        string name, bool browsable, Type handlerType)
    {
        EventInfo notification = typeof(TextBoxBase).GetEvent(name,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(notification);
        Assert.Equal(typeof(TextBoxBase), notification.DeclaringType);
        Assert.Equal(handlerType, notification.EventHandlerType);
        Assert.True(notification.AddMethod!.IsPublic && notification.RemoveMethod!.IsPublic);
        Assert.False(notification.AddMethod.IsVirtual || notification.RemoveMethod.IsVirtual);
        Assert.Equal(browsable, notification.GetCustomAttribute<BrowsableAttribute>(false)!.Browsable);
        Assert.Equal(browsable ? EditorBrowsableState.Always : EditorBrowsableState.Never,
            notification.GetCustomAttribute<EditorBrowsableAttribute>(false)!.State);
        if (name == nameof(TextBoxBase.PaddingChanged))
        {
            Assert.Equal("Layout", notification.GetCustomAttribute<CategoryAttribute>(false)!.Category);
            Assert.Equal(DesignerSerializationVisibility.Hidden,
                notification.GetCustomAttribute<DesignerSerializationVisibilityAttribute>(false)!.Visibility);
        }
    }

    [Fact]
    public void DoubleBufferedRemainsAProtectedOverrideHiddenFromStatementCompletion()
    {
        PropertyInfo property = typeof(TextBoxBase).GetProperty("DoubleBuffered",
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.True(property.GetMethod!.IsFamily && property.SetMethod!.IsFamily);
        Assert.True(property.GetMethod.IsVirtual && property.SetMethod.IsVirtual);
        Assert.Equal(typeof(Control), property.GetMethod.GetBaseDefinition().DeclaringType);
        Assert.Equal(typeof(Control), property.SetMethod.GetBaseDefinition().DeclaringType);
        Assert.Equal(EditorBrowsableState.Never,
            property.GetCustomAttribute<EditorBrowsableAttribute>(false)!.State);
        Assert.Null(typeof(TextBoxBase).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.Public));
    }

    [Theory]
    [InlineData(nameof(TextBoxBase.BackgroundImage))]
    [InlineData(nameof(TextBoxBase.BackgroundImageLayout))]
    public void TypeDescriptorHidesImagesWhileRetainingInheritedResourceMetadata(string name)
    {
        PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(TextBox))[name]!;
        Assert.NotNull(property);
        Assert.False(property.IsBrowsable);
        Assert.Equal(EditorBrowsableState.Never,
            ((EditorBrowsableAttribute)property.Attributes[typeof(EditorBrowsableAttribute)]!).State);
        Assert.True(((LocalizableAttribute)property.Attributes[typeof(LocalizableAttribute)]!).IsLocalizable);
        object? expectedDefault = name == nameof(TextBoxBase.BackgroundImage) ? null : ImageLayout.Tile;
        Assert.Equal(expectedDefault,
            ((DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)]!).Value);
    }

    [Fact]
    public void TypeDescriptorDoesNotSerializePadding()
    {
        PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(TextBox))[nameof(TextBoxBase.Padding)]!;
        Assert.False(property.IsBrowsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden, property.SerializationVisibility);
        Assert.Equal(EditorBrowsableState.Never,
            ((EditorBrowsableAttribute)property.Attributes[typeof(EditorBrowsableAttribute)]!).State);
    }

    [Theory]
    [InlineData(nameof(TextBoxBase.BackgroundImageChanged), false)]
    [InlineData(nameof(TextBoxBase.BackgroundImageLayoutChanged), false)]
    [InlineData(nameof(TextBoxBase.PaddingChanged), false)]
    [InlineData(nameof(TextBoxBase.MouseClick), true)]
    public void TypeDescriptorKeepsMouseClickVisibleWhileHidingUnusedNativeEditEvents(string name, bool browsable)
    {
        EventDescriptor notification = TypeDescriptor.GetEvents(typeof(TextBox))[name]!;
        Assert.NotNull(notification);
        Assert.Equal(browsable, notification.IsBrowsable);
        Assert.Equal(browsable ? EditorBrowsableState.Always : EditorBrowsableState.Never,
            ((EditorBrowsableAttribute)notification.Attributes[typeof(EditorBrowsableAttribute)]!).State);
    }

    [Fact]
    public void ImageViewsShareStorageNotificationsAndCrossSurfaceRemovalWithoutOwningTheImage()
        => OnSta(() =>
        {
            using var first = new Bitmap(2, 3);
            using var second = new Bitmap(4, 5);
            var textBox = new ProbeTextBox();
            try
            {
                Control control = textBox;
                TextBoxBase editing = textBox;
                List<string> trace = [];
                EventHandler editingHandler = (sender, args) =>
                {
                    Assert.Same(textBox, sender);
                    Assert.Same(EventArgs.Empty, args);
                    trace.Add("editing");
                };
                editing.BackgroundImageChanged += editingHandler;
                control.BackgroundImageChanged += (_, _) => trace.Add("control");

                editing.BackgroundImage = first;
                Assert.Same(first, control.BackgroundImage);
                Assert.Equal(new[] { "editing", "control" }, trace);
                control.BackgroundImageChanged -= editingHandler;
                control.BackgroundImage = second;
                editing.BackgroundImage = second;

                Assert.Same(second, editing.BackgroundImage);
                Assert.Equal(new[] { "editing", "control", "control" }, trace);
                Assert.Equal(2, first.Width);
                Assert.False(textBox.IsHandleCreated);
            }
            finally { textBox.Dispose(); }
            Assert.Equal(2, first.Width);
            Assert.Equal(4, second.Width);
        });

    [Fact]
    public void ImageLayoutViewsUseTheSameNotificationSlotAndKeepTheBaseValidation()
        => OnSta(() =>
        {
            using var textBox = new ProbeTextBox();
            Control control = textBox;
            TextBoxBase editing = textBox;
            List<ImageLayout> observed = [];
            EventHandler handler = (_, args) =>
            {
                Assert.Same(EventArgs.Empty, args);
                observed.Add(editing.BackgroundImageLayout);
            };
            control.BackgroundImageLayoutChanged += handler;
            editing.BackgroundImageLayout = ImageLayout.Center;
            editing.BackgroundImageLayoutChanged -= handler;
            control.BackgroundImageLayout = ImageLayout.None;
            editing.BackgroundImageLayout = ImageLayout.None;

            Assert.Equal(new[] { ImageLayout.Center }, observed);
            Assert.Equal(ImageLayout.None, editing.BackgroundImageLayout);
            Assert.Equal(ImageLayout.None, control.BackgroundImageLayout);
            InvalidEnumArgumentException error = Assert.Throws<InvalidEnumArgumentException>(
                () => editing.BackgroundImageLayout = (ImageLayout)(-1));
            Assert.Equal("value", error.ParamName);
            Assert.Equal(ImageLayout.None, control.BackgroundImageLayout);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ImageChangeRetainsReentrantAssignmentsThroughEitherPropertyView()
        => OnSta(() =>
        {
            using var first = new Bitmap(2, 2);
            using var second = new Bitmap(3, 3);
            using var textBox = new ProbeTextBox();
            Control control = textBox;
            TextBoxBase editing = textBox;
            List<Image?> observed = [];
            editing.BackgroundImageChanged += (_, _) =>
            {
                observed.Add(control.BackgroundImage);
                if (ReferenceEquals(control.BackgroundImage, first)) { control.BackgroundImage = second; }
            };

            editing.BackgroundImage = first;

            Assert.Equal(new Image?[] { first, second }, observed);
            Assert.Same(second, editing.BackgroundImage);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void ImageLayoutCallbackThrowsItsOriginalExceptionAfterSavingTheSharedState()
        => OnSta(() =>
        {
            using var textBox = new ProbeTextBox();
            Control control = textBox;
            var failure = new InvalidOperationException("layout notification");
            EventHandler handler = (_, _) => throw failure;
            textBox.BackgroundImageLayoutChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => control.BackgroundImageLayout = ImageLayout.Zoom));
            Assert.Equal(ImageLayout.Zoom, textBox.BackgroundImageLayout);
            control.BackgroundImageLayoutChanged -= handler;
            textBox.BackgroundImageLayout = ImageLayout.None;
            Assert.Equal(ImageLayout.None, control.BackgroundImageLayout);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaddingViewsShareClampingAndNotificationsAndAllowCrossSurfaceRemoval()
        => OnSta(() =>
        {
            using var textBox = new ProbeTextBox { AutoSize = false };
            Control control = textBox;
            TextBoxBase editing = textBox;
            List<Padding> observed = [];
            EventHandler handler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                observed.Add(control.Padding);
            };
            editing.PaddingChanged += handler;
            editing.Padding = new Padding(-1, 2, -3, 4);
            Assert.Equal(new Padding(0, 2, 0, 4), control.Padding);
            control.PaddingChanged -= handler;
            control.Padding = new Padding(5, 6, 7, 8);
            editing.Padding = new Padding(5, 6, 7, 8);

            Assert.Equal(new[] { new Padding(0, 2, 0, 4) }, observed);
            Assert.Equal(new Padding(5, 6, 7, 8), editing.Padding);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaddingCallbackKeepsTheOriginalExceptionAndTheAlreadyCommittedValue()
        => OnSta(() =>
        {
            using var textBox = new ProbeTextBox { AutoSize = false };
            Control control = textBox;
            var failure = new InvalidOperationException("padding notification");
            EventHandler handler = (_, _) => throw failure;
            control.PaddingChanged += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(
                () => textBox.Padding = new Padding(2, 3, 4, 5)));
            Assert.Equal(new Padding(2, 3, 4, 5), control.Padding);
            textBox.PaddingChanged -= handler;
            control.Padding = new Padding(1);
            Assert.Equal(new Padding(1), textBox.Padding);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void MouseClickUsesTheBaseSlotAndPreservesTheOriginalArguments()
        => OnSta(() =>
        {
            using var textBox = new ProbeTextBox();
            Control control = textBox;
            TextBoxBase editing = textBox;
            var args = new MouseEventArgs(MouseButtons.Left, 1, 7, -9, 0);
            List<string> trace = [];
            MouseEventHandler handler = (sender, actual) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(args, actual);
                trace.Add("editing");
            };
            editing.MouseClick += handler;
            control.MouseClick += (_, actual) =>
            {
                Assert.Same(args, actual);
                trace.Add("control");
            };

            textBox.RaiseMouseClick(args);
            control.MouseClick -= handler;
            textBox.RaiseMouseClick(args);

            Assert.Equal(new[] { "editing", "control", "control" }, trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void DoubleBufferedForwardsToTheExistingControlStyleState()
        => OnSta(() =>
        {
            using var textBox = new ProbeTextBox();
            Assert.False(textBox.Buffered);
            textBox.Buffered = true;
            Assert.True(textBox.Buffered);
            Assert.True(textBox.StyleEnabled(ControlStyles.OptimizedDoubleBuffer));
            Assert.True(textBox.StyleEnabled(ControlStyles.AllPaintingInWmPaint));
            textBox.Buffered = false;
            Assert.False(textBox.Buffered);
            Assert.False(textBox.StyleEnabled(ControlStyles.OptimizedDoubleBuffer));
            Assert.True(textBox.StyleEnabled(ControlStyles.AllPaintingInWmPaint));
            Assert.False(textBox.IsHandleCreated);
        });

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
            finally
            {
                try
                {
                    Application.ExitThread();
                    typeof(Application).GetMethod("DisposeThemedCommonControlsActivation",
                        BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(null, null);
                }
                catch (Exception exception)
                {
                    failure = failure is null ? exception : new AggregateException(failure, exception);
                }
            }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox hidden-member test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ProbeTextBox : TextBox
    {
        internal bool Buffered { get => DoubleBuffered; set => DoubleBuffered = value; }
        internal bool StyleEnabled(ControlStyles style) => GetStyle(style);
        internal void RaiseMouseClick(MouseEventArgs args) => OnMouseClick(args);
    }
}
