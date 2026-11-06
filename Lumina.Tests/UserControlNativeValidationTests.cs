using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class UserControlNativeValidationTests
{
    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NativeFocusSelectsFirstChildThroughNestedUserControls()
        => OnSta(() =>
        {
            using var form = new Form();
            using var outer = new UserControl { Parent = form };
            using var inner = new UserControl { Parent = outer };
            using var first = new Control { Parent = inner, TabIndex = 0 };
            using var second = new Control { Parent = inner, TabIndex = 1 };
            form.Show();
            // Clear the selections made by initial form activation so this
            // exercises UserControl's native focus entry, not Form.Select.
            inner.ActiveControl = null;
            outer.ActiveControl = null;
            form.ActiveControl = null;

            _ = SetFocus(outer.Handle);

            Assert.Same(outer, form.ActiveControl);
            Assert.Same(inner, outer.ActiveControl);
            Assert.Same(first, inner.ActiveControl);
            Assert.True(first.Focused);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NativeFocusRestoresExistingActiveChildInsteadOfSelectingFirst()
        => OnSta(() =>
        {
            using var form = new Form();
            using var composite = new UserControl { Parent = form };
            using var first = new Control { Parent = composite, TabIndex = 0 };
            using var second = new Control { Parent = composite, TabIndex = 1 };
            form.Show();
            composite.ActiveControl = second;
            Assert.True(second.Focused);

            _ = SetFocus(composite.Handle);

            Assert.Same(composite, form.ActiveControl);
            Assert.Same(second, composite.ActiveControl);
            Assert.True(second.Focused);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void MouseDownFocusesChildBeforeRaisingEventAndPreservesFocusAlreadyInside()
        => OnSta(() =>
        {
            using var form = new Form();
            using var composite = new ExposedUserControl { Parent = form };
            using var first = new Control { Parent = composite, TabIndex = 0 };
            using var second = new Control { Parent = composite, TabIndex = 1 };
            using var outside = new Control { Parent = form, TabIndex = 1 };
            form.Show();
            Assert.True(outside.Focus());
            // Interface activation updates the logical path without moving
            // native focus; MouseDown must perform the focus transfer itself.
            Assert.True(((IContainerControl)composite).ActivateControl(second));
            Assert.True(outside.Focused);
            int mouseDowns = 0;
            composite.MouseDown += (_, _) =>
            {
                mouseDowns++;
                Assert.True(second.Focused);
            };

            composite.MouseDownCore();
            composite.MouseDownCore();

            Assert.Equal(2, mouseDowns);
            Assert.Same(second, composite.ActiveControl);
            Assert.True(second.Focused);
        });

    [Theory]
    [InlineData(AutoValidate.EnablePreventFocusChange, true, false)]
    [InlineData(AutoValidate.EnableAllowFocusChange, true, true)]
    [InlineData(AutoValidate.EnablePreventFocusChange, false, true)]
    [Trait("Category", "WindowsHandle")]
    public void NestedTabHonorsValidationPolicyAndCausesValidation(
        AutoValidate policy, bool causesValidation, bool movesFocus)
        => OnSta(() =>
        {
            using var form = new Form { AutoValidate = policy };
            using var outer = new UserControl { Parent = form };
            using var inner = new ExposedUserControl { Parent = outer };
            using var editor = new Control { Parent = inner, TabIndex = 0 };
            using var target = new Control { Parent = inner, TabIndex = 1, CausesValidation = causesValidation };
            int validating = 0;
            editor.Validating += (_, e) => { validating++; e.Cancel = true; };
            form.Show();
            Assert.True(editor.Focus());

            Assert.True(inner.DialogKey(Keys.Tab));

            Assert.Equal(causesValidation ? 1 : 0, validating);
            Assert.Same(outer, form.ActiveControl);
            Assert.Same(inner, outer.ActiveControl);
            Assert.Same(movesFocus ? target : editor, inner.ActiveControl);
            Assert.True((movesFocus ? target : editor).Focused);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NestedTabAndShiftTabMoveBetweenChildrenWithoutLosingContainerChain()
        => OnSta(() =>
        {
            using var form = new Form();
            using var outer = new UserControl { Parent = form };
            using var inner = new ExposedUserControl { Parent = outer };
            using var first = new Control { Parent = inner, TabIndex = 0 };
            using var second = new Control { Parent = inner, TabIndex = 1 };
            form.Show();
            Assert.True(first.Focus());

            Assert.True(inner.DialogKey(Keys.Tab));
            Assert.Same(second, inner.ActiveControl);
            Assert.True(second.Focused);
            Assert.True(inner.DialogKey(Keys.Shift | Keys.Tab));
            Assert.Same(first, inner.ActiveControl);
            Assert.True(first.Focused);
            Assert.Same(outer, form.ActiveControl);
            Assert.Same(inner, outer.ActiveControl);
        });

    private sealed class ExposedUserControl : UserControl
    {
        internal bool DialogKey(Keys keys) => ProcessDialogKey(keys);

        internal void MouseDownCore() => OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, 5, 5, 0));
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The nested UserControl focus check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [DllImport("user32.dll", EntryPoint = "SetFocus")]
    private static extern nint SetFocus(nint handle);
}
