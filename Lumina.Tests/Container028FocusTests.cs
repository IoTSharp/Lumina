using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class Container028FocusTests
{
    [Fact]
    public void ActiveControl_EntersAncestorsBeforeChildrenAndClearsFromTheLeaf()
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            List<string> events = [];
            foreach (Control control in new Control[] { tree.Nested, tree.First, tree.Second })
            {
                control.Enter += (_, _) => events.Add($"{control.Name}.Enter:{tree.State}");
                control.Leave += (_, _) => events.Add($"{control.Name}.Leave:{tree.State}");
            }

            tree.Root.ActiveControl = tree.First;
            tree.Root.ActiveControl = tree.Second;
            tree.Root.ActiveControl = null;

            Assert.Equal(new[]
            {
                "nested.Enter:nested/null", "first.Enter:nested/first",
                "first.Leave:nested/second", "second.Enter:nested/second",
                "second.Leave:null/null", "nested.Leave:null/null",
            }, events);
            Assert.Null(tree.Root.ActiveControl);
            Assert.Null(tree.Nested.ActiveControl);
            Assert.All(tree.Controls, control => Assert.False(control.IsHandleCreated));
        });

    [Fact]
    public void ActiveControl_PlainAncestorsReceiveEnterAndLeaveWithoutBecomingActive()
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            List<string> events = Track(tree.Controls);

            tree.Root.ActiveControl = tree.First;
            events.Clear();
            tree.Root.ActiveControl = tree.Leaf;
            Assert.Equal(new[] { "first.Leave", "nested.Leave", "plain.Enter", "leaf.Enter" }, events);
            Assert.Same(tree.Leaf, tree.Root.ActiveControl);
            Assert.Null(tree.Nested.ActiveControl);

            events.Clear();
            tree.Root.ActiveControl = tree.Last;
            Assert.Equal(new[] { "leaf.Leave", "plain.Leave", "last.Enter" }, events);
        });

    [Fact]
    public void InterfaceActivation_PreservesTheDirectTargetAndAcceptsOutsideSelfAndNull()
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            using var outside = new Control { Name = "outside" };
            List<string> events = Track([.. tree.Controls, outside]);
            IContainerControl contract = tree.Root;

            Assert.True(contract.ActivateControl(tree.First));
            Assert.Same(tree.First, tree.Root.ActiveControl);
            Assert.Null(tree.Nested.ActiveControl);
            Assert.Equal(new[] { "nested.Enter", "first.Enter" }, events);

            events.Clear();
            Assert.True(contract.ActivateControl(outside));
            Assert.Same(outside, tree.Root.ActiveControl);
            Assert.Equal(new[] { "first.Leave", "nested.Leave", "outside.Enter" }, events);

            events.Clear();
            Assert.True(contract.ActivateControl(tree.Root));
            Assert.True(contract.ActivateControl(null!));
            Assert.Null(tree.Root.ActiveControl);
            Assert.Equal(new[] { "outside.Leave" }, events);
            Assert.All(tree.Controls, control => Assert.False(control.IsHandleCreated));
        });

    [Fact]
    public void ActiveControl_RejectsOutsideAndSelfWithoutChangingTheSelectedPath()
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            using var outside = new Control();
            tree.Root.ActiveControl = tree.First;
            List<string> events = Track(tree.Controls);

            foreach (Control value in new[] { outside, tree.Root })
            {
                ArgumentException failure = Assert.Throws<ArgumentException>(() => tree.Root.ActiveControl = value);
                Assert.Equal("value", failure.ParamName);
                Assert.StartsWith("Invisible or disabled control cannot be activated", failure.Message);
            }

            Assert.Same(tree.Nested, tree.Root.ActiveControl);
            Assert.Same(tree.First, tree.Nested.ActiveControl);
            Assert.Empty(events);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DirectSelect_UpdatesTheActivePathEvenWhenTheTargetCannotBeSelected(bool hidden)
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            tree.Root.ActiveControl = tree.First;
            if (hidden) { tree.Second.Visible = false; }
            else { tree.Second.Enabled = false; }
            List<string> events = Track(tree.Controls);

            tree.Second.Select();

            Assert.False(tree.Second.CanSelect);
            Assert.Same(tree.Second, tree.Nested.ActiveControl);
            Assert.Equal(new[] { "first.Leave", "second.Enter" }, events);
            Assert.All(tree.Controls, control => Assert.False(control.IsHandleCreated));
        });

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void ContainerSelect_DirectedSelectionStartsAtTheRequestedEnd(bool directed, bool forward)
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            tree.Nested.ActiveControl = tree.Second;

            tree.Nested.SelectDirected(directed, forward);

            Assert.Same(tree.Nested, tree.Root.ActiveControl);
            Assert.Same(directed && forward ? tree.First : tree.Second, tree.Nested.ActiveControl);
            Assert.All(tree.Controls, control => Assert.False(control.IsHandleCreated));
        });

    [Fact]
    public void ContainerSelect_UndirectedEmptyContainerDoesNotChooseAChild()
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            List<string> events = Track(tree.Controls);

            tree.Nested.Select();

            Assert.Same(tree.Nested, tree.Root.ActiveControl);
            Assert.Null(tree.Nested.ActiveControl);
            Assert.Equal(new[] { "nested.Enter" }, events);
        });

    [Fact]
    public void ContainerSelect_DoesNotChooseAChildWhenItsParentRejectsActivation()
        => OnStaThread(() =>
        {
            using var parent = new RejectingContainer();
            var nested = new SelectableContainer();
            var child = new Control();
            parent.Controls.Add(nested);
            nested.Controls.Add(child);

            nested.SelectDirected(directed: true, forward: true);

            Assert.Same(nested, parent.RequestedControl);
            Assert.Null(parent.ActiveControl);
            Assert.Null(nested.ActiveControl);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void EnterReentrancy_PreservesTheNestedSelectionThatWasStillBeingEntered()
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            List<string> events = Track(tree.Controls);
            tree.First.Enter += (_, _) => tree.Root.ActiveControl = tree.Last;

            tree.Root.ActiveControl = tree.First;

            Assert.Equal(new[] { "nested.Enter", "first.Enter", "nested.Leave", "last.Enter" }, events);
            Assert.Same(tree.Last, tree.Root.ActiveControl);
            Assert.Same(tree.First, tree.Nested.ActiveControl);
        });

    [Fact]
    public void LeaveReentrancy_PreservesTheReferenceRepeatedTransitionNotifications()
        => OnStaThread(() =>
        {
            using var tree = new FocusTree();
            tree.Root.ActiveControl = tree.Last;
            List<string> events = Track(tree.Controls);
            bool redirected = false;
            tree.Last.Leave += (_, _) =>
            {
                if (redirected) { return; }
                redirected = true;
                tree.Root.ActiveControl = tree.Leaf;
            };

            tree.Root.ActiveControl = tree.First;

            Assert.Equal(new[]
            {
                "last.Leave", "last.Leave", "plain.Enter", "leaf.Enter", "plain.Enter", "leaf.Enter",
            }, events);
            Assert.Same(tree.Leaf, tree.Root.ActiveControl);
            Assert.Null(tree.Nested.ActiveControl);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void TransitionFailure_ReportsTheOriginalExceptionAndContinuesTheSelection(bool failOnLeave)
        => OnStaThread(() =>
        {
            using var root = new ContainerControl();
            var first = new Control { Name = "first" };
            var second = new Control { Name = "second" };
            root.Controls.AddRange(first, second);
            root.ActiveControl = first;
            List<string> events = Track([first, second]);
            var failure = new InvalidOperationException("focus-transition-failure");
            Exception? reported = null;
            ThreadExceptionEventHandler reporter = (_, args) =>
            {
                reported = args.Exception;
                events.Add("reported");
            };
            Application.ThreadException += reporter;
            try
            {
                if (failOnLeave) { first.Leave += (_, _) => throw failure; }
                else { second.Enter += (_, _) => throw failure; }

                root.ActiveControl = second;

                Assert.Same(failure, reported);
                Assert.Same(second, root.ActiveControl);
                Assert.Equal(failOnLeave
                    ? new[] { "first.Leave", "reported", "second.Enter" }
                    : new[] { "first.Leave", "second.Enter", "reported" }, events);
            }
            finally { Application.ThreadException -= reporter; }
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    [Trait("Category", "WindowsHandle")]
    public void NativeFocusAndSelect_PreserveTheirDifferentEventOrder(bool select)
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var first = NewControl("first", 0);
            var second = NewControl("second", 1);
            form.Controls.AddRange(first, second);
            form.Show();
            Assert.True(first.Focus());
            List<string> events = Track([first, second]);

            if (select) { second.Select(); }
            else { Assert.True(second.Focus()); }

            Assert.Equal(select
                ? new[] { "first.Leave", "second.Enter", "first.LostFocus", "second.GotFocus" }
                : new[] { "first.LostFocus", "first.Leave", "second.Enter", "second.GotFocus" }, events);
            Assert.Same(second, form.ActiveControl);
            Assert.Equal(second.Handle, GetFocus());
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NativeContainerFocus_RestoresItsActiveChildWithoutGotFocusOnTheContainer()
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var nested = new ContainerControl { Name = "nested", Bounds = new Rectangle(0, 0, 200, 100) };
            var child = NewControl("child", 0);
            nested.Controls.Add(child);
            form.Controls.Add(nested);
            form.Show();
            _ = SetFocus(0);
            List<string> events = Track([nested, child]);

            _ = SetFocus(nested.Handle);

            Assert.Equal(new[] { "nested.LostFocus", "child.GotFocus" }, events);
            Assert.Equal(child.Handle, GetFocus());
            events.Clear();

            _ = SetFocus(nested.Handle);

            Assert.Equal(new[] { "child.LostFocus", "nested.LostFocus", "child.GotFocus" }, events);
            Assert.Same(child, nested.ActiveControl);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NativeContainerFocus_HiddenActiveChildLeavesFocusOnTheContainer()
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var nested = new ContainerControl { Name = "nested", Bounds = new Rectangle(0, 0, 200, 100) };
            var child = NewControl("child", 0);
            nested.Controls.Add(child);
            form.Controls.Add(nested);
            form.Show();
            _ = SetFocus(0);
            child.Visible = false;
            List<string> events = Track([nested, child]);

            _ = SetFocus(nested.Handle);

            Assert.Equal(new[] { "nested.GotFocus" }, events);
            Assert.Equal(nested.Handle, GetFocus());
            Assert.Same(child, nested.ActiveControl);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void TextBox_UsesTheSharedLogicalTransitionsWithoutDuplicateEnterOrLeave()
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var text = new TextBox { Name = "text", Text = "focus", Bounds = new Rectangle(5, 5, 100, 25) };
            var other = NewControl("other", 1);
            form.Controls.AddRange(text, other);
            form.Show();
            Assert.True(text.Focus());
            List<string> events = Track([text, other]);

            Assert.True(other.Focus());

            Assert.Equal(new[] { "text.LostFocus", "text.Leave", "other.Enter", "other.GotFocus" }, events);
            events.Clear();

            text.Select();

            Assert.Equal(new[] { "other.Leave", "text.Enter", "other.LostFocus", "text.GotFocus" }, events);
            Assert.Same(text, form.ActiveControl);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FormShow_SelectsTheFirstTabChildAndFocusesItsNestedSelection()
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var skipped = NewControl("skipped", 0);
            skipped.TabStop = false;
            var nested = new ContainerControl { Name = "nested", TabIndex = 1, Bounds = new Rectangle(0, 40, 200, 100) };
            var first = NewControl("first", 0);
            var second = NewControl("second", 1);
            nested.Controls.AddRange(first, second);
            form.Controls.AddRange(skipped, nested);
            List<string> events = Track([nested, first, second]);

            form.Show();

            Assert.Same(nested, form.ActiveControl);
            Assert.Same(first, nested.ActiveControl);
            Assert.Equal(first.Handle, GetFocus());
            Assert.Equal(new[] { "nested.Enter", "first.Enter", "first.GotFocus" }, events);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FormSelect_ActivatesTheHiddenFormAndShowDoesNotSelectAgain()
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var child = NewControl("child", 0);
            form.Controls.Add(child);

            form.Select();

            Assert.True(form.IsHandleCreated);
            Assert.False(form.Visible);
            Assert.False(child.IsHandleCreated);
            Assert.Null(form.ActiveControl);
            Assert.Equal(form.Handle, GetFocus());

            form.Show();

            Assert.Null(form.ActiveControl);
            Assert.Equal(form.Handle, GetFocus());
            _ = SetFocus(0);
            form.SelectDirected(directed: true, forward: true);
            Assert.Same(child, form.ActiveControl);
            Assert.Equal(0, GetFocus());
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FormRecreateHandle_RestoresTheActivePathWithoutRepeatingEnter()
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var nested = new ContainerControl { Name = "nested", Bounds = new Rectangle(0, 0, 200, 100) };
            var child = NewControl("child", 0);
            nested.Controls.Add(child);
            form.Controls.Add(nested);
            form.Show();
            nint previous = form.Handle;
            int entered = 0;
            nested.Enter += (_, _) => entered++;
            child.Enter += (_, _) => entered++;

            form.Recreate();

            Assert.False(IsWindow(previous));
            Assert.True(form.IsHandleCreated);
            Assert.Same(nested, form.ActiveControl);
            Assert.Same(child, nested.ActiveControl);
            Assert.Equal(child.Handle, GetFocus());
            Assert.Equal(0, entered);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FormLazyHandleAfterExternalDestruction_RestoresVisibilityWithoutActivationOrChildren()
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var child = NewControl("child", 0);
            form.Controls.Add(child);
            form.Show();
            Assert.Same(child, form.ActiveControl);
            Assert.True(DestroyWindow(form.Handle));
            int childCreations = 0;
            child.HandleCreated += (_, _) => childCreations++;

            nint replacement = form.Handle;

            Assert.True(IsWindowVisible(replacement));
            Assert.False(((Control)form).Created);
            Assert.False(child.IsHandleCreated);
            Assert.False(child.Created);
            Assert.Equal(0, childCreations);
            Assert.Same(child, form.ActiveControl);
            Assert.NotEqual(replacement, GetActiveWindow());
            Assert.Equal(0, GetFocus());

            form.CreateControl();

            Assert.True(((Control)form).Created);
            Assert.True(child.Created);
            Assert.True(child.IsHandleCreated);
            Assert.Equal(1, childCreations);
            Assert.NotEqual(replacement, GetActiveWindow());
            Assert.Equal(0, GetFocus());

            form.Select();

            Assert.Equal(replacement, GetActiveWindow());
            Assert.Equal(child.Handle, GetFocus());
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FormCreateControlAfterExternalDestruction_CreatesChildrenAfterTheFormHandleReturns()
        => OnStaThread(() =>
        {
            using var form = NewForm();
            var child = NewControl("child", 0);
            form.Controls.Add(child);
            form.Show();
            Assert.True(DestroyWindow(form.Handle));
            List<bool> creationStates = [];
            child.HandleCreated += (_, _) => creationStates.Add(form.InsideCreateHandle);

            form.CreateControl();

            Assert.Equal(new[] { false }, creationStates);
            Assert.True(((Control)form).Created);
            Assert.True(child.Created);
            Assert.Same(child, form.ActiveControl);
            Assert.NotEqual(form.Handle, GetActiveWindow());
            Assert.Equal(0, GetFocus());
        });

    private static List<string> Track(IEnumerable<Control> controls)
    {
        List<string> events = [];
        foreach (Control control in controls)
        {
            control.Enter += (_, _) => events.Add(control.Name + ".Enter");
            control.Leave += (_, _) => events.Add(control.Name + ".Leave");
            control.GotFocus += (_, _) => events.Add(control.Name + ".GotFocus");
            control.LostFocus += (_, _) => events.Add(control.Name + ".LostFocus");
        }
        return events;
    }

    private static SelectableForm NewForm() => new() { Bounds = new Rectangle(20, 20, 320, 240) };
    private static Control NewControl(string name, int tabIndex)
        => new() { Name = name, TabIndex = tabIndex, Bounds = new Rectangle(5 + tabIndex * 105, 5, 90, 25) };

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The container focus check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class FocusTree : IDisposable
    {
        internal readonly SelectableContainer Root = new() { Name = "root" };
        internal readonly SelectableContainer Nested = new() { Name = "nested" };
        internal readonly Control First = new() { Name = "first", TabIndex = 0 };
        internal readonly Control Second = new() { Name = "second", TabIndex = 1 };
        internal readonly Control Plain = new() { Name = "plain" };
        internal readonly Control Leaf = new() { Name = "leaf" };
        internal readonly Control Last = new() { Name = "last" };
        internal Control[] Controls => [Root, Nested, First, Second, Plain, Leaf, Last];
        internal string State => $"{Root.ActiveControl?.Name ?? "null"}/{Nested.ActiveControl?.Name ?? "null"}";

        internal FocusTree()
        {
            Nested.Controls.AddRange(First, Second);
            Plain.Controls.Add(Leaf);
            Root.Controls.AddRange(Nested, Plain, Last);
        }

        public void Dispose() => Root.Dispose();
    }

    private sealed class SelectableContainer : ContainerControl
    {
        internal void SelectDirected(bool directed, bool forward) => Select(directed, forward);
    }

    private sealed class SelectableForm : Form
    {
        internal bool InsideCreateHandle { get; private set; }
        internal void SelectDirected(bool directed, bool forward) => Select(directed, forward);
        internal void Recreate() => RecreateHandle();

        protected override void CreateHandle()
        {
            InsideCreateHandle = true;
            try { base.CreateHandle(); }
            finally { InsideCreateHandle = false; }
        }
    }

    private sealed class RejectingContainer : Control, IContainerControl
    {
        internal RejectingContainer() => SetStyle(ControlStyles.ContainerControl, true);
        internal Control? RequestedControl { get; private set; }
        public Control? ActiveControl { get => null; set => RequestedControl = value; }
        public bool ActivateControl(Control active) => false;
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SetFocus(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetFocus();

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetActiveWindow();

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindowVisible(nint handle);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}
