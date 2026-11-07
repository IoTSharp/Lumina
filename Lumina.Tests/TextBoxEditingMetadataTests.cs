using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class TextBoxEditingMetadataTests
{
    [Fact]
    public void StandardDefaultEventAndBindingAreDeclaredOnTheSharedEditingBase()
    {
        Assert.Equal(nameof(TextBoxBase.TextChanged),
            typeof(TextBoxBase).GetCustomAttribute<DefaultEventAttribute>(false)!.Name);
        Assert.Equal(nameof(TextBoxBase.Text),
            typeof(TextBoxBase).GetCustomAttribute<DefaultBindingPropertyAttribute>(false)!.Name);
    }

    [Theory]
    [InlineData(typeof(TextBoxBase))]
    [InlineData(typeof(TextBox))]
    public void TypeDescriptorSelectsTextChangedAndTextAsTheEditingDefaults(Type type)
    {
        EventDescriptor notification = TypeDescriptor.GetDefaultEvent(type)!;
        Assert.NotNull(notification);
        Assert.Equal(nameof(TextBoxBase.TextChanged), notification.Name);
        Assert.Equal(typeof(EventHandler), notification.EventType);
        DefaultBindingPropertyAttribute binding = (DefaultBindingPropertyAttribute)
            TypeDescriptor.GetAttributes(type)[typeof(DefaultBindingPropertyAttribute)]!;
        Assert.Equal(nameof(TextBoxBase.Text), binding.Name);
        PropertyDescriptor text = TypeDescriptor.GetProperties(type)[binding.Name!]!;
        Assert.Equal(typeof(string), text.PropertyType);
        Assert.False(text.IsReadOnly);
    }

    [Fact]
    public void PaintIsPublicNonvirtualAndHiddenOnTheEditingBase()
    {
        EventInfo notification = typeof(TextBoxBase).GetEvent(nameof(TextBoxBase.Paint),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(notification);
        Assert.Equal(typeof(PaintEventHandler), notification.EventHandlerType);
        Assert.True(notification.AddMethod!.IsPublic && notification.RemoveMethod!.IsPublic);
        Assert.False(notification.AddMethod.IsVirtual || notification.RemoveMethod.IsVirtual);
        Assert.False(notification.GetCustomAttribute<BrowsableAttribute>(false)!.Browsable);
        Assert.Equal(EditorBrowsableState.Never,
            notification.GetCustomAttribute<EditorBrowsableAttribute>(false)!.State);
    }

    [Theory]
    [InlineData(typeof(TextBoxBase))]
    [InlineData(typeof(TextBox))]
    public void TypeDescriptorHidesPaintWithoutChangingItsHandlerType(Type type)
    {
        EventDescriptor notification = TypeDescriptor.GetEvents(type)[nameof(TextBoxBase.Paint)]!;
        Assert.NotNull(notification);
        Assert.Equal(typeof(PaintEventHandler), notification.EventType);
        Assert.False(notification.IsBrowsable);
        Assert.Equal(EditorBrowsableState.Never,
            ((EditorBrowsableAttribute)notification.Attributes[typeof(EditorBrowsableAttribute)]!).State);
    }

    [Theory]
    [InlineData(nameof(TextBoxBase.Multiline), RefreshProperties.All)]
    [InlineData(nameof(TextBoxBase.ReadOnly), RefreshProperties.Repaint)]
    public void EditingFlagsKeepBehaviorCategoryAndTheirExistingDefaults(
        string name, RefreshProperties refresh)
    {
        PropertyInfo declaration = typeof(TextBoxBase).GetProperty(name,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        Assert.Equal("Behavior", declaration.GetCustomAttribute<CategoryAttribute>(false)!.Category);
        Assert.Equal(false, declaration.GetCustomAttribute<DefaultValueAttribute>(false)!.Value);
        PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(TextBox))[name]!;
        Assert.Equal("Behavior", property.Category);
        Assert.True(property.IsBrowsable);
        Assert.False(property.IsReadOnly);
        Assert.Equal(false, ((DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)]!).Value);
        Assert.Equal(refresh,
            ((RefreshPropertiesAttribute)property.Attributes[typeof(RefreshPropertiesAttribute)]!).RefreshProperties);
    }

    [Theory]
    [InlineData(nameof(TextBoxBase.MultilineChanged))]
    [InlineData(nameof(TextBoxBase.ReadOnlyChanged))]
    public void EditingNotificationsUseTheStandardPropertyChangedCategory(string name)
    {
        EventInfo declaration = typeof(TextBoxBase).GetEvent(name,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        Assert.Equal(typeof(EventHandler), declaration.EventHandlerType);
        Assert.False(declaration.AddMethod!.IsVirtual || declaration.RemoveMethod!.IsVirtual);
        Assert.Equal("Property Changed", declaration.GetCustomAttribute<CategoryAttribute>(false)!.Category);
        EventDescriptor notification = TypeDescriptor.GetEvents(typeof(TextBox))[name]!;
        Assert.Equal("Property Changed", notification.Category);
        Assert.True(notification.IsBrowsable);
    }

    [Theory]
    [InlineData(nameof(TextBoxBase.SelectionStart), false)]
    [InlineData(nameof(TextBoxBase.SelectionLength), true)]
    public void SelectionPropertiesStayWritableButAreNotDesignerState(string name, bool isVirtual)
    {
        PropertyInfo declaration = typeof(TextBoxBase).GetProperty(name,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        Assert.Equal(typeof(int), declaration.PropertyType);
        Assert.True(declaration.GetMethod!.IsPublic && declaration.SetMethod!.IsPublic);
        Assert.Equal(isVirtual, declaration.GetMethod.IsVirtual);
        Assert.Equal(isVirtual, declaration.SetMethod.IsVirtual);
        Assert.Equal("Appearance", declaration.GetCustomAttribute<CategoryAttribute>(false)!.Category);
        Assert.False(declaration.GetCustomAttribute<BrowsableAttribute>(false)!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            declaration.GetCustomAttribute<DesignerSerializationVisibilityAttribute>(false)!.Visibility);

        PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(TextBox))[name]!;
        Assert.Equal("Appearance", property.Category);
        Assert.False(property.IsBrowsable);
        Assert.False(property.IsReadOnly);
        Assert.Equal(DesignerSerializationVisibility.Hidden, property.SerializationVisibility);
    }

    [Fact]
    public void DefaultEventDescriptorSubscribesToTheExistingTextNotification()
        => OnSta(() =>
        {
            using var textBox = new ProbeTextBox();
            EventDescriptor notification = TypeDescriptor.GetDefaultEvent(textBox)!;
            List<string> observed = [];
            EventHandler handler = (sender, args) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(EventArgs.Empty, args);
                observed.Add(textBox.Text);
            };
            notification.AddEventHandler(textBox, handler);
            textBox.Text = "first";
            textBox.TextChanged -= handler;
            textBox.Text = "second";
            Assert.Equal(new[] { "first" }, observed);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void HiddenSelectionDescriptorsStillReadAndWriteTheSharedSelectionState()
        => OnSta(() =>
        {
            using var textBox = new ProbeTextBox { Text = "A\u4e2d\ud83d\ude00tail" };
            TextBoxBase editing = textBox;
            PropertyDescriptor start = TypeDescriptor.GetProperties(textBox)[nameof(TextBoxBase.SelectionStart)]!;
            PropertyDescriptor length = TypeDescriptor.GetProperties(textBox)[nameof(TextBoxBase.SelectionLength)]!;
            start.SetValue(textBox, 2);
            length.SetValue(textBox, 2);
            Assert.Equal(2, editing.SelectionStart);
            Assert.Equal(2, editing.SelectionLength);
            Assert.Equal("\ud83d\ude00", editing.SelectedText);
            editing.Select(1, 1);
            Assert.Equal(1, start.GetValue(textBox));
            Assert.Equal(1, length.GetValue(textBox));
            Assert.False(textBox.IsHandleCreated);
        });

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void PaintSubscriptionsShareOrderArgumentsAndCrossSurfaceRemoval(bool subscribeThroughEditing)
        => OnSta(() =>
        {
            using var bitmap = new Bitmap(3, 4);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using var args = new PaintEventArgs(graphics, new Rectangle(-2, 3, 4, 5));
            using var textBox = new ProbeTextBox();
            Control control = textBox;
            TextBoxBase editing = textBox;
            List<string> trace = [];
            PaintEventHandler first = (sender, actual) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(args, actual);
                trace.Add("first");
            };
            PaintEventHandler second = (_, actual) =>
            {
                Assert.Same(args, actual);
                trace.Add("second");
            };
            if (subscribeThroughEditing)
            {
                editing.Paint += first;
                control.Paint += second;
            }
            else
            {
                control.Paint += first;
                editing.Paint += second;
            }

            textBox.RaisePaint(args);
            if (subscribeThroughEditing) { control.Paint -= first; }
            else { editing.Paint -= first; }
            textBox.RaisePaint(args);
            Assert.Equal(new[] { "first", "second", "second" }, trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaintForwardingPreservesTheExistingDirectHookWhenComponentEventsAreDisabled()
        => OnSta(() =>
        {
            using var bitmap = new Bitmap(2, 2);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using var args = new PaintEventArgs(graphics, Rectangle.Empty);
            using var textBox = new ProbeTextBox { RaiseComponentEvents = false };
            int calls = 0;
            textBox.Paint += (sender, actual) =>
            {
                Assert.Same(textBox, sender);
                Assert.Same(args, actual);
                calls++;
            };
            textBox.RaisePaint(args);
            Assert.Equal(1, calls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaintHandlerFailureKeepsTheOriginalExceptionAndCanBeRemovedThroughTheBase()
        => OnSta(() =>
        {
            using var bitmap = new Bitmap(2, 2);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using var args = new PaintEventArgs(graphics, Rectangle.Empty);
            using var textBox = new ProbeTextBox();
            var failure = new InvalidOperationException("paint notification");
            int laterCalls = 0;
            PaintEventHandler handler = (_, _) => throw failure;
            textBox.Paint += handler;
            ((Control)textBox).Paint += (_, _) => laterCalls++;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => textBox.RaisePaint(args)));
            Assert.Equal(0, laterCalls);
            ((Control)textBox).Paint -= handler;
            textBox.RaisePaint(args);
            Assert.Equal(1, laterCalls);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaintAllowsOneNestedHookWithoutReplacingEitherArgument()
        => OnSta(() =>
        {
            using var bitmap = new Bitmap(2, 2);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using var outer = new PaintEventArgs(graphics, new Rectangle(0, 0, 2, 2));
            using var inner = new PaintEventArgs(graphics, new Rectangle(1, 1, 1, 1));
            using var textBox = new ProbeTextBox();
            List<string> trace = [];
            textBox.Paint += (_, actual) =>
            {
                trace.Add(ReferenceEquals(outer, actual) ? "first:outer" : "first:inner");
                if (ReferenceEquals(outer, actual)) { textBox.RaisePaint(inner); }
                else { Assert.Same(inner, actual); }
            };
            ((Control)textBox).Paint += (_, actual) =>
                trace.Add(ReferenceEquals(outer, actual) ? "second:outer" : "second:inner");
            textBox.RaisePaint(outer);
            Assert.Equal(new[] { "first:outer", "first:inner", "second:inner", "second:outer" }, trace);
            Assert.False(textBox.IsHandleCreated);
        });

    [Fact]
    public void PaintForwardingBorrowsTheCallersGraphicsAcrossControlDisposal()
        => OnSta(() =>
        {
            using var bitmap = new Bitmap(2, 2);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using var args = new PaintEventArgs(graphics, new Rectangle(0, 0, 2, 2));
            using (var textBox = new ProbeTextBox())
            {
                textBox.Paint += (_, actual) => actual.Graphics.Clear(Color.Red);
                textBox.RaisePaint(args);
                Assert.Equal(Color.Red.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
                Assert.False(textBox.IsHandleCreated);
            }
            graphics.Clear(Color.Blue);
            Assert.Equal(Color.Blue.ToArgb(), bitmap.GetPixel(0, 0).ToArgb());
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "TextBox editing metadata test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ProbeTextBox : TextBox
    {
        internal bool RaiseComponentEvents { get; set; } = true;
        protected override bool CanRaiseEvents => RaiseComponentEvents;
        internal void RaisePaint(PaintEventArgs args) => OnPaint(args);
    }
}
