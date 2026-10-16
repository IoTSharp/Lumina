using System.Drawing;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Lumina.Forms.Layout;
using Xunit;

namespace Lumina.Tests;

public sealed class ControlDefaultLayoutTests
{
    [Fact]
    public void DefaultEngine_IsSharedAndBaseEngineValidatesItsControlArguments()
        => OnSta(() =>
        {
            using var first = new Control();
            using var second = new Control();
            Assert.Same(first.LayoutEngine, second.LayoutEngine);
            var engine = new EmptyEngine();
            Assert.Equal("child", Assert.Throws<ArgumentNullException>(() => engine.InitLayout(null!, BoundsSpecified.All)).ParamName);
            Assert.Equal("container", Assert.Throws<ArgumentNullException>(() => engine.Layout(null!, null!)).ParamName);
            Assert.Throws<NotSupportedException>(() => engine.InitLayout(new object(), BoundsSpecified.All));
            Assert.Throws<NotSupportedException>(() => engine.Layout(new object(), null!));
            engine.InitLayout(first, (BoundsSpecified)int.MaxValue);
            Assert.False(engine.Layout(first, null!));
        });

    [Fact]
    public void ControlDisplayRectangle_UsesClientAreaWithoutApplyingPadding()
        => OnSta(() =>
        {
            using var control = new Control { Size = new Size(200, 130), Padding = new Padding(9, 7, 5, 3) };
            Assert.Equal(control.ClientRectangle, control.DisplayRectangle);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void DockLayout_UsesReverseChildOrderAndIgnoresMargin()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(200, 120) };
            using var fill = new Control { Size = new Size(7, 8), Dock = DockStyle.Fill, Margin = new Padding(20) };
            using var left = new Control { Width = 30, Dock = DockStyle.Left };
            using var top = new Control { Height = 20, Dock = DockStyle.Top };
            parent.Controls.AddRange([fill, left, top]);
            Assert.Equal(new Rectangle(0, 0, 200, 20), top.Bounds);
            Assert.Equal(new Rectangle(0, 20, 30, 100), left.Bounds);
            Assert.Equal(new Rectangle(30, 20, 170, 100), fill.Bounds);
            parent.Size = new Size(240, 160);
            Assert.Equal(new Rectangle(30, 20, 210, 140), fill.Bounds);
            Assert.False(fill.IsHandleCreated);
        });

    [Fact]
    public void DockNone_RestoresSpecifiedBoundsAndPreferredSize()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(200, 120) };
            using var child = new Control { Bounds = new Rectangle(13, 17, 31, 29) };
            parent.Controls.Add(child);
            child.Dock = DockStyle.Fill;
            Assert.Equal(new Rectangle(0, 0, 200, 120), child.Bounds);
            Assert.Equal(new Size(31, 29), child.PreferredSize);
            child.Dock = DockStyle.None;
            Assert.Equal(new Rectangle(13, 17, 31, 29), child.Bounds);
        });

    [Fact]
    public void SettingAnchorAfterDock_RestoresBoundsWithoutDockChanged()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(200, 120) };
            using var child = new Control { Bounds = new Rectangle(13, 17, 31, 29) };
            parent.Controls.Add(child);
            child.Dock = DockStyle.Fill;
            int changed = 0;
            child.DockChanged += (_, _) => changed++;
            child.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            Assert.Equal(0, changed);
            Assert.Equal(DockStyle.None, child.Dock);
            Assert.Equal(new Rectangle(13, 17, 31, 29), child.Bounds);
            parent.Size = new Size(220, 150);
            Assert.Equal(new Rectangle(33, 47, 31, 29), child.Bounds);
        });

    [Fact]
    public void Anchor_StretchesOppositeEdgesAndRefreshesAfterExplicitBounds()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(200, 120) };
            using var child = new Control { Bounds = new Rectangle(10, 20, 30, 40), Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom };
            parent.Controls.Add(child);
            parent.Size = new Size(250, 170);
            Assert.Equal(new Rectangle(10, 20, 80, 90), child.Bounds);
            child.Bounds = new Rectangle(15, 25, 70, 80);
            parent.Size = new Size(270, 190);
            Assert.Equal(new Rectangle(15, 25, 90, 100), child.Bounds);
        });

    [Fact]
    public void AutoSize_GrowsTowardAnchoredRightAndBottomEdges()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(200, 120) };
            using var child = new PreferredControl
            {
                Bounds = new Rectangle(100, 70, 20, 10),
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Desired = new Size(60, 30)
            };
            parent.Controls.Add(child);
            child.AutoSize = true;
            Assert.Equal(new Rectangle(60, 50, 60, 30), child.Bounds);
            child.Desired = new Size(10, 5);
            parent.PerformLayout();
            Assert.Equal(new Rectangle(100, 70, 20, 10), child.Bounds);
            child.ChangeAutoSizeMode(AutoSizeMode.GrowAndShrink);
            parent.PerformLayout();
            Assert.Equal(new Rectangle(110, 75, 10, 5), child.Bounds);
        });

    [Fact]
    public void DockAutoSize_PassesTheAvailableAxisToPreferredSize()
        => OnSta(() =>
        {
            using var parent = new Control { Size = new Size(200, 120) };
            using var child = new PreferredControl { Desired = new Size(45, 23), AutoSize = true, Dock = DockStyle.Top };
            parent.Controls.Add(child);
            child.Proposals.Clear();
            parent.PerformLayout();
            Assert.Contains(new Size(200, 1), child.Proposals);
            Assert.Equal(new Rectangle(0, 0, 200, 23), child.Bounds);
        });

    [Fact]
    public void PreferredSize_UsesSpecifiedSizeAndConstraintsWithoutCreatingHandle()
        => OnSta(() =>
        {
            using var control = new CoreControl { Size = new Size(30, 40) };
            control.ApplyCore(new Rectangle(0, 0, 90, 100), BoundsSpecified.None);
            Assert.Equal(new Size(30, 40), control.GetPreferredSize(new Size(1, 1)));
            Assert.Equal(new Size(30, 40), control.PreferredSize);
            control.MinimumSize = new Size(35, 45);
            Assert.Equal(new Size(35, 45), control.PreferredSize);
            Assert.False(control.IsHandleCreated);
            control.Dispose();
            Assert.Equal(Size.Empty, control.GetPreferredSize(Size.Empty));
        });

    [Fact]
    public void PreferredSizeProperty_DispatchesEmptyProposalThroughPublicOverride()
        => OnSta(() =>
        {
            using var control = new PreferredControl { Desired = new Size(12, 34) };
            Assert.Equal(control.Desired, control.PreferredSize);
            Assert.Equal(Size.Empty, Assert.Single(control.Proposals));
        });

    [Fact]
    public void LayoutEvent_PrecedesCustomEngineAndTrueRequestsParentLayout()
        => OnSta(() =>
        {
            using var parent = new Control();
            using var child = new EngineControl();
            parent.Controls.Add(child);
            List<string> trace = [];
            child.Layout += (_, _) => trace.Add("event");
            child.Engine.Arrange = (_, _) => { trace.Add("engine"); return true; };
            parent.Layout += (_, e) => trace.Add("parent:" + e.AffectedProperty);
            child.PerformLayout();
            Assert.Equal(new[] { "event", "engine", "parent:PreferredSize" }, trace);
        });

    [Fact]
    public void CustomEngineException_IsOriginalAndLayoutCanRunAgain()
        => OnSta(() =>
        {
            using var control = new EngineControl();
            var expected = new InvalidOperationException("engine failure");
            control.Engine.Arrange = (_, _) => throw expected;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.PerformLayout()));
            int calls = 0;
            control.Engine.Arrange = (_, _) => { calls++; return false; };
            control.PerformLayout();
            Assert.Equal(1, calls);
        });

    [Fact]
    public void ResumeFalse_InitializesEveryChildEvenWithoutSuspension()
        => OnSta(() =>
        {
            using var parent = new EngineControl();
            using var first = new Control();
            using var second = new Control();
            parent.Controls.AddRange([first, second]);
            parent.Engine.Initializations.Clear();
            parent.ResumeLayout(false);
            Assert.Equal(new[] { (first, BoundsSpecified.All), (second, BoundsSpecified.All) }, parent.Engine.Initializations);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The default layout check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class EmptyEngine : LayoutEngine { }

    private sealed class CoreControl : Control
    {
        internal void ApplyCore(Rectangle bounds, BoundsSpecified specified)
            => SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
    }

    private sealed class PreferredControl : Control
    {
        internal Size Desired { get; set; }
        internal List<Size> Proposals { get; } = [];
        public override Size GetPreferredSize(Size proposedSize)
        {
            Proposals.Add(proposedSize);
            return Desired;
        }
        internal void ChangeAutoSizeMode(AutoSizeMode mode) => SetAutoSizeMode(mode);
    }

    private sealed class EngineControl : Control
    {
        private TraceEngine? _engine;
        internal TraceEngine Engine => _engine ??= new();
        public override LayoutEngine LayoutEngine => Engine;
    }

    private sealed class TraceEngine : LayoutEngine
    {
        internal List<(Control, BoundsSpecified)> Initializations { get; } = [];
        internal Func<Control, LayoutEventArgs, bool>? Arrange { get; set; }
        public override void InitLayout(object child, BoundsSpecified specified)
        {
            Initializations.Add(((Control)child, specified));
            base.InitLayout(child, specified);
        }
        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            => Arrange?.Invoke((Control)container, layoutEventArgs) ?? base.Layout(container, layoutEventArgs);
    }
}
