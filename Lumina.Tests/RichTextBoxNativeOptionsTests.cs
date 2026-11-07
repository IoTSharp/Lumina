using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class RichTextBoxNativeOptionsTests
{
    private const int ScrollStyleMask = 0x003020C4;
    private const int GwlStyle = -16;
    private const int EmGetSel = 0x00B0;
    private const int EmSetModify = 0x00B9;
    private const int EmGetModify = 0x00B8;
    private const int EmSetOptions = 0x044D;
    private const int EmGetOptions = 0x044E;
    private const int EmAutoUrlDetect = 0x045B;
    private const int EmGetAutoUrlDetect = 0x045C;
    private const int EcoAutoWordSelection = 1;

    // These request bits are fixed from v10.0.11's TextBoxBase and RichTextBox
    // CreateParams. Native RichEdit may hide unforced bars for short content;
    // actual HWND styles are compared separately against an independent class.
    [Theory]
    [InlineData(false, false, RichTextBoxScrollBars.None, 0x000000C0)]
    [InlineData(false, false, RichTextBoxScrollBars.Horizontal, 0x000000C0)]
    [InlineData(false, false, RichTextBoxScrollBars.Vertical, 0x000000C0)]
    [InlineData(false, false, RichTextBoxScrollBars.Both, 0x000000C0)]
    [InlineData(false, false, RichTextBoxScrollBars.ForcedHorizontal, 0x000000C0)]
    [InlineData(false, false, RichTextBoxScrollBars.ForcedVertical, 0x000000C0)]
    [InlineData(false, false, RichTextBoxScrollBars.ForcedBoth, 0x000000C0)]
    [InlineData(false, true, RichTextBoxScrollBars.None, 0x000000C0)]
    [InlineData(false, true, RichTextBoxScrollBars.Horizontal, 0x000000C0)]
    [InlineData(false, true, RichTextBoxScrollBars.Vertical, 0x000000C0)]
    [InlineData(false, true, RichTextBoxScrollBars.Both, 0x000000C0)]
    [InlineData(false, true, RichTextBoxScrollBars.ForcedHorizontal, 0x000000C0)]
    [InlineData(false, true, RichTextBoxScrollBars.ForcedVertical, 0x000000C0)]
    [InlineData(false, true, RichTextBoxScrollBars.ForcedBoth, 0x000000C0)]
    [InlineData(true, false, RichTextBoxScrollBars.None, 0x000000C4)]
    [InlineData(true, false, RichTextBoxScrollBars.Horizontal, 0x001000C4)]
    [InlineData(true, false, RichTextBoxScrollBars.Vertical, 0x002000C4)]
    [InlineData(true, false, RichTextBoxScrollBars.Both, 0x003000C4)]
    [InlineData(true, false, RichTextBoxScrollBars.ForcedHorizontal, 0x001020C4)]
    [InlineData(true, false, RichTextBoxScrollBars.ForcedVertical, 0x002020C4)]
    [InlineData(true, false, RichTextBoxScrollBars.ForcedBoth, 0x003020C4)]
    [InlineData(true, true, RichTextBoxScrollBars.None, 0x00000044)]
    [InlineData(true, true, RichTextBoxScrollBars.Horizontal, 0x00000044)]
    [InlineData(true, true, RichTextBoxScrollBars.Vertical, 0x00200044)]
    [InlineData(true, true, RichTextBoxScrollBars.Both, 0x00200044)]
    [InlineData(true, true, RichTextBoxScrollBars.ForcedHorizontal, 0x00000044)]
    [InlineData(true, true, RichTextBoxScrollBars.ForcedVertical, 0x00202044)]
    [InlineData(true, true, RichTextBoxScrollBars.ForcedBoth, 0x00202044)]
    public void TheScrollBarRequestMatchesAllFixedMultilineAndWrappingCombinations(
        bool multiline, bool wordWrap, RichTextBoxScrollBars scrollBars, int expected)
        => OnSta(() =>
        {
            using var control = new OptionProbe
            {
                Multiline = multiline,
                WordWrap = wordWrap,
                ScrollBars = scrollBars
            };
            CreateParams parameters = control.RequestedParameters;
            Assert.Equal("RICHEDIT50W", parameters.ClassName);
            Assert.Equal(expected, parameters.Style & ScrollStyleMask);
            Assert.False(control.IsHandleCreated);
        });

    [Theory]
    [InlineData(BorderStyle.None, 0)]
    [InlineData(BorderStyle.FixedSingle, 0x00000200)]
    [InlineData(BorderStyle.Fixed3D, 0x00000200)]
    public void RichEditUsesClientEdgeInsteadOfTheEditSingleBorder(BorderStyle borderStyle, int expectedEdge)
        => OnSta(() =>
        {
            using var control = new OptionProbe { BorderStyle = borderStyle };
            CreateParams parameters = control.RequestedParameters;
            Assert.Equal(0, parameters.Style & 0x00800000);
            Assert.Equal(expectedEdge, parameters.ExStyle & 0x00000200);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void ChangingScrollBarsRecreatesOncePreservesEditingStateAndMatchesNativeRichEditStyles()
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new OptionProbe
            {
                WordWrap = false,
                ScrollBars = RichTextBoxScrollBars.None,
                Text = "alpha beta"
            };
            nint original = CreateHostedHandle(form, control);
            control.Select(2, 5);
            _ = SendMessageW(original, EmSetModify, 1, 0);
            var lifetime = new List<string>();
            int textChanges = 0;
            control.HandleDestroyed += (_, _) => lifetime.Add("destroyed");
            control.HandleCreated += (_, _) => lifetime.Add("created");
            control.TextChanged += (_, _) => textChanges++;

            control.ScrollBars = RichTextBoxScrollBars.ForcedBoth;

            Assert.Equal(new[] { "destroyed", "created" }, lifetime);
            Assert.Equal("alpha beta", ReadNativeText(control.Handle));
            Assert.Equal("alpha beta", control.Text);
            _ = SendMessageW(control.Handle, EmGetSel, out int start, out int end);
            Assert.Equal(2, start);
            Assert.Equal(7, end);
            nint modified = SendMessageW(control.Handle, EmGetModify, 0, 0);
            Assert.NotEqual((nint)0, modified);
            Assert.Equal(0, textChanges);
            CreateParams parameters = control.RequestedParameters;
            Assert.Equal(0x003020C4, parameters.Style & ScrollStyleMask);

            nint independent = CreateIndependentRichEdit(parameters, "alpha beta");
            try
            {
                _ = SendMessageW(independent, EmSetModify, 1, 0);
                nint independentModified = SendMessageW(independent, EmGetModify, 0, 0);
                Assert.NotEqual((nint)0, independentModified);
                Assert.Equal(independentModified, modified);
                Assert.Equal(GetWindowLongW(independent, GwlStyle) & ScrollStyleMask,
                    GetWindowLongW(control.Handle, GwlStyle) & ScrollStyleMask);
            }
            finally { Assert.True(DestroyWindow(independent)); }

            lifetime.Clear();
            nint current = control.Handle;
            control.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            Assert.Empty(lifetime);
            Assert.Equal(current, control.Handle);
            Assert.Equal("alpha beta", control.Text);
            Assert.Equal(2, control.SelectionStart);
            Assert.Equal(5, control.SelectionLength);
            Assert.True(control.Modified);
            Assert.Equal(0, textChanges);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DetectUrlsChangesTheOldWindowBeforeRecreationAndRestoresTheNewConfiguration(bool value)
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new OptionProbe { DetectUrls = !value, Text = "plain ASCII" };
            nint original = CreateHostedHandle(form, control);
            Assert.Equal(!value ? 1 : 0, (int)SendMessageW(original, EmGetAutoUrlDetect, 0, 0));
            control.Capture = true;
            control.HandleDestroyed += (_, _) => control.UrlTimeline.Add("destroyed");
            control.HandleCreated += (_, _) => control.UrlTimeline.Add("created");

            control.DetectUrls = value;

            Assert.Equal(new[] { "url", "destroyed", "url", "created" }, control.UrlTimeline);
            Assert.Equal(2, control.UrlMessages.Count);
            Assert.Equal(new NativeMessage(original, value ? 1 : 0, 0), control.UrlMessages[0]);
            Assert.Equal(new NativeMessage(control.Handle, value ? 1 : 0, 0), control.UrlMessages[1]);
            Assert.Equal(value ? 1 : 0, (int)SendMessageW(control.Handle, EmGetAutoUrlDetect, 0, 0));
            Assert.Equal(value, control.DetectUrls);
            Assert.Equal("plain ASCII", control.Text);
        });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ReassigningDetectUrlsDoesNotSendOrRecreateAnExistingWindow(bool value)
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new OptionProbe { DetectUrls = value };
            nint handle = CreateHostedHandle(form, control);
            int lifetimeChanges = 0;
            control.HandleDestroyed += (_, _) => lifetimeChanges++;
            control.HandleCreated += (_, _) => lifetimeChanges++;
            control.Capture = true;

            control.DetectUrls = value;

            Assert.Empty(control.UrlMessages);
            Assert.Empty(control.UrlTimeline);
            Assert.Equal(0, lifetimeChanges);
            Assert.Equal(handle, control.Handle);
            Assert.Equal(value ? 1 : 0, (int)SendMessageW(handle, EmGetAutoUrlDetect, 0, 0));
        });

    [Theory]
    [InlineData(false, 4)]
    [InlineData(true, 2)]
    public void RepeatedAutoWordSelectionValuesSendExactlyOneFixedOperationAndMatchIndependentRichEdit(
        bool value, int operation)
        => OnSta(() =>
        {
            using var form = new Form();
            using var control = new OptionProbe { AutoWordSelection = value };
            nint handle = CreateHostedHandle(form, control);
            nint independent = CreateIndependentRichEdit(control.RequestedParameters, string.Empty);
            try
            {
                // Mirror the one setter invocation made by OnHandleCreated.
                // false uses XOR even if it was assigned before construction;
                // a second false assignment intentionally toggles the native bit.
                _ = SendMessageW(independent, EmSetOptions, operation, EcoAutoWordSelection);
                int initial = (int)SendMessageW(handle, EmGetOptions, 0, 0) & EcoAutoWordSelection;
                Assert.Equal((int)SendMessageW(independent, EmGetOptions, 0, 0) & EcoAutoWordSelection, initial);
                control.Capture = true;

                control.AutoWordSelection = value;

                NativeMessage first = Assert.Single(control.OptionMessages);
                Assert.Equal(new NativeMessage(handle, operation, EcoAutoWordSelection), first);
                _ = SendMessageW(independent, EmSetOptions, operation, EcoAutoWordSelection);
                int afterFirst = (int)SendMessageW(handle, EmGetOptions, 0, 0) & EcoAutoWordSelection;
                Assert.Equal((int)SendMessageW(independent, EmGetOptions, 0, 0) & EcoAutoWordSelection, afterFirst);
                Assert.Equal(value, control.AutoWordSelection);
                Assert.Equal(handle, control.Handle);
                if (!value) { Assert.NotEqual(initial, afterFirst); }
                control.OptionMessages.Clear();

                control.AutoWordSelection = value;

                NativeMessage second = Assert.Single(control.OptionMessages);
                Assert.Equal(new NativeMessage(handle, operation, EcoAutoWordSelection), second);
                _ = SendMessageW(independent, EmSetOptions, operation, EcoAutoWordSelection);
                int afterSecond = (int)SendMessageW(handle, EmGetOptions, 0, 0) & EcoAutoWordSelection;
                Assert.Equal((int)SendMessageW(independent, EmGetOptions, 0, 0) & EcoAutoWordSelection, afterSecond);
                Assert.Equal(initial, afterSecond);
                Assert.Equal(value, control.AutoWordSelection);
                Assert.Equal(handle, control.Handle);
                Assert.Empty(control.UrlMessages);
            }
            finally { Assert.True(DestroyWindow(independent)); }
        });

    private static nint CreateHostedHandle(Form form, OptionProbe control)
    {
        form.Controls.Add(control);
        _ = form.Handle;
        nint handle = control.Handle;
        Assert.Equal(form.Handle, GetParent(handle));
        Assert.Same(form, Control.FromHandle(GetParent(handle)));
        return handle;
    }

    private static nint CreateIndependentRichEdit(CreateParams parameters, string text)
    {
        nint module = GetModuleHandleW("msftedit.dll");
        Assert.NotEqual((nint)0, module);
        // The borrowed module remains owned by the library's process-lifetime
        // class registration. This helper owns only the independent HWND.
        uint style = unchecked((uint)parameters.Style);
        style = (style & ~0x50000000u /* CHILD | VISIBLE */) | 0x80000000u /* POPUP */;
        nint handle = CreateWindowExW(unchecked((uint)parameters.ExStyle), "RICHEDIT50W", text,
            style, 0, 0, parameters.Width, parameters.Height, 0, 0, module, 0);
        Assert.NotEqual((nint)0, handle);
        return handle;
    }

    private static string ReadNativeText(nint handle)
    {
        var buffer = new char[128];
        int length = GetWindowTextW(handle, buffer, buffer.Length);
        Assert.InRange(length, 0, buffer.Length - 2);
        return new string(buffer, 0, length);
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
        Assert.True(thread.Join(TimeSpan.FromSeconds(12)), "RichTextBox native options test timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private readonly record struct NativeMessage(nint Handle, nint WParam, nint LParam);

    private sealed class OptionProbe : RichTextBox
    {
        internal CreateParams RequestedParameters => base.CreateParams;
        internal bool Capture { get; set; }
        internal List<NativeMessage> OptionMessages { get; } = new();
        internal List<NativeMessage> UrlMessages { get; } = new();
        internal List<string> UrlTimeline { get; } = new();

        protected override void WndProc(ref Message message)
        {
            if (Capture && message.Msg == EmSetOptions)
            {
                OptionMessages.Add(new(message.HWnd, message.WParam, message.LParam));
            }
            if (Capture && message.Msg == EmAutoUrlDetect)
            {
                UrlMessages.Add(new(message.HWnd, message.WParam, message.LParam));
                UrlTimeline.Add("url");
            }
            base.WndProc(ref message);
        }
    }

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint GetParent(nint hWnd);

    [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern nint GetModuleHandleW(string moduleName);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern nint CreateWindowExW(uint exStyle, string className, string windowName,
        uint style, int x, int y, int width, int height, nint parent, nint menu, nint instance, nint parameter);

    [DllImport("user32.dll", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DestroyWindow(nint hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetWindowLongW(nint hWnd, int index);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(nint hWnd, [Out] char[] text, int maxCount);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, nint wParam, nint lParam);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern nint SendMessageW(nint hWnd, int message, out int wParam, out int lParam);
}
