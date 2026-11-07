using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ControlParkingCommandTests
{
    private const int WmCommand = 0x0111;
    private const int EmSetSel = 0x00B1;
    private const int EmGetModify = 0x00B8;
    private const int EmReplaceSel = 0x00C2;
    private const int EnUpdate = 0x0400;

    [Fact]
    public void UnparentedAppendTextRaisesExactlyOneNotificationAndPreservesExistingTwoChangeContract()
        => OnSta(() =>
        {
            using var textBox = new TextBox();
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            textBox.Text = "Hello";
            textBox.Text = "Hello";
            Assert.Equal(1, changes);
            Assert.False(textBox.IsHandleCreated);

            textBox.AppendText(" World");

            Assert.Equal(2, changes);
            Assert.Equal("Hello World", textBox.Text);
            Assert.True(textBox.IsHandleCreated);
            Assert.Null(textBox.Parent);
            _ = AssertParked(textBox);
        });

    [Fact]
    public void UnparentedNativeReplacementAndUndoUpdateManagedTextAndModifiedObservation()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "abcd" };
            nint handle = textBox.Handle;
            _ = AssertParked(textBox);
            int changes = 0;
            var modifiedValues = new List<bool>();
            textBox.TextChanged += (_, _) => changes++;
            textBox.ModifiedChanged += (_, _) => modifiedValues.Add(textBox.Modified);
            Assert.False(textBox.Modified);

            _ = SendMessageW(handle, EmSetSel, 1, 3);
            _ = SendMessageW(handle, EmReplaceSel, -1, "X");

            Assert.Equal("aXd", textBox.Text);
            Assert.Equal(1, changes);
            Assert.True(textBox.Modified);
            Assert.Equal(new[] { true }, modifiedValues);
            Assert.True(textBox.CanUndo);

            textBox.Undo();

            Assert.Equal("abcd", textBox.Text);
            Assert.Equal(2, changes);
            bool nativeModified = SendMessageW(handle, EmGetModify, 0, 0) != 0;
            Assert.Equal(nativeModified, textBox.Modified);
            Assert.NotEmpty(modifiedValues);
            Assert.Equal(nativeModified, modifiedValues[^1]);
            Assert.Equal(handle, textBox.Handle);
            _ = AssertParked(textBox);
        });

    [Fact]
    public void ChildParkedBeforeParentCreationKeepsNotificationsAndReparentsWithoutDuplicates()
        => OnSta(() =>
        {
            using var form = new Form();
            using var textBox = new TextBox { Text = "abcd" };
            form.Controls.Add(textBox);
            Assert.False(form.IsHandleCreated);
            nint handle = textBox.Handle;
            nint parking = AssertParked(textBox);
            Assert.False(form.IsHandleCreated);
            int changes = 0;
            textBox.TextChanged += (_, _) => changes++;

            _ = SendMessageW(handle, EmSetSel, 1, 3);
            _ = SendMessageW(handle, EmReplaceSel, -1, "X");

            Assert.Equal("aXd", textBox.Text);
            Assert.Equal(1, changes);

            // A hidden Form.Handle only creates its own HWND. The standard
            // Show/CreateControl path reparents already-created children.
            form.Show();

            Assert.Equal(handle, textBox.Handle);
            Assert.Equal(form.Handle, GetParent(handle));
            Assert.Same(form, Control.FromHandle(GetParent(handle)));
            Assert.Same(textBox, Control.FromHandle(handle));
            Assert.Equal((nint)0, (nint)ParkingField.GetValue(textBox)!);
            Assert.False(IsWindow(parking));
            Assert.Equal("aXd", textBox.Text);
            Assert.Equal(1, changes);
            Assert.Equal((int)typeof(Control).GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic)!
                .GetValue(textBox)!, GetDlgCtrlID(handle));
            // Notifications after native reparenting remain a separate
            // Control-17/TextBoxBase-006 gap. The failed probe and native ID /
            // Form-command trace are retained in compatibility-batch048.
        });

    [Fact]
    public void ParkingWindowIsUnmappedAndOwnedChildDisposalDestroysBothWindows()
        => OnSta(() =>
        {
            using var textBox = new TextBox { Text = "abcd" };
            nint handle = textBox.Handle;
            nint parking = AssertParked(textBox);
            Assert.True(IsWindow(handle));
            Assert.True(IsWindow(parking));

            textBox.Dispose();

            Assert.False(IsWindow(handle));
            Assert.False(IsWindow(parking));
            Assert.Null(Control.FromHandle(handle));
            Assert.Null(Control.FromHandle(parking));
            Assert.Equal((nint)0, (nint)ParkingField.GetValue(textBox)!);
            Assert.False(textBox.IsHandleCreated);
            textBox.Dispose();
        });

    [Fact]
    public void ParkingCommandOnlyRoutesMappedControlsWhoseActualParentMatchesTheReceiver()
        => OnSta(() =>
        {
            using var first = new CommandTextBox();
            using var second = new CommandTextBox();
            nint firstHandle = first.Handle;
            nint firstParking = AssertParked(first);
            nint secondHandle = second.Handle;
            nint secondParking = AssertParked(second);
            Assert.NotEqual(firstParking, secondParking);
            first.Notifications.Clear();
            second.Notifications.Clear();
            nint notification = EnUpdate << 16;

            _ = SendMessageW(firstParking, WmCommand, notification, secondHandle);
            _ = SendMessageW(firstParking, WmCommand, notification, 0);

            Assert.Empty(first.Notifications);
            Assert.Empty(second.Notifications);

            _ = SendMessageW(firstParking, WmCommand, notification, firstHandle);

            Assert.Equal(new[] { EnUpdate }, first.Notifications);
            Assert.Empty(second.Notifications);
            Assert.Equal(firstParking, GetParent(firstHandle));
            Assert.Equal(secondParking, GetParent(secondHandle));
        });

    private static FieldInfo ParkingField
        => typeof(Control).GetField("_parkingWindow", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static nint AssertParked(TextBox textBox)
    {
        nint handle = textBox.Handle;
        nint parking = (nint)ParkingField.GetValue(textBox)!;
        Assert.NotEqual((nint)0, parking);
        Assert.Equal(parking, GetParent(handle));
        Assert.True(IsWindow(parking));
        Assert.Null(Control.FromHandle(parking));
        Assert.Same(textBox, Control.FromHandle(handle));
        return parking;
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "Parking command test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class CommandTextBox : TextBox
    {
        internal List<int> Notifications { get; } = new();

        protected override bool OnCommand(int notificationCode)
        {
            Notifications.Add(notificationCode);
            return base.OnCommand(notificationCode);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetDlgCtrlID(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, string lParam);
}
