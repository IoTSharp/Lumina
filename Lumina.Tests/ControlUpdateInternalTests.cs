using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlUpdateInternalTests
{
    private const int WmSetRedraw = 0x000B;

    [Fact]
    public void UpdateHelpersRemainInternalNonvirtualInstanceMembers()
    {
        AssertSignature("BeginUpdateInternal", Type.EmptyTypes, typeof(void));
        AssertSignature("EndUpdateInternal", Type.EmptyTypes, typeof(bool));
        AssertSignature("EndUpdateInternal", [typeof(bool)], typeof(bool));

        static void AssertSignature(string name, Type[] parameters, Type returnType)
        {
            MethodInfo method = typeof(Control).GetMethod(name,
                BindingFlags.Instance | BindingFlags.NonPublic, parameters)!;
            Assert.NotNull(method);
            Assert.True(method.IsAssembly);
            Assert.False(method.IsVirtual);
            Assert.False(method.IsStatic);
            Assert.Equal(returnType, method.ReturnType);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void BeginWithoutHandleDoesNotCreateOrAccumulateAnUpdate(bool invalidate)
        => OnSta(() =>
        {
            using var control = new MessageControl();
            Begin(control);
            Begin(control);

            Assert.False(control.IsHandleCreated);
            Assert.False(End(control, invalidate));
            Assert.False(End(control));
            Assert.Empty(control.RedrawMessages);

            _ = control.Handle;
            Assert.False(End(control, invalidate));
            Assert.Empty(control.RedrawMessages);
        });

    [Fact]
    public void UnmatchedEndWithHandleDoesNotSendMessagesOrInvalidate()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new MessageControl();
            CreateHosted(form, control);
            int invalidations = 0;
            control.Invalidated += (_, _) => invalidations++;

            Assert.False(End(control));
            Assert.False(End(control, false));
            Assert.False(End(control, true));

            Assert.Empty(control.RedrawMessages);
            Assert.Equal(0, invalidations);
        });

    [Fact]
    public void NestedUpdatesSendOnlyBoundaryMessagesAndCommitCountBeforeEnable()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new MessageControl { TryEndDuringRedraw = true };
            CreateHosted(form, control);
            int invalidations = 0;
            control.Invalidated += (_, _) => invalidations++;

            Begin(control);
            Begin(control);
            Begin(control);
            Assert.Equal(new[] { ((nint)0, (nint)0) }, control.RedrawMessages);
            Assert.Equal(new[] { false }, control.ReentrantEndResults);

            Assert.True(End(control));
            Assert.True(End(control, true));
            Assert.Single(control.RedrawMessages);
            Assert.Equal(0, invalidations);

            Assert.True(End(control));
            Assert.Equal(new[] { ((nint)0, (nint)0), ((nint)1, (nint)0) }, control.RedrawMessages);
            Assert.Equal(new[] { false, false }, control.ReentrantEndResults);
            Assert.Equal(1, invalidations);
            Assert.False(End(control));
        });

    [Fact]
    public void FinalEndWithoutInvalidationEnablesRedrawAndLeavesInvalidatedEventUntouched()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new MessageControl();
            CreateHosted(form, control);
            int invalidations = 0;
            control.Invalidated += (_, _) => invalidations++;

            Begin(control);
            Assert.True(End(control, false));

            Assert.Equal(new[] { ((nint)0, (nint)0), ((nint)1, (nint)0) }, control.RedrawMessages);
            Assert.Equal(0, invalidations);
            Assert.False(End(control));
        });

    [Fact]
    public void DefaultEndInvalidatesSelfAfterEnablingRedrawWithoutChildNotifications()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new MessageControl();
            using var child = new MessageControl { Bounds = new Rectangle(2, 3, 15, 10) };
            control.Controls.Add(child);
            CreateHosted(form, control);
            _ = child.Handle;
            var invalidatedAreas = new List<Rectangle>();
            int childInvalidations = 0;
            control.Invalidated += (_, args) =>
            {
                Assert.Equal((nint)1, control.RedrawMessages[^1].WParam);
                Assert.False(End(control, false));
                invalidatedAreas.Add(args.InvalidRect);
            };
            child.Invalidated += (_, _) => childInvalidations++;

            Begin(control);
            Assert.True(End(control));

            Assert.Equal(new[] { control.ClientRectangle }, invalidatedAreas);
            Assert.Equal(0, childInvalidations);
            Assert.Empty(child.RedrawMessages);
        });

    [Fact]
    public void InvalidatedExceptionIsPreservedAfterRedrawAndCountHaveBeenRestored()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new MessageControl();
            CreateHosted(form, control);
            var failure = new InvalidOperationException("update invalidation");
            InvalidateEventHandler handler = (_, _) => throw failure;
            control.Invalidated += handler;

            Begin(control);
            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => End(control)));
            Assert.Equal(new[] { ((nint)0, (nint)0), ((nint)1, (nint)0) }, control.RedrawMessages);
            Assert.False(End(control));

            control.Invalidated -= handler;
            Begin(control);
            Assert.True(End(control, false));
            Assert.Equal(4, control.RedrawMessages.Count);
        });

    [Fact]
    public void FinalEndAfterHandleDestructionUsesHandleGetterToCreateAReplacement()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new MessageControl();
            CreateHosted(form, control);
            int created = 0;
            control.HandleCreated += (_, _) => created++;

            Begin(control);
            control.DestroyNativeHandle();
            Assert.False(control.IsHandleCreated);
            Assert.True(End(control, false));

            Assert.True(control.IsHandleCreated);
            Assert.Equal(1, created);
            Assert.Equal(new[] { ((nint)0, (nint)0), ((nint)1, (nint)0) }, control.RedrawMessages);
            Assert.False(End(control));
        });

    [Fact]
    public void ReplacementCreationExceptionIsPreservedAfterFinalCountDecrement()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new MessageControl();
            CreateHosted(form, control);
            Begin(control);
            control.DestroyNativeHandle();
            var failure = new InvalidOperationException("update replacement handle");
            control.CreationFailure = failure;

            Assert.Same(failure, Assert.Throws<InvalidOperationException>(() => End(control, false)));
            Assert.False(control.IsHandleCreated);
            Assert.Equal(new[] { ((nint)0, (nint)0) }, control.RedrawMessages);
            Assert.False(End(control));
            Assert.False(control.IsHandleCreated);

            control.CreationFailure = null;
        });

    private static void CreateHosted(Form form, Control control)
    {
        form.Controls.Add(control);
        _ = form.Handle;
        _ = control.Handle;
        Assert.Same(form, control.Parent);
    }

    private static void Begin(Control control)
        => Invoke(control, "BeginUpdateInternal", Type.EmptyTypes, null);

    private static bool End(Control control)
        => (bool)Invoke(control, "EndUpdateInternal", Type.EmptyTypes, null)!;

    private static bool End(Control control, bool invalidate)
        => (bool)Invoke(control, "EndUpdateInternal", [typeof(bool)], [invalidate])!;

    private static object? Invoke(Control control, string name, Type[] parameters, object?[]? arguments)
    {
        MethodInfo method = typeof(Control).GetMethod(name,
            BindingFlags.Instance | BindingFlags.NonPublic, parameters)!;
        try { return method.Invoke(control, arguments); }
        catch (TargetInvocationException exception) when (exception.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            throw;
        }
    }

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "Control update test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class MessageControl : Control
    {
        internal List<(nint WParam, nint LParam)> RedrawMessages { get; } = new();
        internal List<bool> ReentrantEndResults { get; } = new();
        internal bool TryEndDuringRedraw { get; set; }
        internal Exception? CreationFailure { get; set; }

        internal MessageControl() { Size = new Size(80, 40); }

        internal void DestroyNativeHandle() => DestroyHandle();

        protected override void CreateHandle()
        {
            if (CreationFailure is not null) { throw CreationFailure; }
            base.CreateHandle();
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == WmSetRedraw)
            {
                RedrawMessages.Add((message.WParam, message.LParam));
                if (TryEndDuringRedraw) { ReentrantEndResults.Add(End(this, false)); }
                // Observe the managed boundary without changing native visibility or redraw state.
                message.Result = (nint)123;
                return;
            }
            base.WndProc(ref message);
        }
    }
}
