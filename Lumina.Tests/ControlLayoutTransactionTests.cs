using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlBoundsCollection.Name)]
public class ControlLayoutTransactionTests
{
    [Fact]
    public void LayoutEntrypoints_HaveStandardNonvirtualShape()
    {
        foreach ((string name, Type[] parameters) in new (string, Type[])[]
        {
            ("SuspendLayout", []), ("ResumeLayout", []),
            ("ResumeLayout", [typeof(bool)]), ("PerformLayout", []),
            ("PerformLayout", [typeof(Control), typeof(string)])
        })
        {
            MethodInfo method = typeof(Control).GetMethod(name, parameters)!;
            Assert.True(method.IsPublic);
            Assert.False(method.IsVirtual);
            Assert.Equal(typeof(void), method.ReturnType);
        }

        MethodInfo hook = typeof(Control).GetMethod("OnLayout", BindingFlags.Instance | BindingFlags.NonPublic,
            [typeof(LayoutEventArgs)])!;
        Assert.True(hook.IsFamily);
        Assert.True(hook.IsVirtual);
        Assert.False(hook.IsFinal);
        Assert.Equal("levent", Assert.Single(hook.GetParameters()).Name);
        Assert.Equal(typeof(LayoutEventHandler), typeof(Control).GetEvent("Layout")!.EventHandlerType);
        Assert.True(typeof(LayoutEventArgs).IsSealed);
    }

    [Fact]
    public void LayoutArguments_PreserveControlComponentAndPropertyIdentity()
        => OnSta(() =>
        {
            using var control = new Control();
            using var component = new Component();
            string property = new('P', 3);
            var controlArgs = new LayoutEventArgs(control, property);
            var componentArgs = new LayoutEventArgs(component, property);
            var nullArgs = new LayoutEventArgs((Control?)null, null);
            Assert.Same(control, controlArgs.AffectedControl);
            Assert.Same(control, controlArgs.AffectedComponent);
            Assert.Same(component, componentArgs.AffectedComponent);
            Assert.Null(componentArgs.AffectedControl);
            Assert.Same(property, controlArgs.AffectedProperty);
            Assert.Same(property, componentArgs.AffectedProperty);
            Assert.Null(nullArgs.AffectedControl);
            Assert.Null(nullArgs.AffectedComponent);
            Assert.Null(nullArgs.AffectedProperty);
        });

    [Fact]
    public void LayoutArguments_DoNotKeepAffectedComponentAlive()
    {
        LayoutEventArgs args = CreateWeakArguments();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        Assert.Null(args.AffectedComponent);
        Assert.Equal("Weak", args.AffectedProperty);
    }

    [Fact]
    public void ResumeWithoutPendingLayout_DoesNotRaiseLayout()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            control.ResumeLayout();
            control.ResumeLayout(true);
            control.ResumeLayout(false);
            control.SuspendLayout();
            control.ResumeLayout();
            Assert.Empty(control.Arguments);
        });

    [Fact]
    public void NestedSuspension_DefersUntilOutermostResumeAndKeepsFirstRequest()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            using var first = new Control();
            using var second = new Control();
            control.SuspendLayout();
            control.SuspendLayout();
            control.PerformLayout(first, "First");
            control.PerformLayout(second, "Second");
            control.ResumeLayout();
            Assert.Empty(control.Arguments);
            control.ResumeLayout();
            LayoutEventArgs args = Assert.Single(control.Arguments)!;
            Assert.Same(first, args.AffectedControl);
            Assert.Equal("First", args.AffectedProperty);
        });

    [Fact]
    public void IntermediateResume_AllowsNextSuspendedRequestToReplaceCachedArguments()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            control.SuspendLayout();
            control.SuspendLayout();
            control.PerformLayout(control, "First");
            control.ResumeLayout();
            control.PerformLayout(control, "Replacement");
            control.ResumeLayout();
            Assert.Equal("Replacement", Assert.Single(control.Arguments)!.AffectedProperty);
        });

    [Fact]
    public void ResumeFalse_LeavesCachedRequestForParameterlessPerformLayout()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            control.SuspendLayout();
            control.PerformLayout(control, "Deferred");
            control.ResumeLayout(false);
            control.ResumeLayout(true);
            Assert.Empty(control.Arguments);
            control.PerformLayout();
            Assert.Equal("Deferred", Assert.Single(control.Arguments)!.AffectedProperty);
            control.PerformLayout();
            Assert.Null(control.Arguments[1]!.AffectedProperty);
            Assert.Null(control.Arguments[1]!.AffectedControl);
        });

    [Fact]
    public void ExplicitPerformLayout_DoesNotConsumePreviouslyCachedArguments()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            control.SuspendLayout();
            control.PerformLayout(control, "Deferred");
            control.ResumeLayout(false);
            control.PerformLayout(control, "Explicit");
            control.PerformLayout();
            Assert.Equal(new[] { "Explicit", "Deferred" }, control.Arguments.Select(e => e!.AffectedProperty));
        });

    [Fact]
    public void ParameterlessPerformWhileSuspended_ConsumesCacheBeforeNextRequest()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            control.SuspendLayout();
            control.PerformLayout(control, "First");
            control.PerformLayout();
            control.PerformLayout(control, "Next");
            control.ResumeLayout();
            Assert.Equal("Next", Assert.Single(control.Arguments)!.AffectedProperty);
        });

    [Fact]
    public void LayoutReentry_IsDeferredAndCanBeConsumedByNextExplicitCall()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            bool reenter = true;
            control.Layout += (_, _) =>
            {
                if (!reenter) { return; }
                reenter = false;
                control.PerformLayout(control, "Reentrant");
            };
            control.PerformLayout(control, "Outer");
            Assert.Single(control.Arguments);
            control.ResumeLayout();
            Assert.Single(control.Arguments);
            control.PerformLayout();
            Assert.Equal(new[] { "Outer", "Reentrant" }, control.Arguments.Select(e => e!.AffectedProperty));
        });

    [Fact]
    public void SuspensionInsideLayout_DoesNotLeaveTheControlSuspended()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            LayoutEventHandler handler = (_, _) => control.SuspendLayout();
            control.Layout += handler;
            control.PerformLayout();
            control.Layout -= handler;
            control.PerformLayout();
            Assert.Equal(2, control.Arguments.Count);
        });

    [Fact]
    public void LayoutException_PropagatesOriginalAndResetsSuspension()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            var expected = new InvalidOperationException("layout callback");
            LayoutEventHandler handler = (_, _) => throw expected;
            control.Layout += handler;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.PerformLayout(control, "Throw")));
            control.Layout -= handler;
            control.PerformLayout(control, "After");
            Assert.Equal(new[] { "Throw", "After" }, control.Arguments.Select(e => e!.AffectedProperty));
        });

    [Fact]
    public void DeferredLayoutException_PreservesTheSameCachedArgumentsForRetry()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            var expected = new InvalidOperationException("deferred callback");
            LayoutEventHandler handler = (_, _) => throw expected;
            control.SuspendLayout();
            control.PerformLayout(control, "Deferred");
            control.Layout += handler;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.ResumeLayout()));
            control.Layout -= handler;
            control.PerformLayout();
            Assert.Equal(2, control.Arguments.Count);
            Assert.Same(control.Arguments[0], control.Arguments[1]);
        });

    [Fact]
    public void DirectOnLayout_ForwardsArgumentsEvenWhileSuspended()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            var args = new LayoutEventArgs(control, "Direct");
            LayoutEventArgs? observed = null;
            control.Layout += (_, e) => observed = e;
            control.SuspendLayout();
            control.RaiseLayout(args);
            Assert.Same(args, observed);
            control.ResumeLayout();
            Assert.Single(control.Arguments);
        });

    [Fact]
    public void DirectOnLayout_NullArgumentsAreForwardedWithoutValidation()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            int events = 0;
            control.Layout += (sender, args) =>
            {
                Assert.Same(control, sender);
                Assert.Null(args);
                events++;
            };
            control.RaiseLayout(null!);
            Assert.Equal(1, events);
            Assert.Null(Assert.Single(control.Arguments));
        });

    [Fact]
    public void OnLayoutOverride_CanSuppressBaseEventAndReceivesSameArguments()
        => OnSta(() =>
        {
            using var control = new TraceControl { SkipBase = true };
            int events = 0;
            control.Layout += (_, _) => events++;
            control.PerformLayout(control, "Suppressed");
            Assert.Equal(0, events);
            Assert.Equal("Suppressed", Assert.Single(control.Arguments)!.AffectedProperty);
        });

    [Fact]
    public void PerformLayout_DoesNotRecursivelyRaiseChildLayout()
        => OnSta(() =>
        {
            using var parent = new TraceControl();
            using var child = new TraceControl();
            parent.Controls.Add(child);
            parent.Arguments.Clear();
            child.Arguments.Clear();
            parent.PerformLayout();
            Assert.Single(parent.Arguments);
            Assert.Empty(child.Arguments);
            Assert.False(parent.IsHandleCreated);
            Assert.False(child.IsHandleCreated);
        });

    [Fact]
    public void AddRange_ReportsTheLastChildAfterItsNestedResumeBoundary()
        => OnSta(() =>
        {
            using var parent = new TraceControl();
            using var first = new Control();
            using var second = new Control();
            parent.Controls.AddRange([first, second]);
            LayoutEventArgs args = Assert.Single(parent.Arguments)!;
            Assert.Same(second, args.AffectedControl);
            Assert.Equal("Parent", args.AffectedProperty);
        });

    [Fact]
    public void CollectionChanges_UseParentAndChildIndexReasons()
        => OnSta(() =>
        {
            using var parent = new TraceControl();
            using var first = new Control();
            using var second = new Control();
            parent.Controls.Add(first);
            parent.Controls.Add(second);
            parent.Controls.SetChildIndex(second, 0);
            parent.Controls.Remove(first);
            Assert.Equal(new[] { "Parent", "Parent", "ChildIndex", "Parent" },
                parent.Arguments.Select(e => e!.AffectedProperty));
            Assert.Same(first, parent.Arguments[0]!.AffectedControl);
            Assert.Same(second, parent.Arguments[2]!.AffectedControl);
            Assert.Same(first, parent.Arguments[3]!.AffectedControl);
        });

    [Fact]
    public void Resize_NotifiesOwnLayoutBeforeResizeAndParentAfterSizeNotifications()
        => OnSta(() =>
        {
            using var parent = new Control();
            using var child = new Control();
            parent.Controls.Add(child);
            List<string> trace = [];
            child.Layout += (_, e) => trace.Add("child:" + e.AffectedProperty);
            child.Resize += (_, _) => trace.Add("resize");
            child.SizeChanged += (_, _) => trace.Add("size");
            child.ClientSizeChanged += (_, _) => trace.Add("client");
            parent.Layout += (_, e) => trace.Add("parent:" + e.AffectedProperty);
            child.Size = new Size(20, 30);
            Assert.Equal(new[] { "child:Bounds", "resize", "size", "client", "parent:Bounds", "parent:Bounds" }, trace);
        });

    [Fact]
    public void PropertyNotifications_PreserveTheirLayoutTransactionOrder()
        => OnSta(() =>
        {
            using var parent = new Control();
            using var child = new Control();
            parent.Controls.Add(child);
            List<string> trace = [];
            parent.Layout += (_, e) => trace.Add("parent:" + e.AffectedProperty);
            child.Layout += (_, e) => trace.Add("child:" + e.AffectedProperty);
            child.PaddingChanged += (_, _) => trace.Add("padding");
            child.MarginChanged += (_, _) => trace.Add("margin");
            child.AutoSizeChanged += (_, _) => trace.Add("autosize");
            child.VisibleChanged += (_, _) => trace.Add("visible");
            child.Padding = new Padding(4);
            child.Margin = new Padding(5);
            child.AutoSize = true;
            child.Visible = false;
            Assert.Equal(new[]
            {
                "padding", "parent:Padding", "child:Padding", "parent:Margin", "margin",
                "parent:AutoSize", "autosize", "visible", "parent:Visible"
            }, trace);
        });

    [Fact]
    public void SuspendedPropertyChanges_KeepNotificationsImmediateAndLayoutsDeferred()
        => OnSta(() =>
        {
            using var parent = new Control();
            using var child = new Control();
            parent.Controls.Add(child);
            List<string> trace = [];
            parent.Layout += (_, e) => trace.Add("parent:" + e.AffectedProperty);
            child.Layout += (_, e) => trace.Add("child:" + e.AffectedProperty);
            child.PaddingChanged += (_, _) => trace.Add("padding");
            child.MarginChanged += (_, _) => trace.Add("margin");
            parent.SuspendLayout();
            child.SuspendLayout();
            child.Padding = new Padding(4);
            child.Margin = new Padding(5);
            Assert.Equal(new[] { "padding", "margin" }, trace);
            parent.ResumeLayout();
            child.ResumeLayout();
            Assert.Equal(new[] { "padding", "margin", "parent:Margin", "child:Padding" }, trace);
        });

    [Fact]
    public void PaddingChangedException_StillResumesParentAndPreservesOriginalFailure()
        => OnSta(() =>
        {
            using var parent = new TraceControl();
            using var child = new Control();
            parent.Controls.Add(child);
            parent.Arguments.Clear();
            var expected = new InvalidOperationException("padding callback");
            child.PaddingChanged += (_, _) => throw expected;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => child.Padding = new Padding(4)));
            Assert.Equal("Padding", Assert.Single(parent.Arguments)!.AffectedProperty);
            parent.PerformLayout(parent, "Recovered");
            Assert.Equal("Recovered", parent.Arguments[1]!.AffectedProperty);
        });

    [Fact]
    public void LayoutIsSuppressedDuringDisposalButRemainsCallableAfterwards()
        => OnSta(() =>
        {
            using var control = new TraceControl();
            control.DisposingCallback = () => control.PerformLayout(control, "During");
            control.Dispose();
            Assert.Empty(control.Arguments);
            control.PerformLayout(control, "After");
            Assert.Equal("After", Assert.Single(control.Arguments)!.AffectedProperty);
        });

    [Fact]
    public void InitLayoutAndCollectionEvents_EncloseParentLayoutInStandardOrder()
        => OnSta(() =>
        {
            using var parent = new Control();
            using var child = new TraceControl();
            List<string> trace = [];
            child.InitCallback = () => trace.Add("init");
            child.ParentChanged += (_, _) => trace.Add("parent-changed");
            parent.Layout += (_, e) => trace.Add("layout:" + e.AffectedProperty);
            parent.ControlAdded += (_, _) => trace.Add("added");
            parent.ControlRemoved += (_, _) => trace.Add("removed");
            parent.Controls.Add(child);
            parent.Controls.Remove(child);
            Assert.Equal(new[] { "parent-changed", "init", "layout:Parent", "added",
                "parent-changed", "layout:Parent", "removed" }, trace);
        });

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static LayoutEventArgs CreateWeakArguments() => new(new Component(), "Weak");

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The layout transaction check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class TraceControl : Control
    {
        public List<LayoutEventArgs?> Arguments { get; } = [];
        public bool SkipBase { get; set; }
        public Action? DisposingCallback { get; set; }
        public Action? InitCallback { get; set; }
        public void RaiseLayout(LayoutEventArgs args) => OnLayout(args);

        protected override void OnLayout(LayoutEventArgs levent)
        {
            Arguments?.Add(levent);
            if (!SkipBase) { base.OnLayout(levent); }
        }

        protected override void InitLayout()
        {
            InitCallback?.Invoke();
            base.InitLayout();
        }

        protected override void OnDisposing()
        {
            DisposingCallback?.Invoke();
            base.OnDisposing();
        }
    }
}
