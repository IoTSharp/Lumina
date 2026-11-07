using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxDefaultsTests
{
    [Fact]
    public void RichTextBoxDirectlyUsesTheStandardEditingBaseAndOnlyItsDefaultConstructor()
    {
        Assert.Equal(typeof(TextBoxBase), typeof(RichTextBox).BaseType);
        Assert.False(typeof(TextBox).IsAssignableFrom(typeof(RichTextBox)));
        ConstructorInfo constructor = Assert.Single(typeof(RichTextBox).GetConstructors());
        Assert.Empty(constructor.GetParameters());
    }

    [Theory]
    [InlineData(nameof(TextBox.AcceptsReturn))]
    [InlineData(nameof(TextBox.CharacterCasing))]
    [InlineData(nameof(TextBox.PasswordChar))]
    [InlineData(nameof(TextBox.UseSystemPasswordChar))]
    [InlineData(nameof(TextBox.PlaceholderText))]
    [InlineData(nameof(TextBox.TextAlign))]
    public void TheDirectHierarchyDoesNotExposeTextBoxSpecificProperties(string name)
        => Assert.Null(typeof(RichTextBox).GetProperty(name));

    [Theory]
    [InlineData(nameof(TextBoxBase.AutoSize), typeof(bool), false)]
    [InlineData(nameof(TextBoxBase.Multiline), typeof(bool), true)]
    [InlineData(nameof(TextBoxBase.MaxLength), typeof(int), int.MaxValue)]
    public void RichEditingDefaultsAreDeclaredAsRealOverrides(string name, Type type, object defaultValue)
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(name,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(type, property.PropertyType);
        Assert.True(property.GetMethod!.IsPublic && property.GetMethod.IsVirtual);
        Assert.True(property.SetMethod!.IsPublic && property.SetMethod.IsVirtual);
        Assert.False(property.GetMethod.IsFinal || property.SetMethod.IsFinal);
        Assert.NotEqual(typeof(RichTextBox), property.GetMethod.GetBaseDefinition().DeclaringType);
        Assert.Equal(defaultValue, property.GetCustomAttribute<DefaultValueAttribute>(false)!.Value);
        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(RichTextBox))[name]!;
        Assert.False(descriptor.IsReadOnly);
        Assert.Equal(defaultValue, ((DefaultValueAttribute)descriptor.Attributes[typeof(DefaultValueAttribute)]!).Value);
    }

    [Fact]
    public void AutoSizeRemainsHiddenButDesignerSerializableWithTheRichDefault()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.AutoSize),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>(false)!.Browsable);
        Assert.Equal(EditorBrowsableState.Never,
            property.GetCustomAttribute<EditorBrowsableAttribute>(false)!.State);
        Assert.Equal(RefreshProperties.Repaint,
            property.GetCustomAttribute<RefreshPropertiesAttribute>(false)!.RefreshProperties);
        Assert.Equal(DesignerSerializationVisibility.Visible,
            property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>(false)!.Visibility);
        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(RichTextBox))[nameof(RichTextBox.AutoSize)]!;
        Assert.False(descriptor.IsBrowsable);
        Assert.Equal(DesignerSerializationVisibility.Visible, descriptor.SerializationVisibility);
    }

    [Fact]
    public void TheConstructorSetsTheRichDefaultsAndSizeWithoutCreatingAHandle()
        => OnSta(() =>
        {
            using var control = new Probe();
            TextBoxBase editing = control;
            Control common = control;

            Assert.Equal(new Size(100, 96), control.ProtectedDefaultSize);
            Assert.Equal(new Size(100, 96), control.Size);
            Assert.False(control.AutoSize);
            Assert.False(editing.AutoSize);
            Assert.False(common.AutoSize);
            Assert.True(control.Multiline);
            Assert.True(editing.Multiline);
            Assert.Equal(int.MaxValue, control.MaxLength);
            Assert.Equal(int.MaxValue, editing.MaxLength);
            Assert.True(control.HideSelection);
            Assert.False(control.ReadOnly);
            Assert.False(control.AcceptsTab);
            Assert.True(control.WordWrap);
            Assert.True(control.ShortcutsEnabled);
            Assert.Equal(BorderStyle.Fixed3D, control.BorderStyle);
            Assert.False(control.Modified);
            Assert.Equal(string.Empty, control.Text);
            Assert.Equal(RichTextBoxScrollBars.Both, control.ScrollBars);
            Assert.False(control.AutoWordSelection);
            Assert.True(control.DetectUrls);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, control.HandleCreatedCount);
        });

    [Fact]
    public void ConstructionDispatchesTheThreeEditingSettersInTheFixedOrder()
        => OnSta(() =>
        {
            using var control = new ConstructorProbe();
            Assert.Equal(new[] { "MaxLength:2147483647", "Multiline:True", "AutoSize:False" }, control.Writes);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void AFurtherDerivedDefaultSizeSurvivesTheRichConstructor()
        => OnSta(() =>
        {
            using var control = new DerivedSizeProbe();
            Assert.Equal(new Size(173, 125), control.Size);
            Assert.False(control.AutoSize);
            Assert.True(control.Multiline);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void ReassigningTheDefaultsDoesNotRaiseEventsOrCreateAHandle()
        => OnSta(() =>
        {
            using var control = new Probe();
            int notifications = 0;
            control.AutoSizeChanged += (_, _) => notifications++;
            control.MultilineChanged += (_, _) => notifications++;
            control.HideSelectionChanged += (_, _) => notifications++;
            control.AutoSize = false;
            control.Multiline = true;
            control.MaxLength = int.MaxValue;
            control.HideSelection = true;
            control.AutoWordSelection = false;
            control.DetectUrls = true;
            control.ScrollBars = RichTextBoxScrollBars.Both;
            Assert.Equal(0, notifications);
            Assert.Equal(new Size(100, 96), control.Size);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void AutoSizeSharesTheEditingStateAndTheExistingEventSlot()
        => OnSta(() =>
        {
            using var control = new Probe();
            TextBoxBase editing = control;
            Control common = control;
            var values = new List<bool>();
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                Assert.Equal(control.AutoSize, editing.AutoSize);
                Assert.Equal(control.AutoSize, common.AutoSize);
                values.Add(control.AutoSize);
            };
            common.AutoSizeChanged += handler;
            editing.AutoSize = true;
            Assert.True(control.AutoSize);
            Assert.Equal(new Size(100, 96), control.Size);
            common.AutoSize = false;
            Assert.False(control.AutoSize);
            control.AutoSizeChanged -= handler;
            editing.AutoSize = true;
            Assert.Equal(new[] { true, false }, values);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void MultilineSharesStateAndNotifiesAfterEachActualChange()
        => OnSta(() =>
        {
            using var control = new Probe();
            TextBoxBase editing = control;
            var values = new List<bool>();
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                Assert.Equal(control.Multiline, editing.Multiline);
                values.Add(control.Multiline);
            };
            editing.MultilineChanged += handler;
            editing.Multiline = false;
            control.Multiline = false;
            control.Multiline = true;
            control.MultilineChanged -= handler;
            editing.Multiline = false;
            Assert.Equal(new[] { false, true }, values);
            Assert.Equal(new Size(100, 96), control.Size);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void HideSelectionUsesTheSingleInheritedPropertyStateAndEvent()
        => OnSta(() =>
        {
            using var control = new Probe();
            TextBoxBase editing = control;
            Assert.Equal(typeof(TextBoxBase), typeof(RichTextBox).GetProperty(nameof(RichTextBox.HideSelection))!.DeclaringType);
            var values = new List<bool>();
            EventHandler handler = (sender, e) =>
            {
                Assert.Same(control, sender);
                Assert.Same(EventArgs.Empty, e);
                Assert.Equal(control.HideSelection, editing.HideSelection);
                values.Add(control.HideSelection);
            };
            control.HideSelectionChanged += handler;
            control.HideSelection = false;
            Assert.False(editing.HideSelection);
            editing.HideSelection = true;
            editing.HideSelectionChanged -= handler;
            control.HideSelection = false;
            Assert.Equal(new[] { false, true }, values);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void MaxLengthChangesTheSharedLimitWithoutTruncatingPendingTextOrCreatingAHandle(int value)
        => OnSta(() =>
        {
            using var control = new Probe { Text = "abc中" };
            TextBoxBase editing = control;
            int notifications = 0;
            control.TextChanged += (_, _) => notifications++;
            editing.MaxLength = value;
            Assert.Equal(value, control.MaxLength);
            Assert.Equal("abc中", control.Text);
            Assert.Equal(0, notifications);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void MaxLengthRejectsANegativeValueAndPreservesThePreviousLimit(int value)
        => OnSta(() =>
        {
            using var control = new Probe { MaxLength = 37 };
            ArgumentOutOfRangeException failure = Assert.Throws<ArgumentOutOfRangeException>(() => control.MaxLength = value);
            Assert.Equal("value", failure.ParamName);
            Assert.Equal(37, control.MaxLength);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void AnAutoSizeHandlerExceptionPropagatesAfterTheSharedStateChanges()
        => OnSta(() =>
        {
            using var control = new Probe();
            var failure = new InvalidOperationException("rich auto-size handler");
            EventHandler handler = (_, _) => throw failure;
            control.AutoSizeChanged += handler;
            try
            {
                Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.AutoSize = true));
                Assert.True(control.AutoSize);
                Assert.True(((TextBoxBase)control).AutoSize);
                Assert.False(control.IsHandleCreated);
            }
            finally { control.AutoSizeChanged -= handler; }
            control.AutoSize = false;
            Assert.False(control.AutoSize);
        });

    [Fact]
    public void AMultilineHandlerExceptionPropagatesWithoutRollingBackTheChange()
        => OnSta(() =>
        {
            using var control = new Probe();
            var failure = new InvalidOperationException("rich multiline handler");
            EventHandler handler = (_, _) => throw failure;
            control.MultilineChanged += handler;
            try
            {
                Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.Multiline = false));
                Assert.False(control.Multiline);
                Assert.False(((TextBoxBase)control).Multiline);
                Assert.False(control.IsHandleCreated);
            }
            finally { control.MultilineChanged -= handler; }
            control.Multiline = true;
            Assert.True(control.Multiline);
        });

    [Fact]
    public void HideSelectionSupportsOneBoundedReentrantChangeThroughTheBaseReference()
        => OnSta(() =>
        {
            using var control = new Probe();
            TextBoxBase editing = control;
            var values = new List<bool>();
            bool reentered = false;
            control.HideSelectionChanged += (_, _) =>
            {
                values.Add(control.HideSelection);
                if (!reentered)
                {
                    reentered = true;
                    editing.HideSelection = true;
                }
            };
            control.HideSelection = false;
            Assert.Equal(new[] { false, true }, values);
            Assert.True(control.HideSelection);
            Assert.True(editing.HideSelection);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(nameof(RichTextBox.AutoWordSelection), false)]
    [InlineData(nameof(RichTextBox.DetectUrls), true)]
    public void RichNativeOptionPropertiesUseTheStandardNonvirtualDeclarations(string name, bool defaultValue)
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(name,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.NotNull(property);
        Assert.Equal(typeof(bool), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual || property.SetMethod!.IsVirtual);
        Assert.Equal(defaultValue, property.GetCustomAttribute<DefaultValueAttribute>(false)!.Value);
        Assert.Equal("Behavior", property.GetCustomAttribute<CategoryAttribute>(false)!.Category);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void NativeOptionSettingsRemainPendingWithoutCreatingAHandle(bool value)
        => OnSta(() =>
        {
            using var control = new Probe();
            control.AutoWordSelection = value;
            control.DetectUrls = value;
            Assert.Equal(value, control.AutoWordSelection);
            Assert.Equal(value, control.DetectUrls);
            Assert.False(control.IsHandleCreated);
            Assert.Equal(0, control.HandleCreatedCount);
        });

    [Fact]
    public void ScrollBarsHasTheRichEnumTypeAndStandardMetadata()
    {
        PropertyInfo property = typeof(RichTextBox).GetProperty(nameof(RichTextBox.ScrollBars),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)!;
        Assert.Equal(typeof(RichTextBoxScrollBars), property.PropertyType);
        Assert.False(property.GetMethod!.IsVirtual || property.SetMethod!.IsVirtual);
        Assert.Equal("Appearance", property.GetCustomAttribute<CategoryAttribute>(false)!.Category);
        Assert.True(property.GetCustomAttribute<LocalizableAttribute>(false)!.IsLocalizable);
        Assert.Equal(RichTextBoxScrollBars.Both, property.GetCustomAttribute<DefaultValueAttribute>(false)!.Value);
        Assert.Equal(new[] { 0, 1, 2, 3, 17, 18, 19 },
            Enum.GetValues<RichTextBoxScrollBars>().Select(value => (int)value));
    }

    [Theory]
    [InlineData(RichTextBoxScrollBars.None)]
    [InlineData(RichTextBoxScrollBars.Horizontal)]
    [InlineData(RichTextBoxScrollBars.Vertical)]
    [InlineData(RichTextBoxScrollBars.Both)]
    [InlineData(RichTextBoxScrollBars.ForcedHorizontal)]
    [InlineData(RichTextBoxScrollBars.ForcedVertical)]
    [InlineData(RichTextBoxScrollBars.ForcedBoth)]
    public void EveryStandardScrollBarSettingCanBeStoredWithoutCreatingAHandle(RichTextBoxScrollBars value)
        => OnSta(() =>
        {
            using var control = new Probe();
            control.ScrollBars = value;
            Assert.Equal(value, control.ScrollBars);
            Assert.Equal(new Size(100, 96), control.Size);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    [InlineData(16)]
    [InlineData(20)]
    [InlineData(int.MaxValue)]
    public void ScrollBarsRejectsUndefinedValuesWithoutChangingStateOrCreatingAHandle(int value)
        => OnSta(() =>
        {
            using var control = new Probe { ScrollBars = RichTextBoxScrollBars.ForcedVertical };
            InvalidEnumArgumentException failure = Assert.Throws<InvalidEnumArgumentException>(
                () => control.ScrollBars = (RichTextBoxScrollBars)value);
            Assert.Equal("value", failure.ParamName);
            Assert.Equal(RichTextBoxScrollBars.ForcedVertical, control.ScrollBars);
            Assert.False(control.IsHandleCreated);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox defaults test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private class Probe : RichTextBox
    {
        internal Size ProtectedDefaultSize => base.DefaultSize;
        internal int HandleCreatedCount { get; private set; }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            HandleCreatedCount++;
        }
    }

    private sealed class DerivedSizeProbe : RichTextBox
    {
        protected override Size DefaultSize => new(173, 125);
    }

    private sealed class ConstructorProbe : RichTextBox
    {
        internal List<string> Writes { get; } = new();

        public override int MaxLength
        {
            get => base.MaxLength;
            set
            {
                Writes.Add($"MaxLength:{value}");
                base.MaxLength = value;
            }
        }

        public override bool Multiline
        {
            get => base.Multiline;
            set
            {
                Writes.Add($"Multiline:{value}");
                base.Multiline = value;
            }
        }

        public override bool AutoSize
        {
            get => base.AutoSize;
            set
            {
                Writes.Add($"AutoSize:{value}");
                base.AutoSize = value;
            }
        }
    }
}
