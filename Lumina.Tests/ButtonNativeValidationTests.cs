using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class ButtonNativeValidationTests
{
    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NativeMouseClickRunsValidationBeforeClickAndHonorsPreventFocusChange()
        => OnSta(() =>
        {
            using var form = new Form { Bounds = new Rectangle(120, 120, 360, 180) };
            using var editor = new Control { Bounds = new Rectangle(12, 12, 120, 28) };
            using var button = new Button { Bounds = new Rectangle(12, 60, 120, 32) };
            form.Controls.Add(editor);
            form.Controls.Add(button);
            form.AutoValidate = AutoValidate.EnablePreventFocusChange;

            List<string> calls = [];
            editor.Validating += (_, e) =>
            {
                calls.Add("validating");
                e.Cancel = true;
            };
            button.Click += (_, _) => calls.Add("click");

            form.Show();
            Assert.True(editor.Focus());
            Assert.Same(editor, form.ActiveControl);

            SendNativeClick(button);

            Assert.Equal(new[] { "validating" }, calls);
            Assert.Same(editor, form.ActiveControl);
            Assert.True(editor.Focused);
        });

    [Fact]
    [Trait("Category", "WindowsHandle")]
    public void NativeMouseClickAllowsFocusAndClickWhenValidationPolicyAllowsIt()
        => OnSta(() =>
        {
            using var form = new Form { Bounds = new Rectangle(120, 120, 360, 180) };
            using var editor = new Control { Bounds = new Rectangle(12, 12, 120, 28) };
            using var button = new Button { Bounds = new Rectangle(12, 60, 120, 32) };
            form.Controls.Add(editor);
            form.Controls.Add(button);
            form.AutoValidate = AutoValidate.EnableAllowFocusChange;

            List<string> calls = [];
            editor.Validating += (_, e) =>
            {
                calls.Add("validating");
                e.Cancel = true;
            };
            button.Click += (_, _) => calls.Add("click");

            form.Show();
            Assert.True(editor.Focus());
            SendNativeClick(button);

            Assert.Equal(new[] { "validating", "click" }, calls);
            Assert.Same(button, form.ActiveControl);
            Assert.True(button.Focused);
        });

    private static void SendNativeClick(Button button)
    {
        nint handle = button.Handle;
        Assert.True(GetWindowRect(handle, out RECT rect));
        int x = rect.Left + Math.Max(1, (rect.Right - rect.Left) / 2);
        int y = rect.Top + Math.Max(1, (rect.Bottom - rect.Top) / 2);
        Assert.True(SetCursorPos(x, y));
        _ = SendMessageW(handle, 0x0201 /* WM_LBUTTONDOWN */, 1, Pack(8, 8));
        _ = SendMessageW(handle, 0x0202 /* WM_LBUTTONUP */, 0, Pack(8, 8));
    }

    private static nint Pack(int x, int y)
        => unchecked((nint)((uint)(ushort)x | ((uint)(ushort)y << 16)));

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
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The native button validation check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll", EntryPoint = "GetWindowRect", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(nint handle, out RECT rect);

    [DllImport("user32.dll", EntryPoint = "SetCursorPos", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll", EntryPoint = "SendMessageW")]
    private static extern nint SendMessageW(nint handle, uint message, nint wParam, nint lParam);
}
