using System.ComponentModel;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public class ControlFocusTests
{
    [Fact]
    public void FocusMembers_PreserveTheWinFormsSignaturesAndDesignerMetadata()
    {
        MethodInfo focus = typeof(Control).GetMethod(nameof(Control.Focus), Type.EmptyTypes)!;
        Assert.Equal(typeof(bool), focus.ReturnType);
        Assert.False(focus.IsVirtual);
        Assert.Equal(EditorBrowsableState.Advanced, focus.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
        CheckProperty(nameof(Control.Focused), isVirtual: true);
        CheckProperty(nameof(Control.ContainsFocus), isVirtual: false);
        CheckProperty(nameof(Control.CanFocus), isVirtual: false);

        static void CheckProperty(string name, bool isVirtual)
        {
            PropertyInfo property = typeof(Control).GetProperty(name)!;
            Assert.Equal(typeof(bool), property.PropertyType);
            Assert.Null(property.SetMethod);
            Assert.Equal(isVirtual, property.GetMethod!.IsVirtual);
            Assert.False(property.GetCustomAttribute<BrowsableAttribute>()!.Browsable);
            Assert.Equal(EditorBrowsableState.Advanced, property.GetCustomAttribute<EditorBrowsableAttribute>()!.State);
            Assert.Equal(DesignerSerializationVisibility.Hidden, property.GetCustomAttribute<DesignerSerializationVisibilityAttribute>()!.Visibility);
        }
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void UncreatedControl_FocusQueriesAndRequestsDoNotCreateAHandle(bool enabled, bool visible)
        => OnStaThread(() =>
        {
            using var control = new Control { Enabled = enabled, Visible = visible };
            AssertNoFocusOrHandle(control);
        });

    [Fact]
    public void DisposedUncreatedControl_FocusReturnsFalseWithoutThrowingOrCreatingAHandle()
        => OnStaThread(() =>
        {
            var control = new Control();
            control.Dispose();
            Assert.True(control.IsDisposed);
            AssertNoFocusOrHandle(control);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void OwnedControlWithoutAHandle_FocusDoesNotUseOrCreateItsOwnersHandle()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var child = new HandlelessControl { Visible = false };
            form.Controls.Add(child);
            AssertNoFocusOrHandle(child);
            form.Show();
            _ = SetFocus(form.Handle);
            AssertNoFocusOrHandle(child);
            Assert.Equal(form.Handle, GetFocus());
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void Focus_ReturnsTheCurrentNativeStateWhenFocusMovesBetweenControls()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var first = new Control();
            using var second = new Control();
            form.Controls.Add(first);
            form.Controls.Add(second);
            form.Show();
            _ = SetFocus(0);

            Assert.True(first.CanFocus);
            Assert.False(first.Focused);
            Assert.False(first.ContainsFocus);
            Assert.True(first.Focus());
            Assert.Equal(first.Handle, GetFocus());
            Assert.True(first.Focus());
            Assert.True(first.Focused);
            Assert.True(first.ContainsFocus);
            Assert.True(second.Focus());
            Assert.Equal(second.Handle, GetFocus());
            Assert.False(first.Focused);
            Assert.False(first.ContainsFocus);
            Assert.True(second.Focused);
            Assert.True(second.ContainsFocus);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void FocusedAndContainsFocus_ObserveFocusAssignedOutsideTheManagedApi()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new Control();
            form.Controls.Add(control);
            form.Show();

            _ = SetFocus(control.Handle);
            Assert.True(control.Focused);
            Assert.True(control.ContainsFocus);
            _ = SetFocus(0);
            Assert.False(control.Focused);
            Assert.False(control.ContainsFocus);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void ContainsFocus_RecognizesAnUnmanagedDescendantWithoutClaimingDirectFocus()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new Control();
            using var sibling = new Control();
            form.Controls.Add(control);
            form.Controls.Add(sibling);
            form.Show();
            nint nativeChild = CreateWindowExW(0, "STATIC", "focus descendant", 0x50000000, 0, 0, 20, 20, control.Handle, 0, 0, 0);
            Assert.NotEqual(0, nativeChild);
            try
            {
                _ = SetFocus(nativeChild);
                Assert.Equal(nativeChild, GetFocus());
                Assert.False(control.Focused);
                Assert.True(control.ContainsFocus);
                Assert.False(sibling.ContainsFocus);
                Assert.True(control.Focus());
                Assert.Equal(control.Handle, GetFocus());
                Assert.True(control.Focused);
            }
            finally { _ = DestroyWindow(nativeChild); }
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    [Trait("Category", "WindowsHandle")]
    public void DisabledOrHiddenControl_RejectsFocusWhilePreservingItsHandle(bool disable)
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new Control();
            form.Controls.Add(control);
            form.Show();
            _ = SetFocus(0);
            nint handle = control.Handle;

            if (disable) { control.Enabled = false; }
            else { control.Visible = false; }
            Assert.False(control.CanFocus);
            Assert.False(control.Focus());
            Assert.False(control.Focused);
            Assert.False(control.ContainsFocus);
            Assert.Equal(handle, control.Handle);
            Assert.Equal(0, GetFocus());

            if (disable) { control.Enabled = true; }
            else { control.Visible = true; }
            Assert.True(control.CanFocus);
            Assert.True(control.Focus());
            if (disable) { control.Enabled = false; }
            else { control.Visible = false; }
            Assert.False(control.Focused);
            Assert.False(control.ContainsFocus);
            Assert.False(control.Focus());
            Assert.Equal(handle, control.Handle);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    [Trait("Category", "WindowsHandle")]
    public void CanFocus_ReadsNativeVisibilityAndEnablementInsteadOfManagedFlags(bool disable)
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new Control();
            form.Controls.Add(control);
            form.Show();
            _ = SetFocus(0);

            if (disable) { _ = EnableWindow(control.Handle, false); }
            else { _ = ShowWindow(control.Handle, 0); }
            Assert.True(control.Enabled);
            Assert.True(control.Visible);
            Assert.False(control.CanFocus);
            Assert.False(control.Focus());

            if (disable) { _ = EnableWindow(control.Handle, true); }
            else { _ = ShowWindow(control.Handle, 5); }
            Assert.True(control.CanFocus);
            Assert.True(control.Focus());
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    [Trait("Category", "WindowsHandle")]
    public void NativeOwnerState_DistinguishesCanFocusFromAnActualFocusRequest(bool disable)
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var control = new Control();
            form.Controls.Add(control);
            form.Show();
            _ = SetFocus(0);

            if (disable) { _ = EnableWindow(form.Handle, false); }
            else { _ = ShowWindow(form.Handle, 0); }
            try
            {
                Assert.Equal(disable, control.CanFocus);
                Assert.False(control.Focus());
                Assert.False(control.Focused);
                Assert.False(control.ContainsFocus);
            }
            finally
            {
                if (disable) { _ = EnableWindow(form.Handle, true); }
                else { _ = ShowWindow(form.Handle, 5); }
            }
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void DisposingANativeControlOrItsOwner_ClearsTheFocusSurfaceAndDoesNotRecreateAHandle()
        => OnStaThread(() =>
        {
            using var form = new Form();
            using var first = new Control();
            using var second = new Control();
            form.Controls.Add(first);
            form.Controls.Add(second);
            form.Show();
            Assert.True(first.Focus());
            nint handle = first.Handle;
            first.Dispose();
            Assert.False(IsWindow(handle));
            AssertNoFocusOrHandle(first);
            form.Dispose();
            AssertNoFocusOrHandle(second);
        });

    private static void AssertNoFocusOrHandle(Control control)
    {
        Assert.False(control.IsHandleCreated);
        Assert.False(control.Focused);
        Assert.False(control.ContainsFocus);
        Assert.False(control.CanFocus);
        Assert.False(control.Focus());
        Assert.False(control.IsHandleCreated);
        Assert.Equal(0, control.Handle);
    }

    private static void OnStaThread(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            // Focus checks do not depend on automatic context installation.
            // Other checks cover that contract on their own STA threads.
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "The native focus check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class HandlelessControl : Control
    {
        protected override bool ShouldCreateNativeHandle => false;
    }

    [DllImport("user32.dll")]
    private static extern nint GetFocus();
    [DllImport("user32.dll")]
    private static extern nint SetFocus(nint handle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnableWindow(nint handle, [MarshalAs(UnmanagedType.Bool)] bool enabled);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow(nint handle, int command);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string text, uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint handle);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint handle);
}

[CollectionDefinition(ControlFocusCollection.Name, DisableParallelization = true)]
public sealed class ControlFocusCollection
{
    public const string Name = "Control native focus";
}
