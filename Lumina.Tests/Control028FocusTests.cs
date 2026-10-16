using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class Control028FocusTests
{
    [Fact]
    public void SelectionAndFocusMembersPreserveStandardVisibilityAndMetadata()
    {
        PropertyInfo canSelect = typeof(Control).GetProperty(nameof(Control.CanSelect))!;
        Assert.Equal(typeof(bool), canSelect.PropertyType);
        Assert.False(canSelect.GetMethod!.IsVirtual);
        Assert.Null(canSelect.SetMethod);
        Assert.False(canSelect.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
        Assert.Equal(DesignerSerializationVisibility.Hidden,
            canSelect.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        Assert.Equal(EditorBrowsableState.Advanced, canSelect.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
        Assert.Equal("Focus", canSelect.GetCustomAttribute<CategoryAttribute>()!.Category);
        Assert.Equal("Checks if this control can be selected.", canSelect.GetCustomAttribute<DescriptionAttribute>()!.Description);

        MethodInfo select = typeof(Control).GetMethod(nameof(Control.Select), Type.EmptyTypes)!;
        Assert.Equal(typeof(void), select.ReturnType);
        Assert.False(select.IsVirtual);
        MethodInfo directed = typeof(Control).GetMethod(nameof(Control.Select),
            BindingFlags.Instance | BindingFlags.NonPublic, [typeof(bool), typeof(bool)])!;
        Assert.True(directed.IsFamily);
        Assert.True(directed.IsVirtual);
        Assert.Equal(["directed", "forward"], directed.GetParameters().Select(p => p.Name));

        foreach (string name in new[] { "OnEnter", "OnLeave", "OnGotFocus", "OnLostFocus", "InvokeGotFocus", "InvokeLostFocus" })
        {
            MethodInfo method = typeof(Control).GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic)!;
            Assert.Equal(EditorBrowsableState.Advanced, method.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
            Assert.Equal(name is "OnEnter" or "OnLeave", method.IsFamilyOrAssembly);
            Assert.Equal(!name.StartsWith("Invoke", StringComparison.Ordinal), method.IsVirtual);
        }
        foreach (string name in new[] { nameof(Control.GotFocus), nameof(Control.LostFocus) })
        {
            EventInfo focusEvent = typeof(Control).GetEvent(name)!;
            Assert.Equal(typeof(EventHandler), focusEvent.EventHandlerType);
            Assert.False(focusEvent.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
            Assert.Equal(EditorBrowsableState.Advanced, focusEvent.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
            Assert.Equal("Focus", focusEvent.GetCustomAttribute<CategoryAttribute>()!.Category);
        }
        foreach ((string name, string description) in new[]
        {
            (nameof(Control.Enter), "Occurs when the control becomes the active control of the form."),
            (nameof(Control.Leave), "Occurs when the control is no longer the active control of the form."),
            (nameof(Control.GotFocus), "Occurs when the control gets focus."),
            (nameof(Control.LostFocus), "Occurs when the control loses focus.")
        })
        {
            Assert.Equal(description, typeof(Control).GetEvent(name)!.GetCustomAttribute<DescriptionAttribute>()!.Description);
        }
    }

    [Fact]
    public void CanSelectUsesManagedAncestorStateAndSelectableStyleWithoutRequiringFocusOrTabStop()
        => OnSta(() =>
        {
            using var parent = new Control();
            var child = new ProbeControl { TabStop = false };
            parent.Controls.Add(child);
            Assert.True(child.CanSelect);
            Assert.False(child.CanFocus);
            Assert.False(child.Focus());
            Assert.False(child.IsHandleCreated);
            child.Selectable(false);
            Assert.False(child.CanSelect);
            child.Selectable(true);
            parent.Enabled = false;
            Assert.False(child.CanSelect);
            parent.Enabled = true;
            parent.Visible = false;
            Assert.False(child.CanSelect);
            parent.Visible = true;
            Assert.True(child.CanSelect);
            Assert.False(parent.IsHandleCreated);
            // The inherited visibility notification creates the child window,
            // even though its detached parent still has no window of its own.
            Assert.True(child.IsHandleCreated);
            child.Dispose();
            Assert.False(child.Visible);
            Assert.False(child.CanSelect);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void DisposedUncreatedControlRemainsSelectableWithoutCreatingAHandle()
        => OnSta(() =>
        {
            var control = new Control();
            Assert.False(control.IsHandleCreated);
            control.Dispose();
            Assert.True(control.IsDisposed);
            Assert.True(control.CanSelect);
            Assert.True(control.Visible);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void HiddenFormPreventsChildSelectionBeforeTheFormIsShown()
        => OnSta(() =>
        {
            using var form = new Form();
            var child = new Control();
            form.Controls.Add(child);
            Assert.False(form.CanSelect);
            Assert.False(child.CanSelect);
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void ContainerLookupRequiresBothTheStyleAndInterfaceAndReturnsAContainerItself()
        => OnSta(() =>
        {
            using var outer = new ContainerControl();
            var plain = new ProbeControl();
            var custom = new InterfaceContainer();
            var leaf = new Control();
            outer.Controls.Add(plain);
            plain.Controls.Add(custom);
            custom.Controls.Add(leaf);
            Assert.Same(outer, outer.GetContainerControl());
            Assert.Same(outer, leaf.GetContainerControl());
            custom.ContainerStyle(true);
            Assert.Same(custom, custom.GetContainerControl());
            Assert.Same(custom, leaf.GetContainerControl());
            custom.ContainerStyle(false);
            plain.ContainerStyle(true);
            Assert.Same(outer, leaf.GetContainerControl());
            using var detached = new Control();
            Assert.Null(detached.GetContainerControl());
            Assert.False(outer.IsHandleCreated);
            Assert.False(leaf.IsHandleCreated);
        });

    [Fact]
    public void GetNextControlUsesTabIndexThenCollectionOrderWithoutApplyingSelectionFilters()
        => OnSta(() =>
        {
            using var root = new Control();
            var a = new Control { Name = "a", TabIndex = 2 };
            var b = new Control { Name = "b", TabIndex = 0, Visible = false };
            var c = new Control { Name = "c", TabIndex = 2, Enabled = false };
            var d = new Control { Name = "d", TabIndex = 1, TabStop = false };
            root.Controls.AddRange(a, b, c, d);
            Assert.Equal(["b", "d", "a", "c"], Walk(root, true));
            Assert.Equal(["c", "a", "d", "b"], Walk(root, false));
            root.Controls.SetChildIndex(c, 0);
            Assert.Equal(["b", "d", "c", "a"], Walk(root, true));
            Assert.Equal(["a", "c", "d", "b"], Walk(root, false));
            using var unrelated = new Control();
            Assert.Same(b, root.GetNextControl(unrelated, true));
            Assert.Same(a, root.GetNextControl(root, false));
            Assert.False(root.IsHandleCreated);
        });

    [Fact]
    public void GetNextControlDescendsPlainControlsButTreatsFocusContainersAsBoundaries()
        => OnSta(() =>
        {
            using var root = new Control();
            var plain = new Control { Name = "plain", TabIndex = 0 };
            var leaf = new Control { Name = "leaf" };
            var nested = new ContainerControl { Name = "nested", TabIndex = 1 };
            var inside = new Control { Name = "inside" };
            var last = new Control { Name = "last", TabIndex = 2 };
            plain.Controls.Add(leaf);
            nested.Controls.Add(inside);
            root.Controls.AddRange(plain, nested, last);
            Assert.Equal(["plain", "leaf", "nested", "last"], Walk(root, true));
            Assert.Equal(["last", "nested", "leaf", "plain"], Walk(root, false));
            Assert.Same(last, root.GetNextControl(inside, true));
            Assert.Same(nested, root.GetNextControl(inside, false));
            Assert.Same(inside, nested.GetNextControl(null, true));
            Assert.Null(nested.GetNextControl(inside, true));
            Assert.False(inside.IsHandleCreated);
        });

    [Theory]
    [InlineData("disabled")]
    [InlineData("hidden")]
    [InlineData("tabstop")]
    [InlineData("style")]
    public void SelectDoesNotFilterCanSelectOrCreateHandles(string filter)
        => OnSta(() =>
        {
            using var container = new ContainerControl();
            var child = new ProbeControl();
            container.Controls.Add(child);
            if (filter == "disabled") { child.Enabled = false; }
            if (filter == "hidden") { child.Visible = false; }
            if (filter == "tabstop") { child.TabStop = false; }
            if (filter == "style") { child.Selectable(false); }
            child.Select();
            Assert.Same(child, container.ActiveControl);
            Assert.Equal([(false, false)], child.Selections);
            Assert.False(child.IsHandleCreated);
            Assert.False(container.IsHandleCreated);
        });

    [Fact]
    public void SelectUsesTheProtectedVirtualHookAndPreservesAnOriginalException()
        => OnSta(() =>
        {
            using var container = new ContainerControl();
            var child = new ProbeControl { SkipSelection = true };
            container.Controls.Add(child);
            child.Select();
            child.SelectDirected(true, false);
            Assert.Equal([(false, false), (true, false)], child.Selections);
            Assert.Null(container.ActiveControl);
            var failure = new InvalidOperationException("selection failure");
            child.SelectionFailure = failure;
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(child.Select));
        });

    [Theory]
    [InlineData(true, true, true, false, "inside")]
    [InlineData(true, true, false, false, "end")]
    [InlineData(true, false, true, false, "plain")]
    [InlineData(true, false, false, false, "plain")]
    [InlineData(false, false, true, false, "nested")]
    [InlineData(false, true, true, false, "inside")]
    [InlineData(false, true, false, false, null)]
    public void SelectNextControlHonorsDirectionTabStopAndNesting(bool forward, bool tabStopOnly,
        bool nested, bool wrap, string? expected)
        => OnSta(() =>
        {
            using var root = new InterfaceContainer();
            root.ContainerStyle(true);
            var first = new Control { Name = "first", TabIndex = 0, Enabled = false };
            var plain = new ProbeControl { Name = "plain", TabIndex = 1, TabStop = false };
            var inside = new ProbeControl { Name = "inside" };
            var container = new ContainerControl { Name = "nested", TabIndex = 2, TabStop = false };
            var hidden = new ProbeControl { Name = "hidden", TabIndex = 3, Visible = false };
            var end = new ProbeControl { Name = "end", TabIndex = 4 };
            plain.Controls.Add(inside);
            root.Controls.AddRange(first, plain, container, hidden, end);
            bool result = root.SelectNextControl(forward ? first : end, forward, tabStopOnly, nested, wrap);
            Assert.Equal(expected is not null, result);
            Assert.Equal(expected, root.ActiveControl?.Name);
            Assert.False(root.IsHandleCreated);
            Assert.False(inside.IsHandleCreated);
        });

    [Fact]
    public void SelectNextControlWrapsOnceAndCanReselectItsStartingControl()
        => OnSta(() =>
        {
            using var root = new Control();
            Assert.False(root.SelectNextControl(null, true, true, true, true));
            var only = new ProbeControl { SkipSelection = true };
            root.Controls.Add(only);
            Assert.True(root.SelectNextControl(only, true, true, true, true));
            Assert.Equal([(true, true)], only.Selections);
            Assert.False(root.SelectNextControl(only, true, true, true, false));
            only.Enabled = false;
            Assert.False(root.SelectNextControl(null, false, false, true, true));
            only.Enabled = true;
            Assert.True(root.SelectNextControl(null, false, true, false, false));
            Assert.Equal((true, false), only.Selections[^1]);
            Assert.False(only.IsHandleCreated);
        });

    [Theory]
    [InlineData("enter")]
    [InlineData("leave")]
    [InlineData("got")]
    [InlineData("lost")]
    public void DirectFocusEventsKeepSenderArgumentsHandlerRemovalAndOriginalExceptions(string kind)
        => OnSta(() =>
        {
            using var control = new ProbeControl();
            object? sender = null;
            EventArgs? observed = null;
            EventHandler handler = (s, e) => { sender = s; observed = e; };
            control.Subscribe(kind, handler, true);
            var args = new EventArgs();
            control.Raise(kind, args);
            Assert.Same(control, sender);
            Assert.Same(args, observed);
            control.Raise(kind, null!);
            Assert.Null(observed);
            control.Subscribe(kind, handler, false);
            control.Raise(kind, args);
            Assert.Null(observed);
            var failure = new InvalidOperationException("event failure");
            control.Subscribe(kind, (_, _) => throw failure, true);
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => control.Raise(kind, args)));
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void FocusHelpersCallTheTargetVirtualMethodsAndAcceptANullTarget()
        => OnSta(() =>
        {
            using var caller = new ProbeControl();
            using var target = new ProbeControl();
            var args = new EventArgs();
            object? sender = null;
            EventArgs? observed = null;
            target.GotFocus += (s, e) => { sender = s; observed = e; };
            target.LostFocus += (s, e) => { sender = s; observed = e; };
            caller.InvokeFocus(null, args, true);
            caller.InvokeFocus(null, args, false);
            Assert.Null(sender);
            caller.InvokeFocus(target, args, true);
            Assert.Equal(["got"], target.EventsRaised);
            Assert.Same(target, sender);
            Assert.Same(args, observed);
            caller.InvokeFocus(target, null!, false);
            Assert.Equal(["got", "lost"], target.EventsRaised);
            Assert.Null(observed);
            Assert.False(caller.IsHandleCreated);
            Assert.False(target.IsHandleCreated);
        });

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void SetFocusMessageActivatesTheContainerBeforeDefaultProcessingAndGotFocus(bool activate)
        => OnSta(() =>
        {
            var calls = new List<string>();
            using var container = new InterfaceContainer { Activation = activate, Calls = calls };
            container.ContainerStyle(true);
            var child = new ProbeControl { NativeCalls = calls };
            container.Controls.Add(child);
            child.GotFocus += (_, _) => calls.Add("got-event");
            child.Enter += (_, _) => calls.Add("enter-event");
            child.Dispatch(0x0007);
            Assert.Equal(activate ? new[] { "activate", "default:7", "got-event" } : ["activate"], calls);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void KillFocusMessageProcessesTheDefaultProcedureBeforeLostFocusWithoutInventingLeave()
        => OnSta(() =>
        {
            var calls = new List<string>();
            using var child = new ProbeControl { NativeCalls = calls };
            child.LostFocus += (_, _) => calls.Add("lost-event");
            child.Leave += (_, _) => calls.Add("leave-event");
            child.Dispatch(0x0008);
            Assert.Equal(["default:8", "lost-event"], calls);
            Assert.False(child.IsHandleCreated);
        });

    private static string[] Walk(Control root, bool forward)
    {
        var names = new List<string>();
        Control? current = null;
        while ((current = root.GetNextControl(current, forward)) is not null)
        {
            names.Add(current.Name);
            Assert.True(names.Count <= 20, "Tab traversal must terminate.");
        }
        return names.ToArray();
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "Focus contract check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class InterfaceContainer : Control, IContainerControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control? ActiveControl { get; set; }
        public bool Activation = true;
        public List<string>? Calls;
        public bool ActivateControl(Control active)
        {
            Calls?.Add("activate");
            if (Activation) { ActiveControl = active; }
            return Activation;
        }
        public void ContainerStyle(bool value) => SetStyle(ControlStyles.ContainerControl, value);
    }

    private sealed class ProbeControl : Control
    {
        public readonly List<(bool Directed, bool Forward)> Selections = [];
        public readonly List<string> EventsRaised = [];
        public List<string>? NativeCalls;
        public bool SkipSelection;
        public Exception? SelectionFailure;
        public void Selectable(bool value) => SetStyle(ControlStyles.Selectable, value);
        public void ContainerStyle(bool value) => SetStyle(ControlStyles.ContainerControl, value);
        public void SelectDirected(bool directed, bool forward) => Select(directed, forward);
        protected override void Select(bool directed, bool forward)
        {
            Selections.Add((directed, forward));
            if (SelectionFailure is not null) { ExceptionDispatchInfo.Capture(SelectionFailure).Throw(); }
            if (!SkipSelection) { base.Select(directed, forward); }
        }
        public void Raise(string kind, EventArgs e)
        {
            switch (kind)
            {
                case "enter": OnEnter(e); break;
                case "leave": OnLeave(e); break;
                case "got": OnGotFocus(e); break;
                case "lost": OnLostFocus(e); break;
                default: throw new ArgumentOutOfRangeException(nameof(kind));
            }
        }
        public void Subscribe(string kind, EventHandler handler, bool add)
        {
            switch (kind)
            {
                case "enter": if (add) { Enter += handler; } else { Enter -= handler; } break;
                case "leave": if (add) { Leave += handler; } else { Leave -= handler; } break;
                case "got": if (add) { GotFocus += handler; } else { GotFocus -= handler; } break;
                case "lost": if (add) { LostFocus += handler; } else { LostFocus -= handler; } break;
                default: throw new ArgumentOutOfRangeException(nameof(kind));
            }
        }
        public void InvokeFocus(Control? target, EventArgs e, bool got)
        {
            if (got) { InvokeGotFocus(target, e); }
            else { InvokeLostFocus(target, e); }
        }
        protected override void OnGotFocus(EventArgs e) { EventsRaised.Add("got"); base.OnGotFocus(e); }
        protected override void OnLostFocus(EventArgs e) { EventsRaised.Add("lost"); base.OnLostFocus(e); }
        public void Dispatch(int message)
        {
            Message value = Message.Create(0, message, 0, 0);
            WndProc(ref value);
        }
        protected override void DefWndProc(ref Message m)
        {
            if (NativeCalls is not null) { NativeCalls.Add("default:" + m.Msg); }
            else { base.DefWndProc(ref m); }
        }
    }
}
