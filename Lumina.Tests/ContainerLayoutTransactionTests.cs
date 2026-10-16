using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class ContainerLayoutTransactionTests
{
    [Theory]
    [InlineData("Scrollable")]
    [InlineData("Container")]
    [InlineData("Panel")]
    [InlineData("Form")]
    [InlineData("Flow")]
    [InlineData("Table")]
    [InlineData("Split")]
    [InlineData("Tab")]
    [InlineData("Tool")]
    [InlineData("Menu")]
    [InlineData("Status")]
    public void Containers_UseTheSameNestedLayoutTransactionThroughControl(string kind)
        => OnStaThread(() =>
        {
            using Control container = CreateContainer(kind);
            using var affected = new Control();
            var events = new List<LayoutEventArgs>();
            container.PerformLayout(container, "Settled");
            container.Layout += (_, e) => events.Add(e);

            container.SuspendLayout();
            container.SuspendLayout();
            container.PerformLayout(affected, "Requested");
            container.ResumeLayout();
            Assert.Empty(events);

            container.ResumeLayout();
            LayoutEventArgs args = Assert.Single(events);
            Assert.Same(affected, args.AffectedControl);
            Assert.Equal("Requested", args.AffectedProperty);
            container.ResumeLayout();
            Assert.Single(events);
        });

    [Theory]
    [InlineData("Scrollable")]
    [InlineData("Container")]
    [InlineData("Panel")]
    [InlineData("Form")]
    public void ContainerLayout_DoesNotWalkAnUnchangedChildTree(string kind)
        => OnStaThread(() =>
        {
            using Control container = CreateContainer(kind);
            var child = new Panel();
            var grandchild = new Control();
            child.Controls.Add(grandchild);
            container.Controls.Add(child);
            int childLayouts = 0;
            int grandchildLayouts = 0;
            child.Layout += (_, _) => childLayouts++;
            grandchild.Layout += (_, _) => grandchildLayouts++;

            container.PerformLayout();
            container.PerformLayout(container, "Explicit");

            Assert.Equal(0, childLayouts);
            Assert.Equal(0, grandchildLayouts);
            Assert.False(container.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
            Assert.False(grandchild.IsHandleCreated);
        });

    [Fact]
    public void FormLayout_IsOneStandardEventAcrossAllBaseViews()
        => OnStaThread(() =>
        {
            using var form = new Form();
            Control control = form;
            ScrollableControl scrollable = form;
            var records = new List<(object? Sender, LayoutEventArgs Args)>();
            LayoutEventHandler first = (sender, e) => records.Add((sender, e));
            LayoutEventHandler second = (sender, e) => records.Add((sender, e));
            LayoutEventHandler third = (sender, e) => records.Add((sender, e));
            form.Layout += first;
            control.Layout += second;
            scrollable.Layout += third;

            scrollable.SuspendLayout();
            form.PerformLayout(form, "Shared");
            Assert.Empty(records);
            control.ResumeLayout();

            Assert.Equal(3, records.Count);
            Assert.All(records, record => Assert.Same(form, record.Sender));
            Assert.Same(records[0].Args, records[1].Args);
            Assert.Same(records[0].Args, records[2].Args);
            Assert.Same(form, records[0].Args.AffectedControl);
            Assert.Equal("Shared", records[0].Args.AffectedProperty);

            control.Layout -= first;
            records.Clear();
            form.PerformLayout(form, "Removed");
            Assert.Equal(2, records.Count);
            form.Layout -= second;
            form.Layout -= third;

            Assert.Null(typeof(Form).GetEvent(nameof(Control.Layout),
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            Assert.Null(typeof(Form).GetMethod("OnLayout", BindingFlags.NonPublic | BindingFlags.Instance,
                binder: null, Type.EmptyTypes, modifiers: null));
        });

    [Fact]
    public void FlowLayout_WaitsForResumeAndRaisesTheEventBeforeArrangingChildren()
        => OnStaThread(() =>
        {
            using var panel = new FlowLayoutPanel { Size = new Size(150, 80), Padding = new Padding(4) };
            var child = new Control { Size = new Size(20, 10), Margin = Padding.Empty };
            panel.Controls.Add(child);
            panel.PerformLayout();
            var observed = new List<Point>();
            panel.Layout += (_, _) => observed.Add(child.Location);

            panel.SuspendLayout();
            child.Location = new Point(70, 40);
            panel.PerformLayout();
            Assert.Equal(new Point(70, 40), child.Location);
            Assert.Empty(observed);

            panel.ResumeLayout();
            Assert.Equal(new Point(70, 40), Assert.Single(observed));
            Assert.Equal(new Point(4, 4), child.Location);
        });

    [Fact]
    public void TableLayout_WaitsForResumeAndRaisesTheEventBeforeArrangingChildren()
        => OnStaThread(() =>
        {
            using var panel = new TableLayoutPanel { Size = new Size(200, 80), ColumnCount = 2, RowCount = 1 };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            var child = new Control { Size = new Size(20, 10), Margin = Padding.Empty };
            panel.Controls.Add(child, 1, 0);
            panel.PerformLayout();
            var observed = new List<Point>();
            panel.Layout += (_, _) => observed.Add(child.Location);

            panel.SuspendLayout();
            child.Location = new Point(7, 40);
            panel.PerformLayout();
            Assert.Equal(new Point(7, 40), child.Location);
            Assert.Empty(observed);

            panel.ResumeLayout();
            Assert.Equal(new Point(7, 40), Assert.Single(observed));
            Assert.Equal(new Point(100, 0), child.Location);
        });

    [Theory]
    [InlineData("Tool")]
    [InlineData("Menu")]
    [InlineData("Status")]
    public void StripHostCreation_IsDeferredByTheSharedTransaction(string kind)
        => OnStaThread(() =>
        {
            using var strip = (ToolStrip)CreateContainer(kind);
            strip.Items.Add(new ToolStripLabel { Text = "Ready" });
            int events = 0;
            strip.Layout += (_, _) => events++;
            strip.SuspendLayout();
            strip.PerformLayout();
            Assert.Empty(strip.Controls);
            Assert.Equal(0, events);

            strip.ResumeLayout();
            Assert.Single(strip.Controls);
            Assert.Equal(1, events);
        });

    [Theory]
    [InlineData("Tool")]
    [InlineData("Menu")]
    [InlineData("Status")]
    [InlineData("Tab")]
    [InlineData("Split")]
    public void DerivedContainerResize_RequestsOneOwnLayout(string kind)
        => OnStaThread(() =>
        {
            using Control container = CreateContainer(kind);
            int events = 0;
            container.Layout += (_, _) => events++;
            container.Size = new Size(310, 190);
            Assert.Equal(1, events);
        });

    [Fact]
    public void FormResize_DocksStripsInReverseChildOrderWithoutWalkingOtherChildren()
        => OnStaThread(() =>
        {
            using var form = new Form { ClientSize = new Size(320, 180) };
            var menu = new MenuStrip();
            var tool = new ToolStrip();
            var status = new StatusStrip();
            var child = new Panel();
            form.MainMenuStrip = menu;
            form.Controls.AddRange(menu, tool, status, child);
            form.Show();
            int childLayouts = 0;
            child.Layout += (_, _) => childLayouts++;

            form.ClientSize = new Size(420, 240);

            Assert.Equal(420, menu.Width);
            Assert.Equal(420, tool.Width);
            Assert.Equal(0, tool.Top);
            Assert.Equal(tool.Height, menu.Top);
            Assert.Equal(420, status.Width);
            Assert.Equal(240 - status.Height, status.Top);
            Assert.Equal(0, childLayouts);
        });

    [Fact]
    public void FormLayout_DocksNestedStripsAgainstTheirImmediateParent()
        => OnStaThread(() =>
        {
            using var form = new Form { ClientSize = new Size(320, 180) };
            var panel = new Panel { Size = new Size(140, 90) };
            var strip = new ToolStrip { Bounds = new Rectangle(7, 9, 100, 25) };
            panel.Controls.Add(strip);
            form.Controls.Add(panel);

            form.PerformLayout();
            strip.PerformLayout();

            Assert.Equal(new Rectangle(0, 0, 140, 25), strip.Bounds);
        });

    [Fact]
    public void SplitLayout_PreservesImmediateDistanceUpdatesWhileSizeLayoutWaits()
        => OnStaThread(() =>
        {
            using var split = new SplitContainer { Size = new Size(240, 160), SplitterDistance = 80 };
            split.PerformLayout();
            var records = new List<(string Kind, Rectangle First, Rectangle Second)>();
            LayoutEventArgs? splitArguments = null;
            split.Layout += (_, e) =>
            {
                splitArguments = e;
                records.Add(("split", split.Panel1.Bounds, split.Panel2.Bounds));
            };
            split.Panel1.Layout += (_, _) => records.Add(("first", split.Panel1.Bounds, split.Panel2.Bounds));
            split.Panel2.Layout += (_, _) => records.Add(("second", split.Panel1.Bounds, split.Panel2.Bounds));

            split.SuspendLayout();
            split.SplitterDistance = 90;
            Assert.Equal(90, split.Panel1.Width);
            Assert.Equal(94, split.Panel2.Left);
            Assert.Equal(new[] { "first", "second" }, records.Select(record => record.Kind));
            Assert.Equal(84, records[0].Second.Left);
            records.Clear();

            split.Size = new Size(260, 180);
            Assert.Empty(records);
            Assert.Equal(160, split.Panel1.Height);

            split.ResumeLayout();
            Assert.Equal(new[] { "first", "second", "split" }, records.Select(record => record.Kind));
            Assert.All(records, record => Assert.Equal(split.Panel1.Bounds, record.First));
            Assert.All(records, record => Assert.Equal(split.Panel2.Bounds, record.Second));
            Assert.NotNull(splitArguments);
            Assert.Same(split.Panel2, splitArguments.AffectedControl);
            Assert.Equal("Bounds", splitArguments.AffectedProperty);
            Assert.Equal(180, split.Panel1.Height);
            Assert.Equal(180, split.Panel2.Height);
        });

    [Fact]
    public void FlowLayout_PropagatesHandlerFailureAndCanRunAgain()
        => OnStaThread(() =>
        {
            using var panel = new FlowLayoutPanel { Size = new Size(150, 80) };
            var child = new Control { Size = new Size(20, 10), Margin = Padding.Empty };
            panel.Controls.Add(child);
            var failure = new InvalidOperationException("layout-handler");
            LayoutEventHandler handler = (_, _) => throw failure;
            panel.Layout += handler;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => panel.PerformLayout()));

            panel.Layout -= handler;
            panel.SuspendLayout();
            child.Location = new Point(70, 40);
            panel.ResumeLayout();
            Assert.Equal(Point.Empty, child.Location);
        });

    [Fact]
    public void ContainerConstruction_WithDefaultConstraintsCanLayoutBeforeItsFieldsAreAssigned()
        => OnStaThread(() =>
        {
            using var tool = new ConstrainedToolStrip();
            using var split = new ConstrainedSplitContainer();
            tool.PerformLayout();
            split.PerformLayout();
            Assert.Empty(tool.Items);
            Assert.Equal(2, split.Controls.Count);
        });

    private static Control CreateContainer(string kind) => kind switch
    {
        "Scrollable" => new ScrollableControl(),
        "Container" => new ContainerControl(),
        "Panel" => new Panel(),
        "Form" => new Form(),
        "Flow" => new FlowLayoutPanel(),
        "Table" => new TableLayoutPanel(),
        "Split" => new SplitContainer(),
        "Tab" => new TabControl(),
        "Tool" => new ToolStrip(),
        "Menu" => new MenuStrip(),
        "Status" => new StatusStrip(),
        _ => throw new ArgumentOutOfRangeException(nameof(kind)),
    };

    private sealed class ConstrainedToolStrip : ToolStrip
    {
        protected override Size DefaultMinimumSize => new(20, 10);

        public override Size MinimumSize
        {
            get => base.MinimumSize;
            set { base.MinimumSize = value; Size = value; }
        }
    }

    private sealed class ConstrainedSplitContainer : SplitContainer
    {
        protected override Size DefaultMinimumSize => new(80, 30);

        public override Size MinimumSize
        {
            get => base.MinimumSize;
            set { base.MinimumSize = value; Size = value; }
        }
    }

    private static void OnStaThread(Action action)
    {
        ExceptionDispatchInfo? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = ExceptionDispatchInfo.Capture(exception); }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "The container layout test did not finish.");
        failure?.Throw();
    }
}
