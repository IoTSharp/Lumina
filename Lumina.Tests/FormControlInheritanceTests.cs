using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class FormControlInheritanceTests
{
    [Fact]
    public void Form_HasTheStandardComponentAndContainerInheritance()
    {
        Assert.Equal(typeof(ContainerControl), typeof(Form).BaseType);
        Assert.Equal(typeof(ScrollableControl), typeof(ContainerControl).BaseType);
        Assert.Equal(typeof(Control), typeof(ScrollableControl).BaseType);
        Assert.True(typeof(Component).IsAssignableFrom(typeof(Form)));
        Assert.True(typeof(IWin32Window).IsAssignableFrom(typeof(Form)));
        Assert.True(typeof(ISynchronizeInvoke).IsAssignableFrom(typeof(Form)));
        Assert.True(typeof(IContainerControl).IsAssignableFrom(typeof(Form)));
        Assert.Equal(typeof(Control.ControlCollection), typeof(Form).GetProperty(nameof(Form.Controls))!.PropertyType);
    }

    [Theory]
    [InlineData(nameof(Control.Text))]
    [InlineData(nameof(Control.Name))]
    [InlineData(nameof(Control.Handle))]
    [InlineData(nameof(Control.Controls))]
    [InlineData(nameof(Control.BackColor))]
    [InlineData(nameof(Control.ForeColor))]
    [InlineData(nameof(Control.Width))]
    [InlineData(nameof(Control.Height))]
    [InlineData(nameof(Control.ClientSize))]
    [InlineData(nameof(Control.IsDisposed))]
    public void Form_DoesNotDeclareASecondBasicProperty(string name)
    {
        Assert.Null(typeof(Form).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
    }

    [Fact]
    public void FormAndControlReferences_ObserveTheSameStateAndNotifications()
        => OnStaThread(() =>
        {
            using var form = new Form();
            Control control = form;
            int textChanges = 0;
            int colorChanges = 0;
            control.TextChanged += (_, _) => textChanges++;
            control.BackColorChanged += (_, _) => colorChanges++;
            form.Text = "Shared window title";
            form.BackColor = Color.DarkSlateBlue;
            form.Name = "shared";
            control.Size = new Size(420, 260);

            Assert.Equal(form.Text, control.Text);
            Assert.Equal(form.BackColor, control.BackColor);
            Assert.Equal(form.Name, control.Name);
            Assert.Equal(control.Size, form.Size);
            Assert.Equal(1, textChanges);
            Assert.Equal(1, colorChanges);
            Assert.Same(form.Controls, control.Controls);
        });

    [Fact]
    public void Form_DefaultsShareTheFixedReferenceSizeTextVisibilityAndTabStop()
        => OnStaThread(() =>
        {
            using var form = new Form();
            Control control = form;
            Assert.Equal(new Size(300, 300), control.Size);
            Assert.Equal(string.Empty, control.Text);
            Assert.False(form.Visible);
            Assert.False(control.Visible);
            Assert.True(form.TabStop);
            Assert.True(control.TabStop);
            Assert.False(form.IsHandleCreated);
        });

    [Fact]
    public void Form_DirectAndNestedChildrenHaveStandardParents()
        => OnStaThread(() =>
        {
            using var form = new Form();
            var parent = new Control();
            var child = new Control();
            parent.Controls.Add(child);
            form.Controls.Add(parent);

            Assert.Same(form, parent.Parent);
            Assert.Same(parent, child.Parent);
            Assert.Same(form.Controls, ((Control)form).Controls);
            Assert.Single(parent.Controls);
            Assert.False(form.Controls.Contains(child));
        });

    [Fact]
    public void Form_DisposeThroughComponentDisposesOwnedDescendantsAndRaisesDisposed()
        => OnStaThread(() =>
        {
            var form = new Form();
            var parent = new Control();
            var child = new Control();
            parent.Controls.Add(child);
            form.Controls.Add(parent);
            int disposedEvents = 0;
            ((Component)form).Disposed += (_, _) => disposedEvents++;
            ((Component)form).Dispose();

            Assert.True(form.IsDisposed);
            Assert.True(parent.IsDisposed);
            Assert.True(child.IsDisposed);
            Assert.Equal(1, disposedEvents);
            Assert.Null(parent.Parent);
            Assert.Null(child.Parent);
        });

    [Fact]
    public void Form_CloseBeforeCreationDisposesItsControlTree()
        => OnStaThread(() =>
        {
            var form = new Form();
            var child = new Control();
            form.Controls.Add(child);
            form.Close();
            Assert.True(((Control)form).IsDisposed);
            Assert.True(child.IsDisposed);
            Assert.False(form.IsHandleCreated);
        });

    [Fact]
    public void Form_LayoutSuspensionDefersItsLayoutEventUntilOutermostResume()
        => OnStaThread(() =>
        {
            using var form = new Form();
            int layouts = 0;
            form.Layout += (_, _) => layouts++;
            ((Control)form).SuspendLayout();
            form.SuspendLayout();
            form.PerformLayout();
            form.ResumeLayout();
            Assert.Equal(0, layouts);
            ((Control)form).ResumeLayout();
            Assert.Equal(1, layouts);
        });

    [Fact]
    public void ContainerControl_ActivationUsesTheStandardExplicitInterface()
    {
        Assert.Null(typeof(ContainerControl).GetMethod(nameof(IContainerControl.ActivateControl), BindingFlags.Public | BindingFlags.Instance));
        InterfaceMapping mapping = typeof(ContainerControl).GetInterfaceMap(typeof(IContainerControl));
        int index = Array.FindIndex(mapping.InterfaceMethods, method => method.Name == nameof(IContainerControl.ActivateControl));
        Assert.True(index >= 0);
        Assert.True(mapping.TargetMethods[index].IsPrivate);
    }

    [Fact]
    public void ContainerControl_ActiveControlRejectsAnUnrelatedControlWithoutMutatingTheSelection()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var outside = new Control();
            var child = new Control();
            form.Controls.Add(child);
            form.ActiveControl = child;
            Assert.Throws<ArgumentException>(() => form.ActiveControl = outside);
            Assert.Same(child, form.ActiveControl);
            Assert.True(((IContainerControl)form).ActivateControl(outside));
            Assert.Same(outside, form.ActiveControl);
        });

    [Fact]
    public void ContainerControl_ActivationSynchronizesTheNestedContainerChain()
        => OnStaThread(() =>
        {
            using var form = new Form();
            var container = new ContainerControl();
            var child = new Control();
            container.Controls.Add(child);
            form.Controls.Add(container);
            form.ActiveControl = child;
            Assert.Same(container, form.ActiveControl);
            Assert.Same(child, container.ActiveControl);
            Assert.True(((IContainerControl)form).ActivateControl(child));
            Assert.Same(child, form.ActiveControl);
            Assert.Same(child, container.ActiveControl);
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void ContainerControl_InterfaceActivationAcceptsNullAndNormalizesSelfToNull()
        => OnStaThread(() =>
        {
            using var container = new ContainerControl();
            using var outside = new Control();
            IContainerControl contract = container;
            Assert.True(contract.ActivateControl(null!));
            Assert.Null(container.ActiveControl);
            Assert.True(contract.ActivateControl(outside));
            Assert.Same(outside, container.ActiveControl);
            Assert.True(contract.ActivateControl(container));
            Assert.Null(container.ActiveControl);
        });

    [Fact]
    public void ContainerControl_ClearingTheActiveNestedContainerClearsItsSelection()
        => OnStaThread(() =>
        {
            using var container = new ContainerControl();
            var nested = new ContainerControl();
            var child = new Control();
            container.Controls.Add(nested);
            nested.Controls.Add(child);
            container.ActiveControl = child;
            container.ActiveControl = null;
            Assert.Null(container.ActiveControl);
            Assert.Null(nested.ActiveControl);
        });

    [Fact]
    public void ContainerControl_DisposalClearsEachActiveSelectionAndPreservesStoredVisibility()
        => OnStaThread(() =>
        {
            var container = new ContainerControl();
            var nested = new ContainerControl();
            var child = new Control();
            container.Controls.Add(nested);
            nested.Controls.Add(child);
            container.ActiveControl = child;

            container.Dispose();

            Assert.Null(container.ActiveControl);
            Assert.Null(nested.ActiveControl);
            Assert.True(container.Visible);
            Assert.True(nested.Visible);
            Assert.True(child.Visible);
            Assert.True(container.IsDisposed);
            Assert.True(nested.IsDisposed);
            Assert.True(child.IsDisposed);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void ContainerControl_RemovingTheActiveChildSkipsANestedContainerWithoutATabStop()
        => OnStaThread(() =>
        {
            using var container = new ContainerControl();
            using var active = new Control { TabIndex = 0 };
            var nested = new ContainerControl { TabIndex = 1, TabStop = false };
            var leaf = new Control();
            var remaining = new Control { TabIndex = 2 };
            nested.Controls.Add(leaf);
            container.Controls.AddRange(active, nested, remaining);
            container.ActiveControl = active;

            container.Controls.Remove(active);

            Assert.Same(remaining, container.ActiveControl);
            Assert.Null(nested.ActiveControl);
            Assert.True(container.IsHandleCreated);
            Assert.True(nested.IsHandleCreated);
            Assert.True(leaf.IsHandleCreated);
            Assert.Equal(container.Handle, GetParent(nested.Handle));
            Assert.Equal(nested.Handle, GetParent(leaf.Handle));
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    [Trait("Category", "WindowsHandle")]
    public void ContainerControl_RemovingTheActiveChildCreatesVisibleButNotHiddenSiblings(bool hidden)
        => OnStaThread(() =>
        {
            using var container = new ContainerControl();
            using var active = new Control { TabIndex = 0 };
            var first = new Control { TabIndex = 1, Visible = !hidden, Enabled = hidden };
            var remaining = new Control { TabIndex = 2 };
            container.Controls.AddRange(active, first, remaining);
            container.ActiveControl = active;

            container.Controls.Remove(active);

            Assert.Same(remaining, container.ActiveControl);
            Assert.True(container.IsHandleCreated);
            Assert.Equal(!hidden, first.IsHandleCreated);
            Assert.True(remaining.IsHandleCreated);
            Assert.Equal(container.Handle, GetParent(remaining.Handle));
        });

    [Fact]
    public void ContainerControl_RemovingTheOnlySelectableChildDoesNotCreateHandles()
        => OnStaThread(() =>
        {
            using var container = new ContainerControl();
            using var active = new Control { TabIndex = 0 };
            var noTabStop = new Control { TabIndex = 1, TabStop = false };
            var hidden = new Control { TabIndex = 2, Visible = false };
            var disabled = new Control { TabIndex = 3, Enabled = false };
            container.Controls.AddRange(active, noTabStop, hidden, disabled);
            container.ActiveControl = active;

            container.Controls.Remove(active);

            Assert.Null(container.ActiveControl);
            Assert.False(container.IsHandleCreated);
            Assert.False(noTabStop.IsHandleCreated);
            Assert.False(hidden.IsHandleCreated);
            Assert.False(disabled.IsHandleCreated);
        });

    [Fact]
    public void ContainerControl_RemovingOrDisposingTheActiveChildSelectsTheRemainingContainer()
        => OnStaThread(() =>
        {
            using var container = new ContainerControl();
            using var child = new Control();
            var nested = new ContainerControl();
            container.Controls.AddRange(child, nested);
            container.ActiveControl = child;
            container.Controls.Remove(child);
            Assert.Same(nested, container.ActiveControl);
            container.Controls.Add(child);
            container.ActiveControl = child;
            child.Dispose();
            Assert.Same(nested, container.ActiveControl);
        });

    [Fact]
    public void ContainerControl_RemovingTheActiveSubtreeClearsItsAncestorsSelection()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var container = new ContainerControl();
            var child = new Control();
            container.Controls.Add(child);
            form.Controls.Add(container);
            form.ActiveControl = child;
            form.Controls.Remove(container);
            Assert.Null(form.ActiveControl);
            Assert.Null(container.Parent);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_NativeHandleAndClientBoundsAreSharedWithControl()
        => OnStaThread(() =>
        {
            using var form = new Form { ClientSize = new Size(310, 180) };
            Control control = form;
            form.Show();
            Assert.NotEqual(0, control.Handle);
            Assert.Equal(control.Handle, ((IWin32Window)form).Handle);
            Assert.True(GetClientRect(control.Handle, out RECT client));
            Assert.Equal(new Size(client.Right, client.Bottom), control.ClientSize);
            Assert.Equal(new Size(310, 180), control.ClientSize);
            Assert.True(GetWindowRect(control.Handle, out RECT window));
            Assert.Equal(new Size(window.Right - window.Left, window.Bottom - window.Top), control.Size);
            control.Text = "Updated native title";
            var title = new StringBuilder(128);
            _ = GetWindowTextW(form.Handle, title, title.Capacity);
            Assert.Equal(control.Text, title.ToString());
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_HideThenShowUsesTheSameHandleAndDoesNotRepeatLoadOrShown()
        => OnStaThread(() =>
        {
            using var form = new Form();
            Control control = form;
            int loads = 0;
            int shown = 0;
            form.Load += (_, _) => loads++;
            form.Shown += (_, _) => shown++;
            control.Visible = true;
            nint handle = form.Handle;
            Assert.NotEqual(0, handle);
            Assert.True(control.Visible);
            form.Hide();
            Assert.False(control.Visible);
            form.Show();
            Assert.Equal(handle, control.Handle);
            Assert.True(control.Visible);
            Assert.Equal(1, loads);
            // Shown is posted by the first Show and runs on the UI queue.
            form.EndInvoke(form.BeginInvoke((Action)(() => { })));
            Assert.Equal(1, shown);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_NativeResizeUpdatesTheControlBoundsAndClientCache()
        => OnStaThread(() =>
        {
            using var form = new Form();
            form.Show();
            Assert.True(MoveWindow(form.Handle, 40, 60, 460, 340, true));
            Assert.Equal(new Size(460, 340), ((Control)form).Size);
            Assert.True(GetClientRect(form.Handle, out RECT client));
            Assert.Equal(new Size(client.Right, client.Bottom), form.ClientSize);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_NativeMoveUpdatesTheSharedLocationWithoutAResizeNotification()
        => OnStaThread(() =>
        {
            using var form = new Form();
            form.Show();
            Size initialSize = form.Size;
            int moves = 0;
            int resizes = 0;
            ((Control)form).LocationChanged += (_, _) => moves++;
            ((Control)form).SizeChanged += (_, _) => resizes++;

            Assert.True(MoveWindow(form.Handle, 70, 90, initialSize.Width, initialSize.Height, true));
            Assert.Equal(new Point(70, 90), ((Control)form).Location);
            Assert.Equal(initialSize, form.Size);
            Assert.Equal(1, moves);
            Assert.Equal(0, resizes);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_NestedPlainControlCreatesTheRealNativeParentTree()
        => OnStaThread(() =>
        {
            using var form = new Form();
            var parent = new Control { Size = new Size(100, 100) };
            var child = new Button();
            parent.Controls.Add(child);
            form.Controls.Add(parent);
            form.Show();
            Assert.Equal(form.Handle, GetParent(parent.Handle));
            Assert.Equal(parent.Handle, GetParent(child.Handle));
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Form_CloseHonorsCancellationThenDisposesTheSharedControlState()
        => OnStaThread(() =>
        {
            var form = new Form();
            var child = new Button();
            form.Controls.Add(child);
            bool cancel = true;
            form.FormClosing += (_, e) => e.Cancel = cancel;
            form.Show();
            form.Close();
            Assert.False(((Control)form).IsDisposed);
            Assert.True(form.IsHandleCreated);
            cancel = false;
            form.Close();
            Assert.True(((Control)form).IsDisposed);
            Assert.True(child.IsDisposed);
            Assert.False(form.IsHandleCreated);
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    [Trait("Category", "WindowsHandle")]
    public void Form_DisposalClearsSharedStateWithoutVisibleChangedBeforeDisposed(bool shown, bool close)
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var child = new Control();
            form.Controls.Add(child);
            if (shown) { form.Show(); }
            form.ActiveControl = child;
            nint handle = form.IsHandleCreated ? form.Handle : 0;
            List<string> events = [];
            string State(string name) => $"{name}:{form.Visible}/{child.Visible}:{form.IsDisposed}/{child.IsDisposed}:{form.Disposing}/{child.Disposing}:{form.ActiveControl is null}";
            form.VisibleChanged += (_, _) => events.Add("form-visible");
            child.VisibleChanged += (_, _) => events.Add("child-visible");
            child.Disposed += (_, _) => events.Add(State("child-disposed"));
            form.Disposed += (_, _) => events.Add(State("form-disposed"));

            if (close) { form.Close(); }
            else { form.Dispose(); }

            Assert.Equal([
                "child-disposed:False/True:False/False:True/True:True",
                "form-disposed:False/True:False/True:True/False:True",
            ], events);
            Assert.Null(form.ActiveControl);
            Assert.False(form.Visible);
            Assert.False(((Control)form).Visible);
            Assert.True(child.Visible);
            Assert.True(form.IsDisposed);
            Assert.True(child.IsDisposed);
            Assert.False(form.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
            Assert.False(IsWindow(handle));
        });

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The form inheritance check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT { public int Left, Top, Right, Bottom; }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetClientRect(nint handle, out RECT rectangle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint handle, out RECT rectangle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool MoveWindow(nint handle, int x, int y, int width, int height, [MarshalAs(UnmanagedType.Bool)] bool repaint);
    [DllImport("user32.dll")]
    private static extern nint GetParent(nint handle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint handle, StringBuilder text, int maxCount);
}
