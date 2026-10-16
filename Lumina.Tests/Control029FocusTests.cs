using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class Control029FocusTests
{
    [Fact]
    public void ValidationAndTabStopMembersKeepTheStandardControlSurface()
    {
        PropertyInfo causesValidation = typeof(Control).GetProperty(nameof(Control.CausesValidation))!;
        Assert.Equal(typeof(bool), causesValidation.PropertyType);
        Assert.True(causesValidation.GetMethod!.IsPublic);
        Assert.True(causesValidation.SetMethod!.IsPublic);
        using var control = new Control();
        Assert.True(control.CausesValidation);

        Assert.Equal(typeof(CancelEventHandler), typeof(Control).GetEvent(nameof(Control.Validating))!.EventHandlerType);
        Assert.Equal(typeof(EventHandler), typeof(Control).GetEvent(nameof(Control.Validated))!.EventHandlerType);
        Assert.Equal(typeof(EventHandler), typeof(Control).GetEvent(nameof(Control.TabStopChanged))!.EventHandlerType);
        Assert.Equal(typeof(EventHandler), typeof(Control).GetEvent(nameof(Control.CausesValidationChanged))!.EventHandlerType);

        foreach (string name in new[] { "OnValidating", "OnValidated", "OnTabStopChanged", "OnCausesValidationChanged" })
        {
            MethodInfo method = typeof(Control).GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic)!;
            Assert.True(method.IsFamily);
            Assert.Equal(EditorBrowsableState.Advanced, method.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
        }
    }

    [Fact]
    public void TabStopChangedFiresOnlyForAnActualValueChange()
    {
        using var control = new Control();
        int calls = 0;
        control.TabStopChanged += (_, _) => calls++;

        control.TabStop = true;
        control.TabStop = false;
        control.TabStop = false;
        control.TabStop = true;

        Assert.Equal(2, calls);
        Assert.True(control.TabStop);
    }

    [Fact]
    public void CausesValidationChangedObservesCommittedStateAndSupportsRemovalWithoutCreatingAHandle()
    {
        using var control = new Control();
        var values = new List<bool>();
        EventHandler handler = (sender, args) =>
        {
            Assert.Same(control, sender);
            Assert.Same(EventArgs.Empty, args);
            Assert.False(control.IsHandleCreated);
            values.Add(control.CausesValidation);
        };
        control.CausesValidationChanged += handler;

        control.CausesValidation = true;
        control.CausesValidation = false;
        control.CausesValidation = false;
        control.CausesValidation = true;
        control.CausesValidationChanged -= handler;
        control.CausesValidation = false;

        Assert.Equal(new[] { false, true }, values);
        Assert.False(control.CausesValidation);
        Assert.False(control.IsHandleCreated);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ValidationPropertyNotificationExceptionsPreserveCommittedState(bool causesValidation)
    {
        using var control = new Control();
        var failure = new InvalidOperationException("notification failure");
        EventHandler handler = (_, _) => throw failure;
        if (causesValidation) { control.CausesValidationChanged += handler; }
        else { control.TabStopChanged += handler; }

        Exception actual = Assert.Throws<InvalidOperationException>(() =>
        {
            if (causesValidation) { control.CausesValidation = false; }
            else { control.TabStop = false; }
        });

        Assert.Same(failure, actual);
        Assert.False(causesValidation ? control.CausesValidation : control.TabStop);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void ValidationNotificationsHonorComponentEventSuppression()
    {
        using var control = new ValidationControl { AllowEvents = false };
        var calls = new List<string>();
        control.CausesValidationChanged += (_, _) => calls.Add("causes");
        control.TabStopChanged += (_, _) => calls.Add("tabstop");
        control.Validating += (_, _) => calls.Add("validating");
        control.Validated += (_, _) => calls.Add("validated");

        control.CausesValidation = false;
        control.TabStop = false;
        control.RaiseValidating(new CancelEventArgs());
        control.RaiseValidated(EventArgs.Empty);
        Assert.Empty(calls);
        Assert.False(control.CausesValidation);
        Assert.False(control.TabStop);

        control.AllowEvents = true;
        control.CausesValidation = true;
        control.TabStop = true;
        control.RaiseValidating(new CancelEventArgs());
        control.RaiseValidated(EventArgs.Empty);
        Assert.Equal(new[] { "causes", "tabstop", "validating", "validated" }, calls);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void ValidationHooksForwardOriginalArgumentsAndHonorSubscriptionRemoval()
    {
        using var control = new ValidationControl();
        var validatingArgs = new CancelEventArgs();
        var validatedArgs = new EventArgs();
        int calls = 0;
        CancelEventHandler validating = (sender, args) =>
        {
            Assert.Same(control, sender);
            Assert.Same(validatingArgs, args);
            args.Cancel = true;
            calls++;
        };
        EventHandler validated = (sender, args) =>
        {
            Assert.Same(control, sender);
            Assert.Same(validatedArgs, args);
            calls++;
        };
        control.Validating += validating;
        control.Validated += validated;

        control.RaiseValidating(validatingArgs);
        control.RaiseValidated(validatedArgs);
        control.Validating -= validating;
        control.Validated -= validated;
        control.RaiseValidating(new CancelEventArgs());
        control.RaiseValidated(EventArgs.Empty);

        Assert.True(validatingArgs.Cancel);
        Assert.Equal(2, calls);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void ValidationEnumsKeepTheFixedWinFormsValues()
    {
        Assert.Equal(-1, (int)AutoValidate.Inherit);
        Assert.Equal(0, (int)AutoValidate.Disable);
        Assert.Equal(1, (int)AutoValidate.EnablePreventFocusChange);
        Assert.Equal(2, (int)AutoValidate.EnableAllowFocusChange);
        Assert.Equal(0, (int)ValidationConstraints.None);
        Assert.Equal(1, (int)ValidationConstraints.Selectable);
        Assert.Equal(2, (int)ValidationConstraints.Enabled);
        Assert.Equal(4, (int)ValidationConstraints.Visible);
        Assert.Equal(8, (int)ValidationConstraints.TabStop);
        Assert.Equal(16, (int)ValidationConstraints.ImmediateChildren);
        Assert.NotNull(typeof(ValidationConstraints).GetCustomAttribute<FlagsAttribute>());
    }

    // Fixed reference Control.PerformContainerValidation filters Selectable by style,
    // independently of Enabled, Visible and TabStop; the default overload uses Selectable.
    [Theory]
    [InlineData(true, ValidationConstraints.Selectable, "normal|disabled|hidden|non-tabstop")]
    [InlineData(false, ValidationConstraints.None, "normal|disabled|hidden|non-tabstop|non-selectable")]
    [InlineData(false, ValidationConstraints.Selectable, "normal|disabled|hidden|non-tabstop")]
    [InlineData(false, ValidationConstraints.Enabled, "normal|hidden|non-tabstop|non-selectable")]
    [InlineData(false, ValidationConstraints.Visible, "normal|disabled|non-tabstop|non-selectable")]
    [InlineData(false, ValidationConstraints.TabStop, "normal|disabled|hidden|non-selectable")]
    [InlineData(false, ValidationConstraints.Selectable | ValidationConstraints.Enabled | ValidationConstraints.Visible | ValidationConstraints.TabStop, "normal")]
    public void ValidateChildrenAppliesIndependentConstraintsAndSkipsCausesValidationFalse(
        bool useDefault, ValidationConstraints constraints, string expected)
    {
        using var root = new ContainerControl();
        var normal = new ValidationControl { Name = "normal" };
        var disabled = new ValidationControl { Name = "disabled", Enabled = false };
        var hidden = new ValidationControl { Name = "hidden", Visible = false };
        var nonTabStop = new ValidationControl { Name = "non-tabstop", TabStop = false };
        var nonSelectable = new ValidationControl { Name = "non-selectable" };
        nonSelectable.SetSelectable(false);
        var noValidation = new ValidationControl { Name = "no-validation", CausesValidation = false };
        Control[] children = [normal, disabled, hidden, nonTabStop, nonSelectable, noValidation];
        root.Controls.AddRange(children);
        var calls = new List<string>();
        var validated = new List<string>();
        foreach (Control child in children)
        {
            child.Validating += (_, _) => calls.Add(child.Name);
            child.Validated += (_, _) => validated.Add(child.Name);
        }

        bool valid = useDefault ? root.ValidateChildren() : root.ValidateChildren(constraints);

        Assert.True(valid);
        Assert.Equal(expected, string.Join('|', calls));
        Assert.Equal(expected, string.Join('|', validated));
        Assert.False(root.IsHandleCreated);
        Assert.All(children, child => Assert.False(child.IsHandleCreated));
    }

    [Theory]
    [InlineData(false, ValidationConstraints.None, "branch|outside")]
    [InlineData(true, ValidationConstraints.None, "leaf|branch|outside")]
    [InlineData(true, ValidationConstraints.ImmediateChildren, "branch|outside")]
    public void ValidateChildrenRecursesIntoOrdinaryControlOnlyWithContainerStyle(
        bool containerStyle, ValidationConstraints constraints, string expected)
    {
        using var root = new ContainerControl();
        var branch = new ValidationControl { Name = "branch" };
        branch.SetContainerStyle(containerStyle);
        var leaf = new ValidationControl { Name = "leaf" };
        var outside = new ValidationControl { Name = "outside" };
        branch.Controls.Add(leaf);
        root.Controls.AddRange(branch, outside);
        var calls = new List<string>();
        foreach (Control child in new Control[] { branch, leaf, outside })
        {
            child.Validating += (_, _) => calls.Add(child.Name);
        }

        Assert.True(root.ValidateChildren(constraints));

        Assert.Equal(expected, string.Join('|', calls));
        Assert.False(root.IsHandleCreated);
        Assert.False(leaf.IsHandleCreated);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ValidateChildrenVisitsDescendantsEvenWhenTheirParentIsExcluded(bool skipByCausesValidation)
    {
        using var root = new ContainerControl();
        var branch = new ValidationControl { Name = "branch", CausesValidation = !skipByCausesValidation };
        branch.SetContainerStyle(true);
        branch.SetSelectable(skipByCausesValidation);
        var leaf = new ValidationControl { Name = "leaf" };
        var outside = new ValidationControl { Name = "outside" };
        branch.Controls.Add(leaf);
        root.Controls.AddRange(branch, outside);
        var calls = new List<string>();
        foreach (Control child in new Control[] { branch, leaf, outside })
        {
            child.Validating += (_, _) => calls.Add(child.Name);
        }

        Assert.True(root.ValidateChildren());

        Assert.Equal(new[] { "leaf", "outside" }, calls);
    }

    [Fact]
    public void ValidateChildrenAggregatesCancellationAndContinuesThroughAncestorsAndSiblings()
    {
        using var root = new ContainerControl();
        var branch = new ValidationControl { Name = "branch" };
        branch.SetContainerStyle(true);
        var canceled = new ValidationControl { Name = "canceled" };
        var sibling = new ValidationControl { Name = "sibling" };
        var outside = new ValidationControl { Name = "outside" };
        branch.Controls.AddRange(canceled, sibling);
        root.Controls.AddRange(branch, outside);
        var calls = new List<string>();
        foreach (Control child in new Control[] { root, branch, canceled, sibling, outside })
        {
            child.Validating += (_, args) =>
            {
                calls.Add(child.Name + ":validating");
                if (ReferenceEquals(child, canceled)) { args.Cancel = true; }
            };
            child.Validated += (_, _) => calls.Add(child.Name + ":validated");
        }

        Assert.False(root.ValidateChildren(ValidationConstraints.None));

        Assert.Equal(new[]
        {
            "canceled:validating", "sibling:validating", "sibling:validated",
            "branch:validating", "branch:validated", "outside:validating", "outside:validated",
        }, calls);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ValidateChildrenPropagatesOriginalEventExceptionsAndStopsTraversal(bool throwFromValidated)
    {
        using var root = new ContainerControl();
        var first = new Control();
        var second = new Control();
        root.Controls.AddRange(first, second);
        var failure = new InvalidOperationException("bulk validation failure");
        var calls = new List<string>();
        first.Validating += (_, _) =>
        {
            calls.Add("validating");
            if (!throwFromValidated) { throw failure; }
        };
        first.Validated += (_, _) =>
        {
            calls.Add("validated");
            throw failure;
        };
        second.Validating += (_, _) => calls.Add("second");

        Exception actual = Assert.Throws<InvalidOperationException>(() => root.ValidateChildren());

        Assert.Same(failure, actual);
        Assert.Equal(throwFromValidated ? new[] { "validating", "validated" } : new[] { "validating" }, calls);
        Assert.False(root.IsHandleCreated);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(32)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void ValidateChildrenRejectsInvalidFlagsBeforeRaisingValidation(int value)
    {
        using var root = new ContainerControl();
        var child = new Control();
        root.Controls.Add(child);
        int calls = 0;
        child.Validating += (_, _) => calls++;

        InvalidEnumArgumentException failure = Assert.Throws<InvalidEnumArgumentException>(
            () => root.ValidateChildren((ValidationConstraints)value));

        Assert.Equal("validationConstraints", failure.ParamName);
        Assert.Equal(0, calls);
    }

    [Fact]
    public void AutoValidateHasAmbientMetadataAndResolvesThroughOrdinaryParents()
    {
        PropertyInfo property = typeof(ContainerControl).GetProperty(nameof(ContainerControl.AutoValidate))!;
        Assert.True(property.GetMethod!.IsVirtual);
        Assert.Equal(AutoValidate.Inherit, property.GetCustomAttribute<AmbientValueAttribute>()!.Value);
        Assert.Null(property.GetCustomAttribute<DefaultValueAttribute>());
        Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(EditorBrowsableState.Never, property.GetCustomAttribute<EditorBrowsableAttribute>()!.State);

        using var root = new ContainerControl();
        var ordinaryParent = new Control();
        var child = new ValidationContainer { AllowEvents = false };
        root.Controls.Add(ordinaryParent);
        ordinaryParent.Controls.Add(child);
        Assert.Equal(AutoValidate.EnablePreventFocusChange, root.AutoValidate);
        Assert.Equal(AutoValidate.EnablePreventFocusChange, child.AutoValidate);
        var notifications = new List<AutoValidate>();
        EventHandler handler = (sender, args) =>
        {
            Assert.Same(child, sender);
            Assert.Same(EventArgs.Empty, args);
            notifications.Add(child.AutoValidate);
        };
        child.AutoValidateChanged += handler;

        root.AutoValidate = AutoValidate.Disable;
        Assert.Equal(AutoValidate.Disable, child.AutoValidate);
        Assert.Empty(notifications);
        child.AutoValidate = AutoValidate.Disable;
        child.AutoValidate = AutoValidate.Disable;
        root.AutoValidate = AutoValidate.EnableAllowFocusChange;
        Assert.Equal(AutoValidate.Disable, child.AutoValidate);
        child.AutoValidate = AutoValidate.Inherit;
        child.AutoValidate = AutoValidate.Inherit;
        Assert.Equal(AutoValidate.EnableAllowFocusChange, child.AutoValidate);
        root.AutoValidate = AutoValidate.EnablePreventFocusChange;
        Assert.Equal(AutoValidate.EnablePreventFocusChange, child.AutoValidate);
        child.AutoValidateChanged -= handler;
        child.AutoValidate = AutoValidate.Disable;

        // Unlike Component.Events notifications, this event uses its own delegate.
        Assert.Equal(new[] { AutoValidate.Disable, AutoValidate.EnableAllowFocusChange }, notifications);
        Assert.All(new Control[] { root, ordinaryParent, child }, control => Assert.False(control.IsHandleCreated));
    }

    [Fact]
    public void AutoValidateChangedHookForwardsArgumentsAndSetterPreservesStateAfterAnException()
    {
        using var container = new ValidationContainer();
        var eventArgs = new EventArgs();
        EventArgs? observed = null;
        EventHandler observer = (sender, args) =>
        {
            Assert.Same(container, sender);
            observed = args;
        };
        container.AutoValidateChanged += observer;
        container.RaiseAutoValidateChanged(eventArgs);
        Assert.Same(eventArgs, observed);
        container.AutoValidateChanged -= observer;

        var failure = new InvalidOperationException("auto-validation notification failure");
        container.AutoValidateChanged += (_, _) => throw failure;
        Exception actual = Assert.Throws<InvalidOperationException>(
            () => container.AutoValidate = AutoValidate.Disable);

        Assert.Same(failure, actual);
        Assert.Equal(AutoValidate.Disable, container.AutoValidate);
        Assert.False(container.IsHandleCreated);
    }

    [Theory]
    [InlineData(-2)]
    [InlineData(3)]
    [InlineData(int.MaxValue)]
    public void AutoValidateRejectsInvalidValuesWithoutChangingStateOrRaisingEvents(int value)
    {
        using var container = new ContainerControl { AutoValidate = AutoValidate.EnableAllowFocusChange };
        int notifications = 0;
        container.AutoValidateChanged += (_, _) => notifications++;

        InvalidEnumArgumentException failure = Assert.Throws<InvalidEnumArgumentException>(
            () => container.AutoValidate = (AutoValidate)value);

        Assert.Equal("value", failure.ParamName);
        Assert.Equal(AutoValidate.EnableAllowFocusChange, container.AutoValidate);
        Assert.Equal(0, notifications);
        Assert.False(container.IsHandleCreated);
    }

    [Theory]
    [InlineData(AutoValidate.Disable, false)]
    [InlineData(AutoValidate.Disable, true)]
    [InlineData(AutoValidate.EnablePreventFocusChange, false)]
    [InlineData(AutoValidate.EnablePreventFocusChange, true)]
    [InlineData(AutoValidate.EnableAllowFocusChange, false)]
    [InlineData(AutoValidate.EnableAllowFocusChange, true)]
    public void ExplicitValidateRepeatsAndReportsCancellationInEveryAutoValidateMode(AutoValidate mode, bool cancel)
        => OnSta(() =>
        {
            using var root = new ContainerControl { AutoValidate = mode };
            var child = new Control();
            root.Controls.Add(child);
            root.ActiveControl = child;
            int validating = 0;
            int validated = 0;
            child.Validating += (_, args) => { validating++; args.Cancel = cancel; };
            child.Validated += (_, _) => validated++;

            Assert.Equal(!cancel, root.Validate());
            Assert.Equal(!cancel, root.Validate(false));

            Assert.Equal(2, validating);
            Assert.Equal(cancel ? 0 : 2, validated);
            Assert.Same(child, root.ActiveControl);
            Assert.False(root.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void ValidateCheckingAutoValidateSkipsDisabledValidationButExplicitValidateStillRuns()
        => OnSta(() =>
        {
            using var root = new ContainerControl { AutoValidate = AutoValidate.Disable };
            var child = new Control();
            root.Controls.Add(child);
            root.ActiveControl = child;
            int validating = 0;
            int validated = 0;
            child.Validating += (_, args) => { validating++; args.Cancel = true; };
            child.Validated += (_, _) => validated++;

            Assert.True(root.Validate(true));
            Assert.Equal(0, validating);
            Assert.False(root.Validate());
            Assert.Equal(1, validating);
            Assert.True(root.Validate(true));
            Assert.Equal(1, validating);
            root.AutoValidate = AutoValidate.EnableAllowFocusChange;
            Assert.False(root.Validate(true));

            Assert.Equal(2, validating);
            Assert.Equal(0, validated);
            Assert.Same(child, root.ActiveControl);
            Assert.False(root.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ExplicitValidateVisitsNestedActiveControlsAndTheirAncestorsUntilCancellation(bool cancelLeaf)
        => OnSta(() =>
        {
            using var root = new ContainerControl { Name = "root" };
            var outerParent = new Control { Name = "outer-parent" };
            var nested = new ContainerControl { Name = "nested" };
            var innerParent = new Control { Name = "inner-parent" };
            var leaf = new Control { Name = "leaf" };
            root.Controls.Add(outerParent);
            outerParent.Controls.Add(nested);
            nested.Controls.Add(innerParent);
            innerParent.Controls.Add(leaf);
            root.ActiveControl = leaf;
            Control[] controls = [root, outerParent, nested, innerParent, leaf];
            var calls = new List<string>();
            foreach (Control control in controls)
            {
                control.Validating += (_, args) =>
                {
                    calls.Add(control.Name + ":validating");
                    if (ReferenceEquals(control, leaf)) { args.Cancel = cancelLeaf; }
                };
                control.Validated += (_, _) => calls.Add(control.Name + ":validated");
            }

            Assert.Equal(!cancelLeaf, root.Validate());

            Assert.Equal(cancelLeaf ? new[] { "leaf:validating" } : new[]
            {
                "leaf:validating", "leaf:validated", "inner-parent:validating", "inner-parent:validated",
                "nested:validating", "nested:validated", "outer-parent:validating", "outer-parent:validated",
            }, calls);
            Assert.Same(nested, root.ActiveControl);
            Assert.Same(leaf, nested.ActiveControl);
            Assert.All(controls, control => Assert.False(control.IsHandleCreated));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ReentrantValidateReturnsFalseAndLeavesLaterValidationAvailable(bool useNestedContainer)
        => OnSta(() =>
        {
            using var root = new ContainerControl();
            using var nested = new ContainerControl();
            var child = new Control();
            if (useNestedContainer)
            {
                root.Controls.Add(nested);
                nested.Controls.Add(child);
            }
            else { root.Controls.Add(child); }
            root.ActiveControl = child;
            var nestedResults = new List<bool>();
            int validating = 0;
            int validated = 0;
            child.Validating += (_, _) =>
            {
                validating++;
                nestedResults.Add(root.Validate());
                nestedResults.Add(root.Validate(true));
            };
            child.Validated += (_, _) => validated++;

            Assert.True(root.Validate());
            Assert.True(root.Validate());

            Assert.Equal(new[] { false, false, false, false }, nestedResults);
            Assert.Equal(2, validating);
            Assert.Equal(2, validated);
            Assert.All(new Control[] { root, nested, child }, control => Assert.False(control.IsHandleCreated));
        });

    [Fact]
    public void ClosingAFormWithoutAHandleDisposesWithoutValidationOrClosingNotifications()
        => OnSta(() =>
        {
            using var form = new Form();
            var child = new Control();
            form.Controls.Add(child);
            form.ActiveControl = child;
            var calls = new List<string>();
            child.Validating += (_, args) => { calls.Add("validating"); args.Cancel = true; };
            child.Validated += (_, _) => calls.Add("validated");
            form.FormClosing += (_, args) => { calls.Add("closing"); args.Cancel = true; };
            form.Closed += (_, _) => calls.Add("closed");
            form.HandleCreated += (_, _) => calls.Add("form-handle");
            child.HandleCreated += (_, _) => calls.Add("child-handle");
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);

            form.Close();

            Assert.True(form.IsDisposed);
            Assert.True(child.IsDisposed);
            Assert.Empty(calls);
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Theory]
    [InlineData(ValidationConstraints.None, "leaf|first-page|second-page|tabs")]
    [InlineData(ValidationConstraints.ImmediateChildren, "tabs")]
    public void ValidateChildrenRecursesThroughTabControlEvenWithoutContainerStyle(
        ValidationConstraints constraints, string expected)
        => OnSta(() =>
        {
            using var root = new ContainerControl();
            var tabs = new ValidationTabControl { Name = "tabs" };
            var firstPage = new TabPage { Name = "first-page" };
            var secondPage = new TabPage { Name = "second-page" };
            var leaf = new Control { Name = "leaf" };
            firstPage.Controls.Add(leaf);
            tabs.Controls.AddRange(firstPage, secondPage);
            root.Controls.Add(tabs);
            var calls = new List<string>();
            Control[] controls = [root, tabs, firstPage, secondPage, leaf];
            foreach (Control control in controls)
            {
                control.Validating += (_, _) => calls.Add(control.Name);
            }

            Assert.True(root.ValidateChildren(constraints));

            Assert.Equal(expected, string.Join('|', calls));
            Assert.All(controls, control => Assert.False(control.IsHandleCreated));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ValidateChildrenRecursesThroughScrollableControlAndPanel(bool usePanel)
        => OnSta(() =>
        {
            using var root = new ContainerControl();
            Control container = usePanel ? new Panel() : new ScrollableControl();
            container.Name = "container";
            var leaf = new Control { Name = "leaf" };
            container.Controls.Add(leaf);
            root.Controls.Add(container);
            var calls = new List<string>();
            Control[] controls = [root, container, leaf];
            foreach (Control control in controls)
            {
                control.Validating += (_, _) => calls.Add(control.Name);
            }

            Assert.True(root.ValidateChildren(ValidationConstraints.None));

            Assert.Equal(new[] { "leaf", "container" }, calls);
            Assert.All(controls, control => Assert.False(control.IsHandleCreated));
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void DisablingOrHidingTheFocusedControlTransfersToTheNextSelectableSibling()
        => OnSta(() =>
        {
            using var form = new Form { Bounds = new System.Drawing.Rectangle(20, 20, 320, 200) };
            using var first = new Control { TabIndex = 0 };
            using var second = new Control { TabIndex = 1 };
            form.Controls.AddRange(first, second);
            form.Show();

            Assert.True(first.Focus());
            first.Enabled = false;
            Assert.Same(second, form.ActiveControl);
            Assert.True(second.Focused);

            first.Enabled = true;
            Assert.True(first.Focus());
            first.Visible = false;
            Assert.Same(second, form.ActiveControl);
            Assert.True(second.Focused);
        });

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "Control 029 focus check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class ValidationControl : Control
    {
        internal bool AllowEvents = true;
        protected override bool CanRaiseEvents => AllowEvents;
        internal void SetSelectable(bool value) => SetStyle(ControlStyles.Selectable, value);
        internal void SetContainerStyle(bool value) => SetStyle(ControlStyles.ContainerControl, value);
        internal void RaiseValidating(CancelEventArgs args) => OnValidating(args);
        internal void RaiseValidated(EventArgs args) => OnValidated(args);
    }

    private sealed class ValidationContainer : ContainerControl
    {
        internal bool AllowEvents = true;
        protected override bool CanRaiseEvents => AllowEvents;
        internal void RaiseAutoValidateChanged(EventArgs args) => OnAutoValidateChanged(args);
    }

    private sealed class ValidationTabControl : TabControl
    {
        internal ValidationTabControl() => SetStyle(ControlStyles.ContainerControl, false);
    }
}
