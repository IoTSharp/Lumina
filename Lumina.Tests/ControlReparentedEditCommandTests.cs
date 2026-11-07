using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlReparentedEditCommandTests
{
    private const int WmCommand = 0x0111;
    private const int EmSetSel = 0x00B1;
    private const int EmReplaceSel = 0x00C2;
    private const int EnChange = 0x0300;
    private const int EnUpdate = 0x0400;

    [Fact]
    public void IndependentNativeEditReparentingPreservesItsOriginalCommandReceiver()
        => OnSta(() =>
        {
            using var originalParent = new CommandParent();
            using var newParent = new CommandParent();
            using var configuration = new CommandTextBox { Text = "abcd" };
            nint originalHandle = originalParent.Handle;
            nint newHandle = newParent.Handle;
            CreateParams parameters = configuration.RequestedParameters;
            nint edit = CreateWindowExW(unchecked((uint)parameters.ExStyle), "EDIT", parameters.Caption,
                unchecked((uint)parameters.Style) & ~0x10000000u,
                parameters.X, parameters.Y, parameters.Width, parameters.Height,
                originalHandle, 1001, GetModuleHandleW(null), 0);
            Assert.NotEqual((nint)0, edit);
            try
            {
                Assert.Equal(originalHandle, GetParent(edit));
                originalParent.Commands.Clear();
                newParent.Commands.Clear();

                _ = SendMessageW(edit, EmSetSel, 1, 3);
                _ = SendMessageW(edit, EmReplaceSel, -1, "X");

                Assert.Equal("aXd", GetNativeText(edit));
                Assert.Equal(new[] { EnUpdate, EnChange }, originalParent.EditCommands(edit));
                Assert.Empty(newParent.EditCommands(edit));

                Assert.Equal(originalHandle, SetParent(edit, newHandle));
                Assert.Equal(newHandle, GetParent(edit));
                Assert.Equal(1001, GetDlgCtrlID(edit));
                originalParent.Commands.Clear();
                newParent.Commands.Clear();

                _ = SendMessageW(edit, EmSetSel, 3, 3);
                _ = SendMessageW(edit, EmReplaceSel, -1, "Y");

                Assert.Equal("aXdY", GetNativeText(edit));
                // This independent EDIT has no Lumina handle registration or
                // command routing. Capture the native receiver directly while
                // both parent HWNDs remain alive.
                Assert.Equal(new[] { EnUpdate, EnChange }, originalParent.EditCommands(edit));
                Assert.Empty(newParent.EditCommands(edit));
            }
            finally { Assert.True(DestroyWindow(edit)); }
        });

    [Fact]
    public void ParkedEditDeliversNativeChangesAfterItsExistingHandleIsReparentedToAForm()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new CommandTextBox { Text = "abcd" };
            form.Controls.Add(textBox);
            nint handle = textBox.Handle;
            nint originalParent = GetParent(handle);
            Assert.NotEqual((nint)0, originalParent);
            Assert.Null(Control.FromHandle(originalParent));
            Assert.False(form.IsHandleCreated);
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            _ = SendMessageW(handle, EmSetSel, 1, 3);
            _ = SendMessageW(handle, EmReplaceSel, -1, "X");

            Assert.Equal("aXd", textBox.Text);
            Assert.Equal(1, changes);
            textBox.ClearUndo();
            Assert.False(textBox.CanUndo);
            form.Show();
            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.Same(form, Control.FromHandle(GetParent(handle)));
            Assert.Same(textBox, Control.FromHandle(handle));
            Assert.Equal(1, changes);
            textBox.Commands.Clear();

            _ = SendMessageW(handle, EmSetSel, 3, 3);
            _ = SendMessageW(handle, EmReplaceSel, -1, "Y");

            Assert.Equal("aXdY", GetNativeText(handle));
            Assert.Equal("aXdY", textBox.Text);
            Assert.Equal(2, changes);
            Assert.Equal(new[] { EnUpdate, EnChange }, textBox.EditCommands());
            Assert.True(textBox.Modified);
            Assert.True(textBox.CanUndo);
            Assert.Equal(handle, textBox.Handle);

            textBox.Commands.Clear();
            textBox.Undo();

            Assert.Equal("aXd", textBox.Text);
            Assert.Equal(3, changes);
            Assert.Equal(new[] { EnUpdate, EnChange }, textBox.EditCommands());
            Assert.Equal(handle, textBox.Handle);
        });

    [Fact]
    public void EditMovedBetweenLiveFormsRoutesNotificationsByItsExistingHandle()
        => OnSta(() =>
        {
            using var originalForm = new Form();
            using var newForm = new Form();
            using var textBox = new CommandTextBox { Text = "abcd" };
            originalForm.Controls.Add(textBox);
            originalForm.Show();
            newForm.Show();
            nint handle = textBox.Handle;
            Assert.Equal(originalForm.Handle, GetParent(handle));
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.Parent = newForm;

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(newForm.Handle, GetParent(handle));
            Assert.Same(newForm, textBox.FindForm());
            Assert.Equal(0, changes);
            textBox.Commands.Clear();

            _ = SendMessageW(handle, EmSetSel, 1, 3);
            _ = SendMessageW(handle, EmReplaceSel, -1, "X");

            Assert.Equal("aXd", GetNativeText(handle));
            Assert.Equal("aXd", textBox.Text);
            Assert.Equal(1, changes);
            Assert.Equal(new[] { EnUpdate, EnChange }, textBox.EditCommands());
            Assert.True(textBox.Modified);
            Assert.True(textBox.CanUndo);
            Assert.Equal(handle, textBox.Handle);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void OriginalParkingReceiverRemainsPrivateUntilItsEditIsDestroyed(bool destroyExternally)
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new CommandTextBox { Text = "abcd" };
            form.Controls.Add(textBox);
            nint handle = textBox.Handle;
            nint parking = GetParent(handle);
            Assert.True(IsWindow(parking));
            Assert.Null(Control.FromHandle(parking));

            form.Show();

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.True(IsWindow(parking));
            Assert.Null(Control.FromHandle(parking));
            Assert.Same(textBox, Control.FromHandle(handle));

            if (destroyExternally) { Assert.True(DestroyWindow(handle)); }
            else { textBox.Dispose(); }

            Assert.False(IsWindow(handle));
            Assert.False(IsWindow(parking));
            Assert.Null(Control.FromHandle(handle));
            Assert.Null(Control.FromHandle(parking));
            Assert.False(textBox.IsHandleCreated);
            textBox.Dispose();
        });

    [Fact]
    public void RecreatedEditReleasesItsOldParkingReceiverAndUsesTheRealParentOnce()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new CommandTextBox { Text = "abcd" };
            form.Controls.Add(textBox);
            nint originalHandle = textBox.Handle;
            nint parking = GetParent(originalHandle);
            form.Show();
            Assert.True(IsWindow(parking));
            Assert.Equal(form.Handle, GetParent(originalHandle));
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.Recreate();

            nint handle = textBox.Handle;
            Assert.NotEqual(originalHandle, handle);
            Assert.False(IsWindow(originalHandle));
            Assert.False(IsWindow(parking));
            Assert.Null(Control.FromHandle(originalHandle));
            Assert.Null(Control.FromHandle(parking));
            Assert.Same(textBox, Control.FromHandle(handle));
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.Equal("abcd", textBox.Text);
            Assert.Equal(0, changes);
            textBox.Commands.Clear();

            _ = SendMessageW(handle, EmSetSel, 1, 3);
            _ = SendMessageW(handle, EmReplaceSel, -1, "X");

            Assert.Equal("aXd", GetNativeText(handle));
            Assert.Equal("aXd", textBox.Text);
            Assert.Equal(1, changes);
            Assert.Equal(new[] { EnUpdate, EnChange }, textBox.EditCommands());
        });

    [Fact]
    public void RetainedParkingReceiverRejectsTheOtherReparentedEditAsItsSource()
        => OnSta(() =>
        {
            using var form = new Form();
            using var first = new CommandTextBox { Text = "first" };
            using var second = new CommandTextBox { Text = "second" };
            form.Controls.Add(first);
            form.Controls.Add(second);
            nint firstHandle = first.Handle;
            nint firstParking = GetParent(firstHandle);
            nint secondHandle = second.Handle;
            nint secondParking = GetParent(secondHandle);
            Assert.NotEqual(firstParking, secondParking);
            form.Show();
            Assert.Equal(form.Handle, GetParent(firstHandle));
            Assert.Equal(form.Handle, GetParent(secondHandle));
            Assert.True(IsWindow(firstParking));
            Assert.True(IsWindow(secondParking));
            Assert.Null(Control.FromHandle(firstParking));
            Assert.Null(Control.FromHandle(secondParking));
            first.Commands.Clear();
            second.Commands.Clear();

            _ = SendMessageW(firstParking, WmCommand, EnUpdate << 16, secondHandle);
            _ = SendMessageW(secondParking, WmCommand, EnUpdate << 16, firstHandle);
            _ = SendMessageW(firstParking, WmCommand, EnUpdate << 16, 0);

            Assert.Empty(first.Commands);
            Assert.Empty(second.Commands);

            _ = SendMessageW(firstParking, WmCommand, EnUpdate << 16, firstHandle);

            Assert.Equal(new[] { EnUpdate }, first.Commands);
            Assert.Empty(second.Commands);
        });

    [Fact]
    public void FormCommandReflectsTheSourceHandleEvenWhenItsIdMatchesAnotherControl()
        => OnSta(() =>
        {
            using var originalForm = new Form();
            using var newForm = new Form();
            using var moved = new CommandTextBox { Text = "moved" };
            using var occupied = new CommandTextBox { Text = "occupied" };
            using var counterpart = new CommandTextBox { Text = "counterpart" };
            originalForm.Controls.Add(moved);
            newForm.Controls.Add(occupied);
            originalForm.Show();
            newForm.Show();
            nint movedHandle = moved.Handle;
            moved.Parent = newForm;
            originalForm.Controls.Add(counterpart);
            nint counterpartHandle = counterpart.Handle;
            Assert.Equal(movedHandle, moved.Handle);
            Assert.Equal(newForm.Handle, GetParent(movedHandle));
            Assert.Equal(originalForm.Handle, GetParent(counterpartHandle));
            int sourceId = GetDlgCtrlID(movedHandle);
            Assert.Equal(GetDlgCtrlID(counterpartHandle), sourceId);
            Assert.NotEqual(movedHandle, counterpartHandle);
            Assert.Same(moved, Control.FromHandle(movedHandle));
            moved.Commands.Clear();
            occupied.Commands.Clear();
            counterpart.Commands.Clear();

            // The old native receiver can still receive a control command.
            // Its current ID registration belongs to a different control.
            _ = SendMessageW(originalForm.Handle, WmCommand, (EnUpdate << 16) | sourceId, movedHandle);

            Assert.Equal(new[] { EnUpdate }, moved.Commands);
            Assert.Empty(occupied.Commands);
            Assert.Empty(counterpart.Commands);
            Assert.Equal("moved", moved.Text);
            Assert.Equal("counterpart", counterpart.Text);
        });

    private static string GetNativeText(nint handle)
    {
        var text = new StringBuilder(32);
        int length = GetWindowTextW(handle, text, text.Capacity);
        Assert.InRange(length, 0, text.Capacity - 2);
        return text.ToString();
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "Reparented EDIT command test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class CommandParent : Control
    {
        internal List<(int Notification, nint Source)> Commands { get; } = new();

        internal int[] EditCommands(nint source)
            => Commands.Where(command => command.Source == source
                && command.Notification is EnUpdate or EnChange)
                .Select(command => command.Notification).ToArray();

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == WmCommand)
            {
                Commands.Add(((int)((long)message.WParam >> 16) & 0xFFFF, message.LParam));
            }
            base.WndProc(ref message);
        }
    }

    private sealed class CommandTextBox : TextBox
    {
        internal CreateParams RequestedParameters => CreateParams;
        internal List<int> Commands { get; } = new();

        internal int[] EditCommands()
            => Commands.Where(command => command is EnUpdate or EnChange).ToArray();

        internal void Recreate() => RecreateHandle();

        protected override bool OnCommand(int notificationCode)
        {
            Commands.Add(notificationCode);
            return base.OnCommand(notificationCode);
        }
    }

    [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint GetModuleHandleW(string? moduleName);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string? windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint param);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint hWnd, StringBuilder text, int maxCount);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SetParent(nint hWnd, nint newParent);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetDlgCtrlID(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, string lParam);
}
